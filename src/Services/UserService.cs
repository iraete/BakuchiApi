using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using status = BakuchiApi.StatusExceptions;
using BakuchiApi.Services.Interfaces;

namespace BakuchiApi.Services
{
    public class UserService : IUserService
    {
        private readonly BakuchiContext _context;

        public UserService(BakuchiContext context)
        {
            _context = context;
        }

        public async Task<List<User>> RetrieveUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> RetrieveUser(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> RetrieveUserByDiscordId(long discordId)
        {
            return await _context.Users.FirstOrDefaultAsync(
                u => u.DiscordId == discordId);
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public List<Event> RetrieveEvents(User user)
        {
            return user.Events.ToList();
        }

        public List<Wager> RetrieveWagers(User user)
        {
            return user.Wagers.ToList();
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}