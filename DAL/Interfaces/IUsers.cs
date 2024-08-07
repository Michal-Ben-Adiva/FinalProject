using DAL.DTO;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsers
    {
        Task<bool> CreateUser(UsersDTO u);
        Task<bool> DeleteUser(string id);
        Task<Users> GetUser(string id);
        Task<bool> UpdateUser(string id, UsersDTO updateuser);
        Task<IEnumerable<Users>> GetAllUsers(string id);
    }
}
