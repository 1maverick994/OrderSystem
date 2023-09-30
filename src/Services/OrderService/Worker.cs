
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderService;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "rpc_order_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        var consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume(queue: "rpc_order_queue",
                             autoAck: false,
                             consumer: consumer);
        Console.WriteLine(" [x] Awaiting RPC requests");

        consumer.Received += (model, ea) =>
        {
            string response = string.Empty;

            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                response = "Henlo " + message + " by order service.";
            }
            catch (Exception e)
            {
                Console.WriteLine($" [.] {e.Message}");
                response = string.Empty;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: props.ReplyTo,
                                     basicProperties: replyProps,
                                     body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
