using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;

namespace Phoneshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet("GetPhones")]
        public IActionResult GetPhones(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var phones = _phoneService.GetAll();

                return Ok(phones);
            }
            else
            {
                var phones = _phoneService.Search(query);
                return Ok(phones);
            }
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var phone = _phoneService.Get(id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [HttpPost("Create")]
        public IActionResult Create(Phone phone)
        {
            _phoneService.Create(phone);
            return CreatedAtAction(nameof(Create), new { id = phone.Id }, phone);
        }
    }
}
