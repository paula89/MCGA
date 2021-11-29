using BLL;
using Helpers;
using Helpers.Enum;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL_Arm
{
    class Program
    {
        private static swaggerClient _client = new swaggerClient("https://localhost:44307/", new System.Net.Http.HttpClient());
        private static ArmBLL _armBLL = new ArmBLL();
        private static LogBLL _logBLL = new LogBLL("CL_Arm");
        private static List<Package> _waitingPackages = new List<Package>();
        private static bool _armWasOn = false;
        private static bool _armIsOn = false;
        private static bool _pressWasIdle = false;
        private static bool _pressIsIdle = false;

        public static object JsonSerealize { get; private set; }

        static async Task Main(string[] args)
        {
            while (true)
            {
                await RunArmAsync();
            }
        }

        private static async Task RunArmAsync()
        {
            CheckArmStatus();
            CheckPressIdle();
            while (_armIsOn && _pressIsIdle)
            {
                await ExecuteTransportAsync();
                CheckArmStatus();
                CheckPressIdle();
            }
        }

        private static async Task ExecuteTransportAsync()
        {
            if (!_waitingPackages.Any())
            {
                UpdatePackageWaitingQueue();
                return;
            }
            await TransportToPress();
        }

        private static async Task TransportToPress()
        {
            await _logBLL.Log($"---------------------------------------------------------------");
            var package = _waitingPackages.First();
            _waitingPackages.Remove(package);
            await _logBLL.Log($"Se encontro el paquete {package.Id}.");
            await _logBLL.Log($"Se inicia el proceso de transporte el paquete {package.Id}.");
            var result = await _armBLL.CarryPackage(package.Id);
            await _logBLL.Log(result);
            Thread.Sleep(10000);
            result = await _armBLL.DeliverPackage(package.Id);
            await _logBLL.Log(result);
            Thread.Sleep(2000);
            await _logBLL.Log($"---------------------------------------------------------------");
        }

        private static void UpdatePackageWaitingQueue()
        {
            var queueName = "Package_Waiting";
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(3),
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumidor = new EventingBasicConsumer(channel);
                consumidor.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensaje = Encoding.UTF8.GetString(body);

                    _ = _logBLL.Log($"[X] Recibido {mensaje}");
                    
                    _waitingPackages.Add(JsonSerializer.Deserialize<Package>(body));
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumidor);
                Thread.Sleep(10000);
            }
        }

        private static void CheckArmStatus()
        {
            _armIsOn = MachineIsOn(_client.GetArmStatusAsync().Result);
            if (_armIsOn == _armWasOn)
            {
                return;
            }
            _armWasOn = _armIsOn;
            _ = _logBLL.Log(_armIsOn ? "Se inicio el Brazo" : "Se paro el Brazo");
        }

        private static void CheckPressIdle()
        {
            _pressIsIdle = MachineIsIdle(_client.PressIsIdleAsync().Result);
            if (_pressIsIdle == _pressWasIdle)
            {
                return;
            }
            _pressWasIdle = _pressIsIdle;
            _ = _logBLL.Log(_pressIsIdle ? "La Prensa esta libre" : "La prensa esta ocupada o apagada");
        }

        private static bool MachineIsOn(string result) => (MachineStatusEnum)Convert.ToInt32(result) == MachineStatusEnum.On;

        private static bool MachineIsIdle(string result) => Convert.ToBoolean(result);
    }
}
