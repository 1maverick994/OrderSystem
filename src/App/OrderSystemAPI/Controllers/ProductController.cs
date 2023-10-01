using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Net.Sockets;
using System.Threading.Channels;
using System.Collections.Concurrent;
using System.Text;
using MessageBroker.Abstracts;
using ProductCommon.Commands;
using ProductCommon.Entities;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OrderSystemAPI.Configuration;
using ServiceCommon;

namespace OrderSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IRPCClient _rpcClient;
        private readonly MessageBrokerOptions _options;

        public ProductController(ILogger<ProductController> logger, IRPCClient rpcClient, IOptions<MessageBrokerOptions> options)
        {
            _logger = logger;
            _rpcClient = rpcClient;
            _options = options.Value;

        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<ServiceResult<ProductDto>> Post(CreateProductCommand create)
        {

            var newProduct = await _rpcClient.GetResponse<CreateProductCommand, ProductDto>(_options.HostName, _options.CreateProductQueueName, create);
            return newProduct;

        }

        /// <summary>
        /// Updates existing product
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPatch()]
        public async Task<ServiceResult<ProductDto>> Patch(UpdateProductCommand update)
        {

            var updatedProduct = await _rpcClient.GetResponse<UpdateProductCommand, ProductDto>(_options.HostName, _options.UpdateProductQueueName, update);
            return updatedProduct;

        }

        /// <summary>
        /// Deletes product
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<ServiceResult> Delete(DeleteProductCommand delete)
        {

         return   await _rpcClient.GetResponse<DeleteProductCommand, object>(_options.HostName, _options.DeleteProductQueueName, delete);


        }

        /// <summary>
        /// List products
        /// </summary>
        /// <param name="count">Count of items returned</param>
        /// <param name="page">Page of items (0 based)</param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ServiceResult<ProductDto[]>> List(int count, int page)
        {

            var products = await _rpcClient.GetResponse<ListProductCommand, ProductDto[]>(_options.HostName, _options.ListProductQueueName, new ListProductCommand() { Count = count, Page = page });
            return products;

        }





    }
}