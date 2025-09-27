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
        /// <summary>
        /// Create conection with the db
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates and saves a new user in the db
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>The created user</returns>
        public async Task<User> CreateUser(CreateUserDto createUserDto)
        {
            if (string.IsNullOrEmpty(createUserDto.Password) || string.IsNullOrEmpty(createUserDto.Email)
                || string.IsNullOrEmpty(createUserDto.Surename) || string.IsNullOrEmpty(createUserDto.Name))
            {
                throw new ArgumentException("All fields are required");
            }

            // Check if it's a valid email
            string email = createUserDto.Email;
            if (!email.ToLower().Contains('@'))
            {
                throw new Exception("Invalid Email");
            }

            string[] verifyEmail = email.Split('@');
            if (verifyEmail[1] != "perlametro.cl")
            {
                throw new Exception("Invalid Email");
            }
            // Check if email is already registered
            var emailExists = await _context.users.AnyAsync(u => u.Email == createUserDto.Email);
            if (emailExists)
            {
                throw new Exception("Email is already registered");
            }
            // Check if it is a valid password
            if (!PasswordManager.IsValidPassword(createUserDto.Password))
            {
                throw new Exception("Invalid password");
            }

            // make sure it's a new Id
            string newId;
            while (true)
            {
                newId = Guid.NewGuid().ToString();
                var checkUser = await _context.users.FindAsync(newId);
                if (checkUser == null) break;
            }

            // Create the new user  
            var user = new User
            {
                Id = newId,
                Role = "User",
                Name = createUserDto.Name,
                Surename = createUserDto.Surename,
                Email = email,
                Password = PasswordManager.HashPassword(createUserDto.Password),
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                isActive = true
            };

            // Save changes
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Changes the state of a user, from active to deactivated or in reverse
        /// </summary>
        /// <param name="Id"></param>
        public async Task EnableDisableUser(string Id)
        {
            var user = await _context.users.FindAsync(Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.isActive == true)
            {
                user.isActive = false;
                user.DeactivationDates.Add(DateOnly.FromDateTime(DateTime.Now));
            }
            else
            {
                user.isActive = true;
            }
            await _context.SaveChangesAsync();

        }
        /// <summary>
        /// Gets all the users from the db
        /// </summary>
        /// <returns>A list with all the users</returns>
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

            if (query.isActive.HasValue)
            {
                users = users.Where(x => x.isActive == query.isActive.Value);
            }

            return await users.ToListAsync();
        }
        /// <summary>
        /// Can update the name, surename, email and password of the user
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <param name="currentUser"></param>
        /// <returns>The updated user</returns>
        public async Task<User?> UpdateUser(UpdateUserDto updateUserDto, ClaimsPrincipal currentUser)
        {
            var user = await _context.users.FindAsync(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Check if user exists
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update user email
            if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != user.Email)
            {
                // Check if it's a valid email
                string email = updateUserDto.Email;
                if (!email.ToLower().Contains('@'))
                {
                    throw new Exception("Invalid Email");
                }
                string[] verifyEmail = email.Split('@');
                if (verifyEmail[1] != "perlametro.cl")
                {
                    throw new Exception("Invalid Email");
                }

                // Check if email is already registered
                var emailExists = await _context.users
                    .AnyAsync(u => u.Email == updateUserDto.Email && u.Id != ClaimTypes.NameIdentifier);

                if (emailExists)
                {
                    throw new Exception("Email is already registered");
                }

                user.Email = updateUserDto.Email;
            }

            // Check if it is a valid password
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                if (!PasswordManager.IsValidPassword(updateUserDto.Password))
                {
                    throw new Exception("Invalid password");
                }
                user.Password = updateUserDto.Password;
            }

            // Update user name
            if (!string.IsNullOrEmpty(updateUserDto.Name)) { user.Name = updateUserDto.Name; }

            // Update user surename
            if (!string.IsNullOrEmpty(updateUserDto.Surename)) { user.Surename = updateUserDto.Surename; }

            // Save changes
            await _context.SaveChangesAsync();
            return user;
        }
        /// <summary>
        /// Function for the login of the user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>The logged user</returns>
        public async Task<UserLoginResponse?> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _context.users
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.isActive == true);
                
                if (user == null)
                {
                    return new UserLoginResponse 
                    { 
                        IsValid = false, 
                        ErrorMessage = "User not found" 
                    };
                }
                
                // Verify password
                var isPasswordValid = PasswordManager.VerifyPassword(loginDto.Password, user.Password);

                if (!isPasswordValid)
                {
                    return new UserLoginResponse
                    {
                        IsValid = false,
                        ErrorMessage = "Invalid password"
                    };
                }
                
                return new UserLoginResponse
                {
                    IsValid = true,
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Surename = user.Surename,
                    Role = user.Role,
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.GivenName, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surename),
                        new Claim("fullName", $"{user.Name} {user.Surename}".Trim()),
                        new Claim("registrationDate", user.RegistrationDate.ToString("yyyy-MM-dd"))
                    }
                };
            }
            catch (Exception ex)
            {
                return new UserLoginResponse 
                { 
                    IsValid = false, 
                    ErrorMessage = "Validation error occurred" 
                };
            }
        }
    }
}