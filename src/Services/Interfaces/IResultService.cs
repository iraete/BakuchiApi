using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IResultService
    {
        bool ResultExists(Guid eventId, uint outcomeId);
        Task<List<Result>> GetResultsByEvent(Guid eventId);
        Task PutResult(Result result);
        Task PostResult(Result result);
        Task DeleteResult(Result result);
    }
}