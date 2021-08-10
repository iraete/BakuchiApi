using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IEventService
    {
        bool EventExists(Guid eventId);
        Task<List<Event>> GetEvents();
        Task<Event> GetEvent(Guid eventId);
        Task PutEvent(Event eventDto);
        Task PostEvent(Event eventDto);
        Task DeleteEvent(Event eventDto);
    }
}