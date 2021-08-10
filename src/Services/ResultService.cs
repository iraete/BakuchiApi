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
    public class ResultService : IResultService
    {
        private readonly BakuchiContext _context;

        public ResultService(BakuchiContext context)
        {
            _context = context;
        }

        public async Task<Result> GetResult(Guid eventId, uint outcomeId)
        {
            return await _context.Results.FindAsync(eventId, outcomeId);
        }

        public async Task<List<Result>> GetResultsByEvent(Guid eventId)
        {
            return await _context.Results.Where(
                r => r.EventId == eventId).ToListAsync();
        }

        public async Task PutResult(Result resultDto)
        {
            _context.Entry(resultDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(resultDto.EventId, resultDto.OutcomeId))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostResult(Result resultDto)
        {
            _context.Results.Add(resultDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResult(Result resultDto)
        {
            _context.Results.Remove(resultDto);
            await _context.SaveChangesAsync();
        }

        public bool ResultExists(Guid eventId, uint outcomeId)
        {
            return _context.Results.Any(r => r.EventId == eventId
                && r.OutcomeId == outcomeId);
        }
    }
}