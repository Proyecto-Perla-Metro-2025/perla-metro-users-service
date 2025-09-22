using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task EnableDisableUser(string Id)
        {
            var user = await _context.users.FindAsync(Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.State == true)
            {
                user.State = false;
            }
            else
            {
                user.State = true;
            }
            await _context.SaveChangesAsync();

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

        public async Task<User?> UpdateUser(UpdateUserDto updateUserDto, ClaimsPrincipal currentUser)
        {
            var user = await _context.users.FindAsync(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Actualizar la información del perfil
            user.Name = updateUserDto.Name;
            user.Surename = updateUserDto.Surename;
            user.Email = updateUserDto.Email;


            await _context.SaveChangesAsync();
            return user;
        }
    }
}