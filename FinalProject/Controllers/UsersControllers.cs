using DAL.DTO;
using DAL.Interfaces;
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool delete = await _dbuser.DeleteUser(id);
            if (delete)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("user/{id}" , Name = "GetUser")]
        public async Task<Users> GetUser(long id)
        {
            Users user = await _dbuser.GetUser(id);
            return user;
        }

        [HttpGet("AllUsers/{id}", Name = "GetAllUsers")]
        public async Task<IEnumerable<Users>> GetAllUsers(long id)
        {
            var users = await _dbuser.GetAllUsers(id);
            return users;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UsersDTO value)
        {
            bool update = await _dbuser.UpdateUser(id, value);
            if (update)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
