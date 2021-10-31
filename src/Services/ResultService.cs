using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BakuchiApi.Models;
using BakuchiApi.Models.Validators;
using BakuchiApi.StatusExceptions;
using BakuchiApi.Services.Interfaces;

namespace BakuchiApi.Services
{
    public class ResultService : IResultService
    {
        private ResultValidator _validator;
        private readonly BakuchiContext _context;

        public ResultService(BakuchiContext context)
        {
            _validator = new ResultValidator();
            _context = context;
        }

        public async Task<Result> RetrieveResult(Guid eventId, uint outcomeId)
        {
            return await _context.Results.FindAsync(eventId, outcomeId);
        }

        public async Task<List<Result>> RetrieveResultsByEvent(Guid eventId)
        {
            return await _context.Results.Where(
                r => r.EventId == eventId).ToListAsync();
        }

        public async Task UpdateResult(Result result)
        {
            Validate(result);
            result.LastEdited = DateTime.Now;
            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(result.EventId, result.OutcomeId))
                {
                    throw new NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateResult(Result result)
        {
            Validate(result);
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResult(Result result)
        {
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
        }

        public bool ResultExists(Guid eventId, uint outcomeId)
        {
            return _context.Results.Any(r => r.EventId == eventId
                && r.OutcomeId == outcomeId);
        }

        private void Validate(Result resultObj)
        {
            var validationResult = _validator.Validate(resultObj);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.ToString());
            }
        }
    }
}