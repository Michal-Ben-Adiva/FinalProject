using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;
namespace FinalProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly IUsers _dbuser;
        public UsersControllers(IUsers user)
        {
            _dbuser = user;
        }
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsersDTO value)
        {
            bool create = await _dbuser.CreateUser(value);
            if (create)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            bool delete = await _dbuser.DeleteUser(userId);
            if (delete)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet("user/{userId}", Name = "GetUser")]
        public async Task<Users> GetUser(string userId)
        {
            Users user = await _dbuser.GetUser(userId);
            return user;
        }
        [Authorize]
        [HttpGet("AllUsers/{userId}", Name = "GetAllUsers")]
        public async Task<IEnumerable<Users>> GetAllUsers(string userId)
        {
            var users = await _dbuser.GetAllUsers(userId);
            return users;
        }
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId, [FromBody] UsersDTO value)
        {
            bool update = await _dbuser.UpdateUser(userId, value);
            if (update)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
