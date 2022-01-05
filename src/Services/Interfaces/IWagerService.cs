using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IWagerService
    {
        /// <summary>
        ///     Check that some wager exists.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="poolId">The ID of the pool</param>
        /// <returns>A boolean value</returns>
        Task<bool> WagerExists(long userId, Guid poolId);
        
        /// <summary>
        ///     Fetch a list of wagers of a given user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A list of wager DTOs</returns>
        Task<List<WagerDto>> RetrieveWagers(long userId);
        
        /// <summary>
        ///     Retrieve the details of a particular wager.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="poolId">The ID of the pool</param>
        /// <returns>A wager DTO</returns>
        Task<WagerDto> RetrieveWager(long userId, Guid poolId);
        
        /// <summary>
        ///     Update a wager.
        /// </summary>
        /// <param name="wagerDto">A DTO with information about the wager</param>
        /// <returns></returns>
        Task<WagerDto> UpdateWager(UpdateWagerDto wagerDto);
        
        /// <summary>
        ///     Create a wager.
        /// </summary>
        /// <param name="wagerDto">A DTO with information about the wager</param>
        /// <returns></returns>
        Task<WagerDto> CreateWager(CreateWagerDto wagerDto);
        
        /// <summary>
        ///     Delete a wager.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="poolId">The ID of the pool</param>
        /// <returns></returns>
        Task DeleteWager(long userId, Guid poolId);
    }
}