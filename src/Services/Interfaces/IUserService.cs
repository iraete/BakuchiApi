using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IUserService
    {
        bool UserExists(Guid id);
        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid id);
        Task PutUser(User user);
        Task PostUser(User user);
        Task DeleteUser(User user);
        List<Event> GetEvents(User user);
        List<Wager> GetWagers(User user);
    }
}