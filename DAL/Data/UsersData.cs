using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UsersData : IUsers
    {
        private readonly ModelsContext _Context;
        private readonly IMapper _mapper;
        public UsersData(ModelsContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateUser(UsersDTO u)
        {
            await _Context.users.AddAsync(_mapper.Map<Users>(u));
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUser(long id)
        {
            Users u = await _Context.users.FindAsync(id);
            if (u == null)
            {
                return false;
            }
            _Context.users.Remove(u);
            await _Context.SaveChangesAsync();
            return true;
        }
        public async Task<Users> GetUser(long id)
        {
            Users u = await _Context.users.FindAsync(id);
            if (u == null)
            {
                return null;
            }
            return u;
        }
        public async Task<bool> UpdateUser(long id, UsersDTO updateuser)
        {
            Users currentuser = await _Context.users.FindAsync(id);
            if (currentuser == null)
            {
                return false;
            }
            currentuser.id = updateuser.userId; ;
            currentuser.userId = id; 
            currentuser.password = updateuser.password;
            currentuser.lastName = updateuser.lastName;
            currentuser.firstName = updateuser.firstName;
            await _Context.SaveChangesAsync();
            return true;
        }

    }
}
