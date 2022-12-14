using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderFunction.IRepositories;

namespace OrderFunction.Repositories
{
    public class Order : IOrder
    {
        private readonly ILogger<Order> _logger;
        public Order(ILogger<Order> logger)
        {
            _logger = logger;
        }
       
        public void ExecuteOrder(OrderFunction.Order order)
        {
            if (!(order.Quantity > 100))
            {
                // PROCESS ORDER LOGIC
                _logger.LogInformation($"{order.StockName} assigned to user successfully.");
            }
            else {
                _logger.LogError($"{order.StockName} has quantity greater than 100.");
                throw new Exception("Order failed !!! Quantity greater than 100.");
            }

        }
    }
}
