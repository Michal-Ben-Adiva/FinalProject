using BL.Interfaces; // Use the namespace where ILogIn is defined
using BL.Services;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MODELS.Models;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILogger<LogInController> _logger;
        private readonly ILogIn _logInService;

        public LogInController(ILogger<LogInController> logger, ILogIn logInService)
        {
            _logger = logger;
            _logInService = logInService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel loginRequest)
        {
            var userFind = await _logInService.ValidateUserAsync(loginRequest.userId, loginRequest.password);

            if (userFind != null)
            {
                var tokenString = _logInService.GenerateJwtToken(userFind);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid credentials.");
        }
    }

    public class LoginModel
    {
        public string userId { get; set; }
        public string password { get; set; }
    }

    public class RegisterModel
    {
        public string userId { get; set; }
        public string password { get; set; }
    }
}
