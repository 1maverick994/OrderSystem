using System.Text;
using MediatR;
using MessageBroker.Abstracts;
using Microsoft.Extensions.Options;
using ProductCommon.Commands;
using ProductCommon.Entities;
using ProductService.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.Workers;

public class CreateProductWorker : BackgroundService
{
    private readonly ILogger<CreateProductWorker> _logger;


    public CreateProductWorker(ILogger<CreateProductWorker> logger, IOptions<MessageBrokerOptions> options, IRPCServer rpcServer, ISender sender)
    {
        _logger = logger;

        rpcServer.Start<CreateProductCommand, ProductDto>(options.Value.HostName, options.Value.CreateProductQueueName, async (cp) =>
        {

            return await sender.Send(cp);

        });



    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            await Task.Delay(1000, stoppingToken);
        }
    }
}
