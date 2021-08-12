using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IOutcomeService
    {
        bool OutcomeExists(Guid eventId, string alias);
        Task<List<Outcome>> RetrieveOutcomesByEvent(Guid eventId);
        Task<Outcome> RetrieveOutcome(Guid eventId, string alias);
        Task UpdateOutcome(Outcome outcome);
        Task CreateOutcome(Outcome outcome);
        Task DeleteOutcome(Outcome outcome);
    }
}