using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakuchiApi.Models;
using BakuchiApi.Models.Validators;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.EntityFrameworkCore;

namespace BakuchiApi.Services
{
    public class EventService : IEventService
    {
        private readonly BakuchiContext _context;
        private readonly EventValidator _validator;

        public EventService(BakuchiContext context)
        {
            _validator = new EventValidator();
            _context = context;
        }

        public async Task<List<Event>> RetrieveEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> RetrieveEvent(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task UpdateEvent(Event eventObj)
        {
            Validate(eventObj);
            _context.Entry(eventObj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(eventObj.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task CreateEvent(Event eventObj)
        {
            eventObj.Id = Guid.NewGuid();
            eventObj.Created = DateTime.Now;
            Validate(eventObj);
            _context.Events.Add(eventObj);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(eventObj.Id))
                {
                    throw new ConflictException();
                }

                throw;
            }
        }

        public async Task DeleteEvent(Event eventObj)
        {
            _context.Events.Remove(eventObj);
            await _context.SaveChangesAsync();
        }

        public bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private void Validate(Event eventObj)
        {
            var validationResult = _validator.Validate(eventObj);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.ToString());
            }
        }
    }
}