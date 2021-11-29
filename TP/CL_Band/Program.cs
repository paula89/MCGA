using BLL;
using Helpers;
using Helpers.Enum;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL_Band
{
    class Program
    {
        private static swaggerClient _client = new swaggerClient("https://localhost:44307/", new System.Net.Http.HttpClient());
        private static BandBLL _bandBLL = new BandBLL();
        private static LogBLL _logBLL = new LogBLL("CL_Band");
        private static bool _bandWasOn = false;
        private static bool _bandIsOn = false;
        private static bool _emptyBandQueue = false;

        static async Task Main(string[] args)
        {
            while (true)
            {
                await RunBandAsync();
            }
        }

        private static async Task RunBandAsync()
        {
            CheckBandStatus();
            while (_bandIsOn)
            {
                await ExecuteTransportAsync();
                CheckBandStatus();
            }
        }

        private static async Task ExecuteTransportAsync()
        {
            var bandQueue = _client.GetAllPackagesByStatusOnListAsync(0).Result;
            if (!bandQueue.Any())
            {
                if (bandQueue.Any() != _emptyBandQueue)
                {
                    _emptyBandQueue = bandQueue.Any();
                    await _logBLL.Log($"La cinta no tiene paquetes para transportar.");
                }
                return;             
            }
            _emptyBandQueue = bandQueue.Any();

            await _logBLL.Log($"---------------------------------------------------------------");
            await _logBLL.Log($"Se comenzo el transporte por la cinta de {bandQueue.Count} paquetes.");
            foreach (var package in bandQueue)
            {
                await _logBLL.Log($"---------------------------------------------------------------");
                await _logBLL.Log($"Se inicia el proceso de transporte en la cinta del paquete {package.Id}.");
                var result = await _bandBLL.PackageOnBand(package.Id);
                await _logBLL.Log(result);
                Thread.Sleep(5000);
                SendPackageWaitingMessageToRabbitMQ(package);
                result = await _bandBLL.PackageQueued(package.Id);
                await _logBLL.Log(result);
                await _logBLL.Log($"---------------------------------------------------------------");
            }
            await _logBLL.Log($"---------------------------------------------------------------");
        }

        private static void SendPackageWaitingMessageToRabbitMQ(Package package)
        {
            const string queueName = "Package_Waiting";
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


        private static void CheckBandStatus()
        {
            _bandIsOn = MachineIsOn(_client.GetBandStatusAsync().Result);
            if (_bandIsOn == _bandWasOn)
            {
                return;
            }
            _bandWasOn = _bandIsOn;
            _ = _logBLL.Log(_bandIsOn ? "Se inicio la Cinta" : "Se paro la Cinta");
        }

        private static bool MachineIsOn(string result) => (MachineStatusEnum)Convert.ToInt32(result) == MachineStatusEnum.On;
    }
}
