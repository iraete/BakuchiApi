using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using status = BakuchiApi.StatusExceptions;
using BakuchiApi.Services.Interfaces;


namespace BakuchiApi.Services
{
    public class EventService : IEventService
    {
        private readonly BakuchiContext _context;

        public EventService(BakuchiContext context)
        {
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
            _context.Entry(eventObj).State = EntityState.Modified;

            try
            {                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(eventObj.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task CreateEvent(Event eventObj)
        {
            eventObj.Id = Guid.NewGuid();
            eventObj.Created = DateTime.Now;
            _context.Events.Add(eventObj);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(eventObj.Id))
                {
                    throw new status.ConflictException();
                }
                else
                {
                    throw;
                }
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
    }
}