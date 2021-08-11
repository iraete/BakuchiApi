using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IPoolService
    {
        bool PoolExists(Guid id);
        Task<List<Pool>> GetPools();
        Task<Pool> GetPool(Guid id);
        Task<List<Pool>> GetPoolsByEvent(Guid eventId);
        Task PutPool(Pool pool);
        Task PostPool(Pool pool);
        Task DeletePool(Pool pool);
    }
}