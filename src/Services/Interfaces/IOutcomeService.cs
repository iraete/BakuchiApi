using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IOutcomeService
    {
        bool OutcomeExists(Guid eventId, uint outcomeId);
        Task<List<Outcome>> GetOutcomesByEvent(Guid eventId);
        Task<Outcome> GetOutcome(Guid eventId, uint outcomeId);
        Task PutOutcome(Outcome outcome);
        Task PostOutcome(Outcome outcome);
        Task DeleteOutcome(Outcome outcome);
    }
}