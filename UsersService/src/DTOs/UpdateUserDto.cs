using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.DTOs
{
    public class UpdateUserDto
    {
        public required string Name { get; set; }
        public required string Surename { get; set; }
        public required string Email { get; set; }
        
    }
}