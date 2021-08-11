using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> RetrieveEvents()
        {
            var mapper = new EventDtoMapper();
            var events = await _service.RetrieveEvents();
            return mapper.MapEntitiesToDtos(events);
        }

        // GET: api/Event/?alias=bakuchi&userid=2000&serverid=1000
        [HttpGet]
        public async Task<ActionResult<EventDto>> RetrieveEvent(Guid eventId)
        {
            var mapper = new EventDtoMapper();
            var @event = await _service.RetrieveEvent(eventId);

            if (@event == null)
            {
                return NotFound();
            }

            return mapper.MapEntityToDto(@event);
        }

        // PUT: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventDto dto)
        {
            var mapper = new EventDtoMapper();
            var @event = mapper.MapDtoToEntity(dto);
            try
            {
                await _service.UpdateEvent(@event);
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
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto eventDto)
        {
            var @event = new Event
            {
                Name = eventDto.Name,
                Alias = eventDto.Alias,
                UserId = eventDto.UserId,
                ServerId = eventDto.ServerId,
                Description = eventDto.Description,
                Start = eventDto.Start,
                End = eventDto.End
            };
            
            try
            {
                await _service.CreateEvent(@event);
            }
            catch (status.ConflictException)
            {
                return Conflict();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Error adding event.");
            }

            return CreatedAtAction("RetrieveEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Event/?alias=bakuchi&userid=2000&serverid=1000
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            var @event = await _service.RetrieveEvent(eventId);

            if (@event == null)
            {
                return NotFound();
            }

            await _service.DeleteEvent(@event);
            return NoContent();
        }

    }
}
