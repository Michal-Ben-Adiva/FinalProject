using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MODELS.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


public struct Login
{
    public long id { get; set; }
    public string password { get; set; }

    public Login(long Id, string Password)
    {
        id = Id;
        password = Password;
    }
}
public struct Register
{
    public long id { get; set; }
    public string password { get; set; }
    public string name { get; set; }

    public Register(long Id, string Password, string Name)
    {
        id = Id;
        password = Password;
        name = Name;
    }
}
namespace FinalProject.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILogger<LogInController> _logger;
        private IConfiguration _config;
        private readonly IUsers _user;


        public LogInController(ILogger<LogInController> logger, IConfiguration config, IUsers user)
        {
            _logger = logger;
            _config = config;
            _user = user;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel loginRequest)
        {
            var userFind = await _user.GetUser(loginRequest.id);


            if (userFind != null)
            {
                var tokenString = GenerateJwtToken(userFind);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid credentials.");

        }


        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]); // Ensure UTF8 encoding
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
                                    
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
   
    }

    public class LoginModel
    {
        public long id { get; set; }
        public string password { get; set; }
    }

    public class RegisterModel
    {
        public long id { get; set; }
        public string password { get; set; }
    }

}

