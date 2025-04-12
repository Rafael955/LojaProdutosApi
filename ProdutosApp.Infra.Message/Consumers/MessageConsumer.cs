using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.Logging.Repositories;
using ProdutosApp.Infra.Message.Models;
using ProdutosApp.Infra.Message.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Message.Consumers
{
    public class MessageConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMqSettings = new RabbitMQSettings();
        private readonly ILoggingRepository _loggingRepository;
        
        public MessageConsumer(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }   

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.Host,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.User,
                Password = _rabbitMqSettings.Password,
                VirtualHost = _rabbitMqSettings.VirtualHost
            };

            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();
            model.QueueDeclare(
                queue: _rabbitMqSettings.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(model);

            consumer.Received += (sender, args) =>
            {
                var payload = args.Body.ToArray();

                var json = Encoding.UTF8.GetString(payload);

                var produtoCriado = JsonConvert.DeserializeObject<ProdutoCriado>(json);

                //enviando o email para usuário
                var mailHelper = new Helpers.MailHelper();
                mailHelper.SendMail(produtoCriado);

                //gravando log do sistema
                _loggingRepository.GravarLog_CadastroProduto(new Logging_CadastroProduto
                {
                    Id = produtoCriado.Id,
                    Produto = produtoCriado.Nome,
                    Fornecedor = produtoCriado.Fornecedor,
                    Descricao = "Produto cadastrado com sucesso",
                    DataHoraGravacao = DateTime.Now
                });

                //retirar o registro da fila
                model.BasicAck(args.DeliveryTag, false);
            };

            //executando e finalizando a leitura...
            model.BasicConsume(
                queue: _rabbitMqSettings.Queue,
                consumer: consumer,
                autoAck: false
                );
        }
    }
}
