using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IOutcomeService
    {
        /// <summary>
        /// Check that a given outcome of an event exists.
        /// </summary>
        /// <param name="eventId">The GUID of the event</param>
        /// <param name="alias">The outcome's alias</param>
        /// <returns>A boolean value</returns>
        Task<bool> OutcomeExists(Guid eventId, string alias);
        
        /// <summary>
        /// Retrieve a list of outcomes belonging to an event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>A list of outcome DTOs.</returns>
        Task<List<OutcomeDto>> RetrieveOutcomesByEvent(Guid eventId);
        
        /// <summary>
        /// Retrieve details of a specific outcome.
        /// </summary>
        /// <param name="eventId">The GUID of the event</param>
        /// <param name="alias">The outcome's alias</param>
        /// <returns></returns>
        Task<OutcomeDto> RetrieveOutcome(Guid eventId, string alias);
        
        /// <summary>
        /// Update information about an outcome.
        /// </summary>
        /// <param name="outcomeDto">A DTO containing information to update the outcome.</param>
        /// <returns>A DTO with the updated outcome information</returns>
        Task<OutcomeDto> UpdateOutcome(UpdateOutcomeDto outcomeDto);
        
        /// <summary>
        /// Create a new outcome for an event.
        /// </summary>
        /// <param name="outcome">A DTO containing information to update the outcome.</param>
        /// <returns>A DTO with the new outcome information.</returns>
        Task<OutcomeDto> CreateOutcome(CreateOutcomeDto outcome);
        
        /// <summary>
        /// Delete an outcome.
        /// </summary>
        /// <param name="eventId">The GUID of an event.</param>
        /// <param name="alias">The alias of the outcome.</param>
        /// <returns></returns>
        Task DeleteOutcome(Guid eventId, string alias);
    }
}