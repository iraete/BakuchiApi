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
    public class OutcomeService : IOutcomeService
    {
        private readonly BakuchiContext _context;
        private readonly OutcomeValidator _validator;

        public OutcomeService(BakuchiContext context)
        {
            _validator = new OutcomeValidator();
            _context = context;
        }

        public async Task<List<Outcome>> RetrieveOutcomesByEvent(Guid eventId)
        {
            return await _context.Outcomes.Where(o => o.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Outcome> RetrieveOutcome(Guid eventId, string alias)
        {
            return await _context.Outcomes.FindAsync(eventId, alias);
        }

        public async Task UpdateOutcome(Outcome outcome)
        {
            Validate(outcome);
            _context.Entry(outcome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutcomeExists(outcome.EventId, outcome.Alias))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task CreateOutcome(Outcome outcome)
        {
            Validate(outcome);
            _context.Outcomes.Add(outcome);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOutcome(Outcome outcome)
        {
            _context.Outcomes.Remove(outcome);
            await _context.SaveChangesAsync();
        }

        public bool OutcomeExists(Guid eventId, string alias)
        {
            return _context.Outcomes.Any(o => o.EventId == eventId
                                              && o.Alias == alias);
        }

        private void Validate(Outcome outcomeObj)
        {
            var validationResult = _validator.Validate(outcomeObj);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.ToString());
            }
        }
    }
}