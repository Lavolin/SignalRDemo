using CoffeeDemo.Hubs;
using CoffeeDemo.Models;
using CoffeeDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeDemo.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly OrderService orderService;

        private readonly IHubContext<CoffeeHub> coffeeHub;

        public CoffeeController(
            OrderService orderService, 
            IHubContext<CoffeeHub> coffeeHub)
        {
            this.orderService = orderService;
            this.coffeeHub = coffeeHub;
        }

        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody] Order order)
        {
            await this.coffeeHub.Clients.All.SendAsync("NewOrder", order);

            var orderId = this.orderService.NewOrder();

            return Accepted(orderId);
        }
    }
}
