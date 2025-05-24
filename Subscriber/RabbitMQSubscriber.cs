using ClassLibrary2;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Channels;

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
                                 arguments: new Dictionary<string, object> {
                {"x-max-priority", 1 }
            });

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);



            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var queueName = ea.RoutingKey; // Obtém o nome da fila

                string replace = message.Replace("}", " ");
                string replace2 = replace.Replace("{", " ");
                string replace3 = replace2.Replace("\"", " ");
                string trim = replace3.Trim();

                int indexId = trim.IndexOf(':');
                int indexId2 = trim.IndexOf(',');
                int comprimentoId = indexId2 - indexId;
                string id = trim.Substring(indexId, comprimentoId).Trim();
                string id2 = id.Remove(0, 1);

                int indexTarefa = trim.IndexOf(':', indexId + 1);
                int indexTarefa2 = trim.IndexOf(',', indexId2 + 1);
                int comprimentoTarefa = indexTarefa2 - indexTarefa;
                string tarefa = trim.Substring(indexTarefa, comprimentoTarefa);
                string tarefa2 = tarefa.Remove(0, 2);

                int indexDescricao = trim.IndexOf(':', indexTarefa + 1);
                int indexDescricao2 = trim.IndexOf(',', indexTarefa2 + 1);
                int comprimentoDescricao = indexDescricao2 - indexDescricao;
                string descricao = trim.Substring(indexDescricao, comprimentoDescricao);
                string descricao2 = descricao.Remove(0, 2);

                int indexPrioridade = trim.LastIndexOf(':');
                string prioridade = trim.Substring(indexPrioridade + 1).Trim();

                //if (prioridade != "0" && prioridade != "1")
                //{
                //    Console.Write("Somente prioridades 0 ou 1 são aceitas...");
                //    Thread.Sleep(4000);
                //    Environment.Exit(0);
                //}

                Console.WriteLine($"Id: {id2}");
                Console.WriteLine($"Tarefa: {tarefa2}");
                Console.WriteLine($"Descrição: {descricao2}");
                Console.WriteLine($"Prioridade: {prioridade}\n");

                Thread.Sleep(7000); // 7 segundos pra simular processamento


                //Confirma o recebimento
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Iniciar o consumo
            channel.BasicConsume(queue: PriorityQueue,
                                 autoAck: false,
                                 consumer: consumer);





            Console.WriteLine("[*] Aguardando mensagens...\n");
            Console.ReadKey();
        }
    }
}