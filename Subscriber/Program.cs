using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection.Metadata;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();
channel.QueueDeclare("orders", exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message: {message}");
};

channel.BasicConsume("orders", true, consumer);

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
Console.ReadKey();
