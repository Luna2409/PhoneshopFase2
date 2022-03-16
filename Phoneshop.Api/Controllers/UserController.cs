using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Api.Models;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserModel userModel)
        {
            var user = new IdentityUser 
            { 
                UserName = userModel.UserName, 
                Email = userModel.Email 
            };

            try
            {
                var result = await _userManager.CreateAsync(user, userModel.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, userModel.RememberMe, false);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id)
            };

            var token = _tokenService.Generate(claims);
            return Ok(new
            {
                succes = true,
                token,
                role = "Guest"
            });
        }
    }
}
