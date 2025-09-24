using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersService.src.Data;
using UsersService.src.DTOs;
using UsersService.src.Helper;
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
            return Ok(users);
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

        [HttpGet("UserFilter")]
        public async Task<IActionResult> GetUsers([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userRepository.GetUsers(query);
            var usersDto = users.Select(u => u.ToVisualizeUserDtoFromUser());
            return Ok(usersDto);
        }
        [HttpPut("enable-disable/{id}")]
        public async Task<IActionResult> EnableDisableUser([FromRoute] string Id)
        {
            await _userRepository.EnableDisableUser(Id);
            return Ok();
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userRepository.UpdateUser(userDto, User);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newUser = await _userRepository.CreateUser(createUserDto);

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userRepository.Login(loginDto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    } 
}