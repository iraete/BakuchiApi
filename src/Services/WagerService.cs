using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;
using BakuchiApi.Models.Validators;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BakuchiApi.Services
{
    public class WagerService : IWagerService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<Wager>_validator;
        private readonly IMapper _mapper;

        public WagerService(
            BakuchiContext context,
            IValidator<Wager> validator,
            IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> WagerExists(long userId, Guid eventPoolId)
        {
            var result = await _context.Wagers.FindAsync(new {userId, eventPoolId});
            return result != null;
        }

        public async Task<List<WagerDto>> RetrieveWagers(long userId)
        {
            var results = await _context.Wagers.Where(
                uw => uw.UserId == userId).ToArrayAsync();
            return _mapper.Map<Wager[], List<WagerDto>>(results);
        }

        public async Task<WagerDto> RetrieveWager(long userId, Guid eventPoolId)
        {
            return _mapper.Map<WagerDto>(await _context.Wagers.FindAsync(new {
                userId, eventPoolId
            }));
        }

        public async Task<WagerDto> UpdateWager(UpdateWagerDto wagerDto)
        {
            var entity = await _context.Wagers.FindAsync(new {
                wagerDto.UserId, wagerDto.PoolId});

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(wagerDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<WagerDto>(entity);
        }

        public async Task<WagerDto> CreateWager(CreateWagerDto wagerDto)
        {
            var wager = _mapper.Map<Wager>(wagerDto);
            await _validator.ValidateAndThrowAsync(wager);
            try
            {
                _context.Wagers.Add(wager);
                await _context.SaveChangesAsync();
                return _mapper.Map<WagerDto>(wager);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await WagerExists(wagerDto.UserId, wagerDto.PoolId))
                {
                    throw new ConflictException("A wager with the provided "
                                                + "User ID and Pool ID already exists.");
                }
                throw;
            }
        }

        public async Task DeleteWager(long userId, Guid poolId)
        {
            var wager = await _context.Wagers.FindAsync(new {userId, poolId});
            if (wager != null)
            {
                _context.Wagers.Remove(wager);
                await _context.SaveChangesAsync();
            }
        }
    }
}