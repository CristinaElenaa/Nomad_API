using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Implementations;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;
using Nomad.DataAccess.Entities;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public UserController(IUserService userService, IPhotoService photoService)
        {
            _userService = userService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            try
            {
                var data = await _userService.GetAllUsers();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("id")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            try
            {
                var data = await _userService.GetUserById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserModel updateUserModel)
        {
            try
            {
                await _userService.UpdateUser(updateUserModel);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult> AddPhoto(IFormFile file, int userId)
        {
            try
            {
                var photo = await _photoService.AddProfilePhoto(file, userId);
                return CreatedAtAction(nameof(GetUserById), new {id = userId}, photo );

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveUser(int userId)
        {
            try
            {
                await _userService.RemoveUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
