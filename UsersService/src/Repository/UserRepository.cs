using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public Task EnableDisableUser(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
             return await _context.users.ToListAsync();
        }

        public async Task<User?> GetUser(string Id)
        {
            return await _context.users.FindAsync(Id);
        }

        public async Task<List<User>> GetUsers(QueryObject query)
        {
            var users = _context.users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                users = users.Where(u => 
                    EF.Functions.ILike(u.Name, $"%{query.Name}%") || 
                    EF.Functions.ILike(u.Surename, $"%{query.Name}%"));
            }

            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                users = users.Where(x => x.Email.Contains(query.Email));  
            }
            
            if (query.State.HasValue)
            {
                users = users.Where(x => x.State == query.State.Value);  
            }

            return await users.ToListAsync();
        }

        public Task<User> UpdateUser(UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}