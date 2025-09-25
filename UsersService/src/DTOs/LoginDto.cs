using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.DTOs
{
    public class LoginDto
    {
        /// <summary>
        /// User's email
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// User's registration date
        /// </summary>
        public required string Password { get; set; }
    }
}
