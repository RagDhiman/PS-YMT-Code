using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AccountsManager_API.Workers
{
    public class RabMQServiceBase : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabMQServiceBase> _logger;
        private readonly string _hostName;
        private readonly int _port;

        //protected string RouteKey;
        protected string QueueName;

        public RabMQServiceBase(ILogger<RabMQServiceBase> logger, IConfiguration configuration)
        {
            try
            {
                _logger = logger;
                _hostName = configuration.GetValue<string>("RabbitMQHost");
                _port = int.Parse(configuration.GetValue<string>("RabbitMQPort"));

                if (!string.IsNullOrEmpty(_hostName))
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = _hostName,
                        Port = _port,
                    };
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RabbitListener init error,ex:{ex.Message}");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && !string.IsNullOrEmpty(_hostName))
            {
                Register();

                await Task.Delay(1000, stoppingToken);
            }
        }

        public void Register()
        {
            _channel.ExchangeDeclare(exchange: "AccountManager", type: "direct");
            _channel.QueueDeclare(queue: QueueName, exclusive: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += MessageReceived;

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }

        private void MessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);

            var result = Process(message);

            if (result)
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
        }

        public virtual bool Process(string message)
        {
            throw new NotImplementedException();
        }

        public void DeRegister()
        {
            _connection.Close();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _logger.LogInformation($"Worker disposed at: {DateTime.Now}");
            _channel.Dispose();
            _connection.Dispose();
            base.Dispose();
        }
    }
}
