using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersService.src.DTOs;
using UsersService.src.Helper;
using UsersService.src.Model;

namespace UsersService.src.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateUser(CreateUserDto createUserDto);
        Task<List<User>> GetAll();
        Task<User?> GetUser(string Id);
        Task<User?> UpdateUser(UpdateUserDto updateUserDto, ClaimsPrincipal currentUser);
        Task EnableDisableUser(string Id);
        Task<List<User>> GetUsers(QueryObject query);
        Task<User?> Login(LoginDto loginDto);
    }
}