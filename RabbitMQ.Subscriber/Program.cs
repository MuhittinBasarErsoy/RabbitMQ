using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("\tamqps://qdnmlash:1GBY31rv-Kwtr51ZZVjxeerZGp6X6ZlQ@puffin.rmq2.cloudamqp.com/qdnmlash");
using var connnection = factory.CreateConnection();

var channel = connnection.CreateModel();

//channel.QueueDeclare("hello-queue", true, false, false);

//prefetchsize ->Kac sizelık alacagı
//count ->Kacar tane alacagı
//global ->paylasım
channel.BasicQos(0, 1, false);
var subscriber = new EventingBasicConsumer(channel);

//autoAck kuyruktan otomatik olarak sil
channel.BasicConsume("hello-queue", false, subscriber);

subscriber.Received += Subscriber_Received;

void Subscriber_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1000);
    Console.WriteLine(message);

    channel.BasicAck(e.DeliveryTag, false);
}

Console.ReadLine();
