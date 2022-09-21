using System;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using OrderFunction.IRepositories;

namespace OrderFunction
{
    public class ProcessOrder
    {
        private readonly IOrder _order;
        //private readonly ILogger<ProcessOrder> _logger;

        public ProcessOrder(IOrder order)
        {
           _order = order;
            // _logger = logger;, ILogger<ProcessOrder> logger
        }
        [FunctionName("ProcessOrder")]
        public void Run([QueueTrigger("order-queue", Connection = "StorageConnectionString")]string message, ILogger _logger)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message}");
            var order = JsonSerializer.Deserialize<Order>(message);
            _order.ExecuteOrder(order);



        }
    }
}
