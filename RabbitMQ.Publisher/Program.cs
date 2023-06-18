using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("\tamqps://qdnmlash:1GBY31rv-Kwtr51ZZVjxeerZGp6X6ZlQ@puffin.rmq2.cloudamqp.com/qdnmlash");
using var connnection = factory.CreateConnection();

var channel = connnection.CreateModel();

channel.ExchangeDeclare("logs-fanout",durable:true,type:ExchangeType.Fanout);

for (int i = 1; i <= 50; i++)
{

    string message = $"log {i}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("logs-fanout","", null, messageBody);

    Console.WriteLine($"Mesaj Gönderildi : {message}");
}


Console.ReadLine(); 