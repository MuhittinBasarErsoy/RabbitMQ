using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("\tamqps://qdnmlash:1GBY31rv-Kwtr51ZZVjxeerZGp6X6ZlQ@puffin.rmq2.cloudamqp.com/qdnmlash");
using var connnection = factory.CreateConnection();

var channel = connnection.CreateModel();

//QueueDeclare Prametreleri
//queue     -->kuyruğun adı
//durable   -->kuyruklar memoryde tutuluyor. true dersek hdd de.
//exclusive -->sadece suan olusturulan channel mı ulaşabilir
//autodelete-->subscriber koptugunda kuyrugu sil
channel.QueueDeclare("hello-queue",true,false,false);

string message = "hello world";

var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);

Console.WriteLine("Mesaj Gönderildi");

Console.ReadLine(); 