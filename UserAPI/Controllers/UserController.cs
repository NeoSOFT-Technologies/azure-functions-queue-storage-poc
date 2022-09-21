using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly QueueClient _client;

        public UserController(QueueClient client, ILogger<UserController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> Authenticate(User user)
        {
            _logger.LogInformation($"Authenticate Action executed in controller for user:- {user}");
            await _client.SendMessageAsync(JsonSerializer.Serialize(user));
            return Ok("User authenticating started");
        }
    }
}
