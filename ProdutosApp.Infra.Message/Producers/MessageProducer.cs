using Newtonsoft.Json;
using ProdutosApp.Infra.Message.Models;
using ProdutosApp.Infra.Message.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Message.Producers
{
    public class MessageProducer
    {
        private readonly RabbitMQSettings _rabbitMQSettings = new RabbitMQSettings();

        public void SendMessage(ProdutoCriado produto)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.Host,
                UserName = _rabbitMQSettings.User,
                Password = _rabbitMQSettings.Password,
                Port = _rabbitMQSettings.Port,
                VirtualHost = _rabbitMQSettings.VirtualHost
            };

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare(
                        queue: _rabbitMQSettings.Queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var json = JsonConvert.SerializeObject(produto);

                    model.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _rabbitMQSettings.Queue,
                        body: Encoding.UTF8.GetBytes(json),
                        basicProperties: null
                        );
                }
            }
        }
    }
}
