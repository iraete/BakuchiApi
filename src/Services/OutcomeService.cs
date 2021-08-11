using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BakuchiApi.Models;
using status = BakuchiApi.StatusExceptions;
using BakuchiApi.Services.Interfaces;


namespace BakuchiApi.Services
{
    public class OutcomeService : IOutcomeService
    {
        private readonly BakuchiContext _context;

        public OutcomeService(BakuchiContext context)
        {
            _context = context;
        }

        public async Task<List<Outcome>> RetrieveOutcomesByEvent(Guid eventId)
        {
            return await _context.Outcomes.Where(o => o.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Outcome> RetrieveOutcome(Guid eventId, uint outcomeId)
        {
            return await _context.Outcomes.FindAsync(eventId, outcomeId);
        }

        public async Task UpdateOutcome(Outcome outcome)
        {
            _context.Entry(outcome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutcomeExists(outcome.EventId, outcome.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateOutcome(Outcome outcome)
        {
            _context.Outcomes.Add(outcome);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOutcome(Outcome outcome)
        {
            _context.Outcomes.Remove(outcome);
            await _context.SaveChangesAsync();
        }

        public bool OutcomeExists(Guid eventId, uint outcomeId)
        {
            return _context.Outcomes.Any(o => o.EventId == eventId
                && o.Id == outcomeId);
        }
    }
}