using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IUserService
    {
        bool UserExists(Guid id);
        bool DiscordIdExists(long? id);
        Task<List<User>> RetrieveUsers();
        Task<User> RetrieveUser(Guid id);
        Task<User> RetrieveUserByDiscordId(long discordId);
        Task UpdateUser(User user);
        Task CreateUser(User user);
        Task DeleteUser(User user);
        List<Event> RetrieveEvents(User user);
        List<Wager> RetrieveWagers(User user);
    }
}