using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IPoolService
    {
        /// <summary>
        ///     Check that a given pool exists.
        /// </summary>
        /// <param name="alias">The pool's alias</param>
        /// <param name="eventId">The event ID of the pool</param>
        /// <returns>A boolean value.</returns>
        Task<bool> PoolExists(string alias, Guid eventId);
        
        /// <summary>
        ///     Retrieve a list of pools.
        /// </summary>
        /// <returns>A list of Pool DTOs.</returns>
        Task<List<PoolDto>> RetrievePools();
        
        /// <summary>
        ///     Retrieve a particular pool.
        /// </summary>
        /// <param name="poolId">The ID of the pool</param>
        /// <returns>A pool DTO with information on the pool.</returns>
        Task<PoolDto> RetrievePool(Guid poolId);
        
        /// <summary>
        ///     Updates a pool.
        /// </summary>
        /// <param name="pool">A pool DTO</param>
        /// <returns></returns>
        Task<PoolDto> UpdatePool(UpdatePoolDto pool);
        
        /// <summary>
        ///     Creates a pool.
        /// </summary>
        /// <param name="pool">A pool DTO</param>
        /// <returns></returns>
        Task<PoolDto> CreatePool(CreatePoolDto pool);
        
        /// <summary>
        ///     Deletes a pool.
        /// </summary>
        /// <param name="poolId">ID of the pool</param>
        /// <returns></returns>
        Task DeletePool(Guid poolId);
    }
}