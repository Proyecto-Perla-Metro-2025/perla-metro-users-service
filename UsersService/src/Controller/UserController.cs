using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersService.src.Data;
using UsersService.src.Interface;
using UsersService.src.Mapper;

namespace UsersService.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            var usersDtos = users.ToDtoEnumerable();
            return Ok(usersDtos);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string Id)
        {
            var user = await _userRepository.GetUser(Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToVisualizeUserDtoFromUser());
        }
    } 
}