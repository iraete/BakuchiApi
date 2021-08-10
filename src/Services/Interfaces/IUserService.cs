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
        Task PutUser(User userDto);
        Task PostUser(User userDto);
        Task DeleteUser(User userDto);
        List<Event> GetEvents(User userDto);
        List<Wager> GetWagers(User userDto);
    }
}