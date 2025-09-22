using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.src.Helper
{
    public class QueryObject
    {
        public string? Name { get; set; } = string.Empty;
        public string? SortByEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}