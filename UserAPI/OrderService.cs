using Azure.Storage.Queues;
using System.Text.Json;
using UserAPI.Models;

namespace UserAPI
{
    public class OrderService : BackgroundService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly QueueClient _queueClient;

        public OrderService(QueueClient queueClient, ILogger<OrderService> logger)
        {
            _logger = logger;
            _queueClient = queueClient;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("reading queue");

                await _queueClient.CreateIfNotExistsAsync();
                //var queueMessage = await _queueClient.PeekMessageAsync();    // message won't be invisible after getting read
                var queueMessage = await _queueClient.ReceiveMessageAsync();   // message will be invisible after getting read


                if (queueMessage.Value != null)
                {
                    _logger.LogInformation("QUEUE Body" + queueMessage.Value.Body.ToString());
                    var orderData = JsonSerializer.Deserialize<Order>(queueMessage.Value.MessageText);


                    try
                    {
                        if (orderData.Quantity > 100)
                        {
                            throw new Exception("quantity greater than 100");
                        }
                    
                        else
                        {
                        _logger.LogInformation($"stock name -> {orderData.StockName}, and quantity -> {orderData.Quantity} assigned to user with userId -> {orderData.UserId} successfully.");
                        await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);      // we need to delete message once message is processed.
                        _logger.LogInformation("Message dequed");
                         }

                    }
                    catch (Exception ex)
                    {
                    _logger.LogError("quantity greater than 100 and message will be pushed to poison queue", ex);
                    }



            }
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
}
