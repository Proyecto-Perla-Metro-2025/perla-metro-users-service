using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UsersService.src.DTOs
{
    public class UserLoginResponse
    {
        public bool IsValid { get; set; }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surename { get; set; }
        public string? Role { get; set; }
        public List<Claim> Claims { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }
}