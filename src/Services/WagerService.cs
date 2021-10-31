using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Models.Validators;
using BakuchiApi.StatusExceptions;
using BakuchiApi.Services.Interfaces;


namespace BakuchiApi.Services
{
    public class WagerService : IWagerService
    {
        private WagerValidator _validator;
        private readonly BakuchiContext _context;

        public WagerService(BakuchiContext context)
        {
            _validator = new WagerValidator();
            _context = context;
        }

        public bool WagerExists(Guid userId, Guid eventPoolId)
        {
            return _context.Wagers.Any(uw => uw.UserId == userId
                && uw.PoolId == eventPoolId);
        }

        public async Task<List<Wager>> RetrieveWagers(Guid userId)
        {
            return await _context.Wagers.Where(
                uw => uw.UserId == userId).ToListAsync();
        }

        public async Task<Wager> RetrieveWager(Guid userId, Guid eventPoolId)
        {
            return await _context.Wagers.FindAsync(userId, eventPoolId);
        }

        public async Task UpdateWager(Wager userWager)
        {
            Validate(userWager);
            _context.Entry(userWager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagerExists(userWager.UserId, userWager.PoolId))
                {
                    throw new NotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateWager(Wager userWager)
        {
            Validate(userWager);
            _context.Wagers.Add(userWager);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WagerExists(userWager.UserId, userWager.PoolId))
                {
                    throw new ConflictException();
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

        private void Validate(Wager wagerObj)
        {
            var validationResult = _validator.Validate(wagerObj);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.ToString());
            }
        }
    }
}