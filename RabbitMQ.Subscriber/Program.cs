using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("\tamqps://qdnmlash:1GBY31rv-Kwtr51ZZVjxeerZGp6X6ZlQ@puffin.rmq2.cloudamqp.com/qdnmlash");
using var connnection = factory.CreateConnection();

var channel = connnection.CreateModel();

//channel.QueueDeclare("hello-queue", true, false, false);

var subscriber = new EventingBasicConsumer(channel);

//autoAck kuyruktan otomatik olarak sil
channel.BasicConsume("hello-queue", true, subscriber);

subscriber.Received += Subscriber_Received;

void Subscriber_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine(message);
}

Console.ReadLine();
