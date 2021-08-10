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

        public async Task<List<Pool>> GetPools()
        {
            return await _context.Pools.ToListAsync();
        }

        public async Task<Pool> GetPool(Guid id)
        {
            return await _context.Pools.FindAsync(id);
        }

        public async Task<List<Pool>> GetPoolsByEvent(Guid eventId)
        {
            return await _context.Pools.Where(
                p => p.EventId == eventId).ToListAsync();
        }

        public async Task PutPool(Pool poolDto)
        {
            _context.Entry(poolDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExists(poolDto.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostPool(Pool poolDto)
        {
            _context.Pools.Add(poolDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePool(Pool poolDto)
        {
            _context.Pools.Remove(poolDto);
            await _context.SaveChangesAsync();
        }

        public bool PoolExists(Guid id)
        {
            return _context.Pools.Any(e => e.Id == id);
        }
    }
}