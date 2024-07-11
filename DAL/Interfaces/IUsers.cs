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
        Task<bool> DeleteUser(long id);
        Task<Users> GetUser(long id);
        Task<bool> UpdateUser(long id, UsersDTO updateuser);
    }
}
