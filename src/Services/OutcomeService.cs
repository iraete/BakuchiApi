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
using Microsoft.Extensions.Logging;

namespace BakuchiApi.Services
{
    public class OutcomeService : IOutcomeService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<Outcome> _validator;
        private readonly IMapper _mapper;

        public OutcomeService(
            BakuchiContext context,
            IValidator<Outcome> validator,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OutcomeDto>> RetrieveOutcomesByEvent(Guid eventId)
        {
            var results = await _context.Outcomes.Where(o => o.EventId == eventId).ToArrayAsync();
            return _mapper.Map<Outcome[], List<OutcomeDto>>(results);
        }

        public async Task<OutcomeDto> RetrieveOutcome(Guid eventId, string alias)
        {
            return _mapper.Map<OutcomeDto>(await _context.Outcomes.FindAsync(eventId, alias));
        }

        public async Task<OutcomeDto> UpdateOutcome(UpdateOutcomeDto outcomeDto)
        {
            var entity = await _context.Outcomes.FindAsync(outcomeDto.EventId, outcomeDto.Alias);

            if (entity == null)
            {
                throw new NotFoundException();
            }
            
            _mapper.Map(outcomeDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await OutcomeExists(outcomeDto.EventId, outcomeDto.Alias))
                {
                    throw new ConflictException("A outcome with the given alias " +
                                                "associated with the event already exists");
                }
                throw;
            }
            return _mapper.Map<OutcomeDto>(entity);
        }

        public async Task<OutcomeDto> CreateOutcome(CreateOutcomeDto outcomeDto)
        {
            var entity = _mapper.Map<Outcome>(outcomeDto);
            entity.Created = DateTime.Now;
            await _validator.ValidateAndThrowAsync(entity);
            try
            {
                _context.Outcomes.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await OutcomeExists(outcomeDto.EventId, outcomeDto.Alias))
                {
                    throw new ConflictException("A outcome with the given alias " +
                                                "associated with the event already exists");
                }
                throw;
            }
            return _mapper.Map<OutcomeDto>(entity);
        }

        public async Task DeleteOutcome(Guid eventId, string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new Exception("Alias must not be null or whitespace.");
            }
            
            var entity = await _context.Outcomes.FindAsync(eventId, alias);
            if (entity != null)
            {
                _context.Outcomes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> OutcomeExists(Guid eventId, string alias)
        {
            var result = await _context.Outcomes.FindAsync(eventId, alias);
            return result != null;
        }
    }
}