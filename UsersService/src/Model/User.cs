using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.Model
{
    public class User
    {
        /// <summary>
        /// User's Id
        /// </summary>
        public required string Id { get; set; }
        /// <summary>
        /// User's role
        /// </summary>
        public required string Role { get; set; }
        /// <summary>
        /// User's name
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// User's lastname
        /// </summary>
        public required string Surename { get; set; }
        /// <summary>
        /// User's email
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// User's password
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// User's registration date
        /// </summary>
        public DateOnly RegistrationDate { get; set; }
        /// <summary>
        /// User's state
        /// </summary>
        public bool isActive { get; set; }
        /// <summary>
        /// User's dates of deactivation
        /// </summary>
        public List<DateOnly> DeactivationDates { get; set; } = new List<DateOnly>();
    }
}