using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ILogIn
    {
        Task<Users> ValidateUserAsync(string id, string password);
        string GenerateJwtToken(Users user);
    }
}
