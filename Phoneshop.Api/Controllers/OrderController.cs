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
            await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(CreateAsync), new { id = order.Id }, order);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("Get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var userId = User.FindFirst("UserId").Value;

            var order = await _orderService.GetByIdAsync(id, userId);
            return Ok(order);
        }

        [HttpGet("Get/{userId}")]
        public IActionResult GetAllForUserAsync()
        {
            var userId = User.FindFirst("UserId").Value;

            var orders = _orderService.GetAllByUser(userId);
            return Ok(orders);
        }
    }
}
