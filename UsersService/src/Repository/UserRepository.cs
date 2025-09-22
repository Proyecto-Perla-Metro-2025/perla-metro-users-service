using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersService.src.Data;
using UsersService.src.DTOs;
using UsersService.src.Helper;
using UsersService.src.Interface;
using UsersService.src.Model;

namespace UsersService.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public Task<User> CreateUser(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task EnableDisableUser(bool enable)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
             return await _context.users.ToListAsync();
        }

        public Task<User> GetUserAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsers(QueryObject query)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}