using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IWagerService
    {
        bool WagerExists(Guid userId, Guid eventPoolId);
        Task<List<Wager>> RetrieveWagers(Guid userId);
        Task<Wager> RetrieveWager(Guid userId, Guid eventPoolId);
        Task UpdateWager(Wager userWager);
        Task CreateWager(Wager userWager);
        Task DeleteWager(Wager userWager);
    }
}