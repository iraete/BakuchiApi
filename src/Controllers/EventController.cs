using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;
        private IUserService _userService;
        private EventDtoMapper _eventMapper;
        private UserDtoMapper _userMapper;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
            _eventMapper = new EventDtoMapper();
            _userMapper = new UserDtoMapper();
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> RetrieveEvents()
        {
            var events = await _eventService.RetrieveEvents();
            return _eventMapper.MapEntitiesToDtos(events);
        }

        // GET: api/Event/?alias=bakuchi&userid=2000&serverid=1000
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> RetrieveEvent(Guid eventId)
        {
            var @event = await _eventService.RetrieveEvent(eventId);

            if (@event == null)
            {
                return NotFound();
            }

            return _eventMapper.MapEntityToDto(@event);
        }

        // PUT: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(
            Guid id, UpdateEventDto eventDto)
        {
            if (id != eventDto.Id 
                || !ValidateDates(eventDto.Start, eventDto.End))
            {
                return BadRequest();
            }

            var @event = _eventMapper.MapUpdateDtoToEntity(eventDto);
            try
            {
                var entity = await _eventService.RetrieveEvent(id);
                entity.Description = @event.Description;
                entity.Start = @event.Start;
                entity.End = @event.End;
                await _eventService.UpdateEvent(entity);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error updating event");
            }

            return NoContent();
        }

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventDto>> 
            CreateEvent(CreateEventDto eventDto)
        {
            if (!ValidateDates(eventDto.Start, eventDto.End))
            {
                return BadRequest();
            }

            if (!_userService.UserExists(eventDto.UserId) ||
                !_userService.DiscordIdExists(eventDto.DiscordId))
            {
                await CreateUser(eventDto);
            }

            var @event = _eventMapper.MapCreateDtoToEntity(eventDto);
            
            try
            {
                await _eventService.CreateEvent(@event);
            }
            catch (status.ConflictException)
            {
                return Conflict();
            }

            return CreatedAtAction("RetrieveEvent", new { id = @event.Id }, 
                _eventMapper.MapEntityToDto(@event));
        }

        // DELETE: api/Event/?alias=bakuchi&userid=2000&serverid=1000
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            var @event = await _eventService.RetrieveEvent(eventId);

            if (@event == null)
            {
                return NotFound();
            }

            await _eventService.DeleteEvent(@event);
            return NoContent();
        }

        private bool ValidateDates(DateTime start, DateTime end)
        {
            return start < end;
        }

        private async Task CreateUser(CreateEventDto eventDto)
        {
            var user = _userMapper.MapCreateDtoToEntity(
                    new CreateUserDto {
                        Name = eventDto.UserName,
                        DiscordId = eventDto.DiscordId
                    }
                );
            await _userService.CreateUser(user);
            eventDto.UserId = user.Id;
        }
    }
}
