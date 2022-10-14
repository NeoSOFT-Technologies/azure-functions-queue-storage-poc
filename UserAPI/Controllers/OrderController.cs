using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly QueueClient _client;

        public OrderController(QueueClient client, ILogger<OrderController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> Create(Order order)
        {
            _logger.LogInformation($"PlaceOrder Action executed in controller for user:- {order.UserId}");
            await _client.SendMessageAsync(JsonSerializer.Serialize(order));
            return Ok("Order Initiated");
        }
    }
}
