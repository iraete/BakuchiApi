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
    public class ResultService : IResultService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<Result> _validator;
        private readonly IMapper _mapper;

        public ResultService(
            BakuchiContext context,
            IValidator<Result> validator,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultDto>> RetrieveResultsByEvent(Guid eventId)
        {
            var results = await _context.Results.Where(
                r => r.EventId == eventId).ToArrayAsync();
            return _mapper.Map<Result[], List<ResultDto>>(results);
        }

        public async Task<ResultDto> UpdateResult(UpdateResultDto resultDto)
        {
            var entity = await _context.Results.FindAsync(resultDto.EventId, resultDto.Alias);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(resultDto, entity);
            entity.LastEdited = DateTime.Now;
            await _validator.ValidateAndThrowAsync(entity);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<ResultDto>(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ResultExists(entity.EventId, entity.Alias))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task<ResultDto> CreateResult(CreateResultDto resultDto)
        {
            var entity = _mapper.Map<Result>(resultDto);
            entity.LastEdited = DateTime.Now;
            await _validator.ValidateAndThrowAsync(entity);
            try
            {
                _context.Results.Add(entity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ResultDto>(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ResultExists(entity.EventId, entity.Alias))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task DeleteResult(Guid eventId, string alias)
        {
            var entity = await _context.Results.FindAsync(eventId, alias);
            if (entity != null)
            {
                _context.Results.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ResultExists(Guid eventId, string alias)
        {
            var result = await _context.Results.FindAsync(eventId, alias);
            return result != null;
        }
    }
}