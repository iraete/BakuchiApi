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
        Task<List<Event>> GetEvents();
        Task<Event> GetEvent(Guid eventId);
        Task PutEvent(Event eventObj);
        Task PostEvent(Event eventObj);
        Task DeleteEvent(Event eventObj);
    }
}