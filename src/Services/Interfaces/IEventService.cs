using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;

namespace BakuchiApi.Services.Interfaces
{
    public interface IEventService
    {
        bool EventExists(Guid eventId);
        Task<List<Event>> RetrieveEvents();
        Task<Event> RetrieveEvent(Guid eventId);
        Task UpdateEvent(Event eventObj);
        Task CreateEvent(Event eventObj);
        Task DeleteEvent(Event eventObj);
    }
}