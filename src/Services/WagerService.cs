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
    public class WagerService : IWagerService
    {
        private readonly BakuchiContext _context;

        public WagerService(BakuchiContext context)
        {
            _context = context;
        }

        public bool WagerExists(Guid userId, Guid eventPoolId)
        {
            return _context.Wagers.Any(uw => uw.UserId == userId
                && uw.PoolId == eventPoolId);
        }

        public async Task<List<Wager>> GetWagers(Guid userId)
        {
            return await _context.Wagers.Where(
                uw => uw.UserId == userId).ToListAsync();
        }

        public async Task<Wager> GetWager(Guid userId, Guid eventPoolId)
        {
            return await _context.Wagers.FindAsync(userId, eventPoolId);
        }

        public async Task PutWager(Wager userWager)
        {
            _context.Entry(userWager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagerExists(userWager.UserId, userWager.PoolId))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostWager(Wager userWager)
        {
            _context.Wagers.Add(userWager);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WagerExists(userWager.UserId, userWager.PoolId))
                {
                    throw new status.ConflictException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteWager(Wager userWager)
        {
            _context.Wagers.Remove(userWager);
            await _context.SaveChangesAsync();
        }
    }
}