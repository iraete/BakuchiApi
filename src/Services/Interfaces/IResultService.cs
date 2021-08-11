using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IResultService
    {
        bool ResultExists(Guid eventId, uint outcomeId);
        Task<List<Result>> RetrieveResultsByEvent(Guid eventId);
        Task UpdateResult(Result result);
        Task CreateResult(Result result);
        Task DeleteResult(Result result);
    }
}