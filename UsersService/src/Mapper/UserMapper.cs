using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.src.DTOs;
using UsersService.src.Model;

namespace UsersService.src.Mapper
{
    public static class UserMapper
    {
        public static VisualizeUserDto ToVisualizeUserDtoFromUser(this User user)
        {
            return new VisualizeUserDto
            {
                Id = user.Id,
                Fullname = $"{user.Name} {user.Surename}",
                Email = user.Email,
                State = user.State,
                RegistrationDate = user.RegistrationDate
            };
        }
        public static IEnumerable<VisualizeUserDto> ToDtoEnumerable(this IEnumerable<User> users)
        {
            return users.Select(user => user.ToVisualizeUserDtoFromUser());
        }
    }
}