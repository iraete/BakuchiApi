using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserDtoMapper _mapper;
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
            _mapper = new UserDtoMapper();
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> RetrieveUsers()
        {
            var users = await _service.RetrieveUsers();
            return _mapper.MapEntitiesToDtos(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> RetrieveUser(Guid id)
        {
            var user = await _service.RetrieveUser(id);
            CheckIfUserExists(user);
            return _mapper.MapEntityToDto(user);
        }

        // GET: api/User/5
        [HttpGet("discord/{discordId}")]
        public async Task<ActionResult<UserDto>> 
            RetrieveUserByDiscordId(long discordId)
        {
            var user = await _service.RetrieveUserByDiscordId(discordId);
            CheckIfUserExists(user);
            return _mapper.MapEntityToDto(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto userDto)
        {
            if (id != userDto.Id)
            {
                throw new BadRequestException();
            }

            var user = _mapper.MapUpdateDtoToEntity(userDto);
            var entity = await _service.RetrieveUser(id);

            CheckIfUserExists(entity);

            entity.Name = user.Name;
            entity.DiscordId = user.DiscordId;
            entity.Email = user.Email;

            await _service.UpdateUser(entity);

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.MapCreateDtoToEntity(userDto);
            await _service.CreateUser(user);
            return CreatedAtAction("RetrieveUser", new { id = user.Id },
                 _mapper.MapEntityToDto(user));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _service.RetrieveUser(id);
            CheckIfUserExists(user);
            await _service.DeleteUser(user);
            return NoContent();
        }

        // GET: api/User/5/events
        [HttpGet("{id}/events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> RetrieveUserEvents(Guid id)
        {
            var eventmapper = new EventDtoMapper();
            var user = await _service.RetrieveUser(id);
            CheckIfUserExists(user);
            var events =  _service.RetrieveEvents(user);
            return eventmapper.MapEntitiesToDtos(events);
        }

        // GET: api/User/5/userwagers
        [HttpGet("{id}/wagers")]
        public async Task<ActionResult<IEnumerable<WagerDto>>> RetrieveWagers(Guid id)
        {
            var wagermapper = new WagerDtoMapper();
            var user = await _service.RetrieveUser(id);
            CheckIfUserExists(user);
            var wagers = _service.RetrieveWagers(user);
            return wagermapper.MapEntitiesToDtos(wagers);
        }

        private void CheckIfUserExists(User user)
        {
            if (user == null)
            {
                throw new NotFoundException();
            }
        }
    }
}
