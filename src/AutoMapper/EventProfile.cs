using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<CreateEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
        }
    }
}