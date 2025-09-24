using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.Model
{
    public class User
    {
        public required string Id { get; set; }
        public required string Role { get; set; }
        public required string Name { get; set; }
        public required string Surename { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public bool isActive { get; set; }
        public List<DateOnly> DeactivationDates { get; set; } = new List<DateOnly>();
    }
}