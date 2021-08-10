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
        Task PutResult(Result resultDto);
        Task PostResult(Result resultDto);
        Task DeleteResult(Result resultDto);
    }
}