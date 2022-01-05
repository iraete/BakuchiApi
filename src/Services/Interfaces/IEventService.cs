using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IEventService
    {
        /// <summary>
        ///     Check that an event exists.
        /// </summary>
        /// <param name="eventId">The event GUID</param>
        /// <returns>A boolean value</returns>
        Task<bool> EventExists(Guid eventId);
        
        /// <summary>
        ///     Retrieves a list of events.
        /// </summary>
        /// <returns>A list of event DTOs.</returns>
        Task<List<EventDto>> RetrieveEvents();
        
        /// <summary>
        ///     Retrieves a specific event.
        /// </summary>
        /// <param name="eventId">The event GUID</param>
        /// <returns>An event DTO, or null.</returns>
        Task<EventDto> RetrieveEvent(Guid eventId);
        
        /// <summary>
        ///     Updates a specific event.
        /// </summary>
        /// <param name="eventDto">An event DTO with the updated fields</param>
        /// <returns>A DTO with updated information on the event</returns>
        Task<EventDto> UpdateEvent(UpdateEventDto eventDto);
        
        /// <summary>
        ///     Creates a new event.
        /// </summary>
        /// <param name="eventDto">An event DTO</param>
        /// <returns>A DTO with information on the event.</returns>
        Task<EventDto> CreateEvent(CreateEventDto eventDto);
        
        /// <summary>
        ///     Deletes an event.
        /// </summary>
        /// <param name="eventId">The event GUID</param>
        /// <returns></returns>
        Task DeleteEvent(Guid eventId);
    }
}