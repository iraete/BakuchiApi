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
    public class PoolService : IPoolService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<Pool> _validator;
        private readonly IMapper _mapper;

        public PoolService(BakuchiContext context, IValidator<Pool> validator, IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<PoolDto>> RetrievePools()
        {
            var results = await _context.Pools.ToArrayAsync();
            return _mapper.Map<Pool[], List<PoolDto>>(results);
        }

        public async Task<PoolDto> RetrievePool(Guid id)
        {
            return _mapper.Map<PoolDto>(await _context.Pools.FindAsync(id));
        }

        public async Task<PoolDto> UpdatePool(UpdatePoolDto poolDto)
        {
            var entity = await _context.Pools.FindAsync(poolDto.Id);

            if (entity == null)
                throw new NotFoundException();
            
            _mapper.Map(poolDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await PoolExists(poolDto.Alias, poolDto.EventId))
                {
                    throw new ConflictException("A pool with the given alias " +
                                                "associated with the event already exists");
                }
            }
            return _mapper.Map<PoolDto>(entity);
        }

        public async Task<PoolDto> CreatePool(CreatePoolDto poolDto)
        {
            var entity = _mapper.Map<Pool>(poolDto);
            await _validator.ValidateAndThrowAsync(entity);
            try
            {
                _context.Pools.Add(entity);
                await _context.SaveChangesAsync();
                return _mapper.Map<PoolDto>(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await PoolExists(poolDto.Alias, poolDto.EventId))
                {
                    throw new ConflictException("A pool with the given alias " +
                                                "associated with the event already exists");
                }

                throw;
            }
        }

        public async Task DeletePool(Guid poolId)
        {
            var pool = await _context.Users.FindAsync(poolId);
            if (pool != null)
            {
                _context.Users.Remove(pool);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PoolExists(string alias, Guid eventId)
        {
            var result = await _context.Pools
                .FirstOrDefaultAsync(p => p.Alias == alias && p.EventId == eventId);
            return result != null;
        }
    }
}