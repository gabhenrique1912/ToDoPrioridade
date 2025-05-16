using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

public class Subscriber
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://zcpokczm:b0YygzvfieNuAbm93d3L8vEdV8fDRffE@jackal.rmq.cloudamqp.com/zcpokczm") // Use sua URI
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declarar a fila da qual você quer consumir (use o mesmo nome do Publisher)
            string PriorityQueue = "fila.prioridades";
            channel.QueueDeclare(queue: PriorityQueue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var queueName = ea.RoutingKey; // Obtém o nome da fila
                string priority = queueName.Replace("fila.prioridade.", "Fila de prioridade ");

                Console.WriteLine($"{priority}: {message} ");
                Thread.Sleep(1000);

                //Confirma o recebimento
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Iniciar o consumo
            channel.BasicConsume(queue: PriorityQueue,
                                 autoAck: false,
                                 consumer: consumer);

           

            Console.WriteLine(" [*] Aguardando mensagens...");
            Console.ReadKey();
        }
    }
}