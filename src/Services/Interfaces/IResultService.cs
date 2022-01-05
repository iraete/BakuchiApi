using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IResultService
    {
        Task<bool> ResultExists(Guid eventId, string alias);
        Task<List<ResultDto>> RetrieveResultsByEvent(Guid eventId);
        Task<ResultDto> UpdateResult(UpdateResultDto result);
        Task<ResultDto> CreateResult(CreateResultDto result);
        Task DeleteResult(Guid eventId, string alias);
    }
}