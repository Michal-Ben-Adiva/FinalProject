using DAL.Interfaces;
using MODELS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;

namespace BL.Services
{
    public class LogIn : ILogIn
    {
        private readonly IUsers _user;
        private readonly IConfiguration _config;

        public LogIn(IUsers user, IConfiguration config)
        {
            _user = user;
            _config = config;
        }

        public async Task<Users> ValidateUserAsync(string id, string password)
        {
            var user = await _user.GetUser(id);
            if (user != null && user.password == password) 
            {
                return user;
            }
            return null;
        }

        public string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userId.ToString())
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
}

