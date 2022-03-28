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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("Get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(Brand brand)
        {
            await _brandService.CreateAsync(brand);
            return Accepted();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _brandService.DeleteAsync(id);
            return Ok();
        }
    }
}
