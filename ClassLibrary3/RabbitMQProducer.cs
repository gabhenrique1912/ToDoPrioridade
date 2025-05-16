using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://zcpokczm:b0YygzvfieNuAbm93d3L8vEdV8fDRffE@jackal.rmq.cloudamqp.com/zcpokczm")
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string PriorityQueue = "fila.prioridades";


            channel.QueueDeclare(queue: PriorityQueue,
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null); 
            //Serializa a mensagem
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            //Põe os dados na fila : product
            channel.BasicPublish(exchange: "", routingKey: "PriorityQueue", body: body);

            Console.WriteLine("Teste");

        }
    }
}
