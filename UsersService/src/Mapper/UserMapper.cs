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
        /// <summary>
        /// Method that turns a User into a VisualizeUserDto
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The VisualizeUserDto</returns>
        public static VisualizeUserDto ToVisualizeUserDtoFromUser(this User user)
        {
            return new VisualizeUserDto
            {
                Id = user.Id,
                Fullname = $"{user.Name} {user.Surename}",
                Email = user.Email,
                isActive = user.isActive,
                RegistrationDate = user.RegistrationDate
            };
        }
        /// <summary>
        /// Method that lets a list of Users be turned into a list of VisualizeUserDtos
        /// </summary>
        /// <param name="users"></param>
        /// <returns>The list of VisualizeUserDto</returns>
        public static IEnumerable<VisualizeUserDto> ToDtoEnumerable(this IEnumerable<User> users)
        {
            return users.Select(user => user.ToVisualizeUserDtoFromUser());
        }
    }
}