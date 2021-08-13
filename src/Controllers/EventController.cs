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
        private EventDtoMapper _eventMapper;

        public EventController(IEventService service)
        {
            _service = service;
            _eventMapper = new EventDtoMapper();
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> RetrieveEvents()
        {
            var events = await _service.RetrieveEvents();
            return _eventMapper.MapEntitiesToDtos(events);
        }

        // GET: api/Event/?alias=bakuchi&userid=2000&serverid=1000
        [HttpGet]
        public async Task<ActionResult<EventDto>> RetrieveEvent(Guid eventId)
        {
            var @event = await _service.RetrieveEvent(eventId);

            if (@event == null)
            {
                return NotFound();
            }

            return _eventMapper.MapEntityToDto(@event);
        }

        // PUT: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto eventDto)
        {
            var @event = _eventMapper.MapUpdateDtoToEntity(eventDto);
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
        public async Task<ActionResult<EventDto>> 
            CreateEvent(CreateEventDto eventDto)
        {
            var @event = _eventMapper.MapCreateDtoToEntity(eventDto);
            
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

            return CreatedAtAction("RetrieveEvent", new { id = @event.Id }, 
                _eventMapper.MapEntityToDto(@event));
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
