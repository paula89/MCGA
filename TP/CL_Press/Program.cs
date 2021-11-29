using BLL;
using Helpers.Enum;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL_Press
{
    class Program
    {
        private static swaggerClient _client = new swaggerClient("https://localhost:44307/", new System.Net.Http.HttpClient());
        private static PressBLL _pressBLL = new PressBLL();
        private static LogBLL _logBLL = new LogBLL("CL_Press");
        private static bool _pressWasOn = false;
        private static bool _pressIsOn = false;
        private static bool _emptyBandQueue = false;

        static async Task Main(string[] args)
        {
            while (true)
            {
                await RunPressAsync();
            }
        }

        private static async Task RunPressAsync()
        {
            CheckPressStatus();
            while (_pressIsOn)
            {
                await ExecutePressAsync();
                CheckPressStatus();
            }
        }

        private static async Task ExecutePressAsync()
        {
            // Toma el packete en Delivered e Indica que esta ocupado si toma alguno
            var bandQueue = _client.GetAllPackagesByStatusOnListAsync(4).Result;
            if (!bandQueue.Any())
            {
                if (bandQueue.Any() != _emptyBandQueue)
                {
                    _emptyBandQueue = bandQueue.Any();
                    await _logBLL.Log($"La Prensa se encuentra libre.");
                }
                return;
            }
            await _logBLL.Log($"---------------------------------------------------------------");
            await _pressBLL.Busy();
            var package = bandQueue.First();

            await _logBLL.Log($"Se detecta el paquete {package.Id}.");
            await _logBLL.Log($"Se comienza el prensado.");
            Thread.Sleep(10000);
            await _logBLL.Log($"Prensa Abajo.");
            await _pressBLL.DownPress();

            // Prensa y cambia el estado del packete a pressed
            var result = await _pressBLL.PackagePressed(package.Id);
            await _logBLL.Log(result);
            Thread.Sleep(10000);
            await _pressBLL.UpPress();
            await _logBLL.Log($"Se finaliza el prensado.");

            // Envia el packete a la cola de apilacion.
            await _logBLL.Log($"Se inicia el proceso de transporte a la pila de bultos del paquete {package.Id}.");
            SendPackagePressedMessageToRabbitMQ(package);
            Thread.Sleep(20000);

            // Cambia estado a Idle Nuevamenchi
            await _logBLL.Log($"Prensa Descargada.");
            await _pressBLL.Free();
            await _logBLL.Log($"---------------------------------------------------------------");
        }

        private static void SendPackagePressedMessageToRabbitMQ(Package package)
        {
            const string queueName = "Pressed_Packages";
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(3),
                };

                using (var rabbitConnection = connectionFactory.CreateConnection())
                {
                    using (var channel = rabbitConnection.CreateModel())
                    {
                        //-> Cola
                        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                        //-> Mensaje
                        string mensaje = JsonSerializer.Serialize(package);
                        var body = Encoding.UTF8.GetBytes(mensaje);

                        //-> Enviamos el mensaje
                        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

                        _ = _logBLL.Log($"[x] Enviado {mensaje}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        private static void CheckPressStatus()
        {
            _pressIsOn = MachineIsOn(_client.GetPressStatusAsync().Result);
            if (_pressIsOn == _pressWasOn)
            {
                return;
            }
            _pressWasOn = _pressIsOn;
            _ = _logBLL.Log(_pressIsOn ? "Se inicio la Prensa" : "Se paro la Prensa");
        }

        private static bool MachineIsOn(string result) => (MachineStatusEnum)Convert.ToInt32(result) == MachineStatusEnum.On;
    }
}
