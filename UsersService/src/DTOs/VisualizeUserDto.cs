using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.DTOs
{
    public class VisualizeUserDto
    {
        public required string Id { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public bool isActive { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}