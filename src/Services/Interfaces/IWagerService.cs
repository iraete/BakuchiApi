using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IWagerService
    {
        bool WagerExists(Guid userId, Guid eventPoolId);
        Task<List<Wager>> GetWagers(Guid userId);
        Task<Wager> GetWager(Guid userId, Guid eventPoolId);
        Task PutWager(Wager userWagerDto);
        Task PostWager(Wager userWagerDto);
        Task DeleteWager(Wager userWagerDto);
    }
}