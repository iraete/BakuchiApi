using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IPoolService
    {
        bool PoolExists(Guid id);
        Task<List<Pool>> RetrievePools();
        Task<Pool> RetrievePool(Guid id);
        Task<List<Pool>> RetrievePoolsByEvent(Guid eventId);
        Task UpdatePool(Pool pool);
        Task CreatePool(Pool pool);
        Task DeletePool(Pool pool);
    }
}