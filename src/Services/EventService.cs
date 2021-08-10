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

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task PutEvent(Event eventDto)
        {
            _context.Entry(eventDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(eventDto.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task PostEvent(Event eventDto)
        {
            _context.Events.Add(eventDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(eventDto.Id))
                {
                    throw new status.ConflictException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteEvent(Event eventDto)
        {
            _context.Events.Remove(eventDto);
            await _context.SaveChangesAsync();
        }

        public bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}