using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUser(RegisterModel registerModel)
        {
            try
            {
                await _userService.Add(registerModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return BadRequest( ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userService.Login(loginModel);

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Unauthorized(ex.Message);
                //return StatusCode(500, ex.Message);
            }
        }

    }
}
