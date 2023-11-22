using Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IUserRegistrationUseCase userRegistration;
        public UserController(IUserRegistrationUseCase userRegistration)
        {
            this.userRegistration = userRegistration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel user)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }

                var result = await userRegistration.RegisterUserAsync(user);

                if (result.IsSuccess)
                {
                    return Ok();
                }

                return BadRequest(new { Error = result.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }   
                var loggedin = await userRegistration.LoginUserAsync(user);

                if(loggedin.IsSuccess)
                {
                    return Ok(loggedin.Message);
                }
                else
                {
                    return BadRequest(loggedin.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
