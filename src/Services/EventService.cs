using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BakuchiApi.Services
{
    public class EventService : IEventService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<Event> _validator;
        private readonly IMapper _mapper;

        public EventService(
            BakuchiContext context,
            IValidator<Event> validator,
            IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<EventDto>> RetrieveEvents()
        {
            var results = await _context.Events.ToArrayAsync();
            return _mapper.Map<Event[], List<EventDto>>(results);
        }

        public async Task<EventDto> RetrieveEvent(Guid id)
        {
            return _mapper.Map<EventDto>(await _context.Events.FindAsync(id));
        }

        public async Task<EventDto> UpdateEvent(UpdateEventDto eventDto)
        {
            var entity = await _context.Events.FindAsync(eventDto.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(eventDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDto>(entity);
        }

        public async Task<EventDto> CreateEvent(CreateEventDto eventDto)
        {
            var entity = _mapper.Map<Event>(eventDto);
            entity.Created = DateTime.Now;
            await _validator.ValidateAndThrowAsync(entity);
            _context.Events.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDto>(entity);
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var entity = await _context.Events.FindAsync(eventId);
            if (entity != null)
            {
                _context.Events.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EventExists(Guid id)
        {
            var result = await _context.Events.FindAsync(id);
            return result != null;
        }
    }
}