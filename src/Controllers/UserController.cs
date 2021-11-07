using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> RetrieveUsers()
        {
            return await _service.RetrieveUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> RetrieveUser(long id)
        {
            var user = await _service.RetrieveUser(id);
            if (user == null)
            {
                throw new BadRequestException();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(long id, UpdateUserInfoDto userDto)
        {
            if (id != userDto.Id)
            {
                throw new BadRequestException();
            }

            await _service.UpdateUserInfo(userDto);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
        {
            await _service.CreateUser(userDto);
            return CreatedAtAction("CreateUser", new {id = userDto.Id});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _service.DeleteUser(id);
            return NoContent();
        }
    }
}