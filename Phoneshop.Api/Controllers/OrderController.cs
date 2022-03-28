using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(Order order)
        {
            order.Deleted = false;
            await _orderService.CreateAsync(order);
            return Accepted();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var userId = User.FindFirst("id").Value;

            var order = await _orderService.GetByIdAsync(id, userId);
            return Ok(order);
        }

        [HttpGet("GetByUser")]
        public IActionResult GetAllForUser()
        {
            var userId = User.FindFirst("id").Value;

            var orders = _orderService.GetAllByUser(userId);
            return Ok(orders);
        }
    }
}
