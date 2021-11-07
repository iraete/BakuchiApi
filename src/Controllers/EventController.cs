using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> RetrieveEvents()
        { 
            return await _eventService.RetrieveEvents();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> RetrieveEvent(Guid eventId)
        {
            var @event = await _eventService.RetrieveEvent(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventDto eventDto)
        {
            if (id != eventDto.Id || !ValidateDates(eventDto.Start, eventDto.End))
                throw new BadRequestException();

            await _eventService.UpdateEvent(eventDto);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto eventDto)
        {
            if (!ValidateDates(eventDto.Start, eventDto.End))
                throw new BadRequestException();

            var resultId = await _eventService.CreateEvent(eventDto);

            return CreatedAtAction("RetrieveEvent", new {id = resultId});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            var @event = await _eventService.RetrieveEvent(eventId);

            if (@event == null)
            {
                throw new NotFoundException();
            }

            await _eventService.DeleteEvent(@eventId);
            return NoContent();
        }

        private bool ValidateDates(DateTime start, DateTime end)
        {
            return start < end;
        }
    }
}