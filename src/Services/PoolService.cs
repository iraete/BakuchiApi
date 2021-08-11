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
    public class PoolService : IPoolService
    {
        private readonly BakuchiContext _context;

        public PoolService(BakuchiContext context)
        {
            _context = context;
        }

        public async Task<List<Pool>> RetrievePools()
        {
            return await _context.Pools.ToListAsync();
        }

        public async Task<Pool> RetrievePool(Guid id)
        {
            return await _context.Pools.FindAsync(id);
        }

        public async Task<List<Pool>> RetrievePoolsByEvent(Guid eventId)
        {
            return await _context.Pools.Where(
                p => p.EventId == eventId).ToListAsync();
        }

        public async Task UpdatePool(Pool pool)
        {
            _context.Entry(pool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExists(pool.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreatePool(Pool pool)
        {
            _context.Pools.Add(pool);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePool(Pool pool)
        {
            _context.Pools.Remove(pool);
            await _context.SaveChangesAsync();
        }

        public bool PoolExists(Guid id)
        {
            return _context.Pools.Any(e => e.Id == id);
        }
    }
}