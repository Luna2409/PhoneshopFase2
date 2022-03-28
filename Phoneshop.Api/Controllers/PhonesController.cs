using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet("GetPhones")]
        public async Task<IActionResult> GetAllAsync(string query)
        {
            return await Task.Run(() =>
            {
                // statement ? true : false
                var phones = string.IsNullOrEmpty(query) ? _phoneService.GetAll() : _phoneService.Search(query);
                return Ok(phones);
            });
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var phone = await _phoneService.GetByIdAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(Phone phone)
        {
            await _phoneService.CreateAsync(phone);
            return Accepted();
        }
    }
}
