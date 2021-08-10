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

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task PutUser(User userDto)
        {
            _context.Entry(userDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userDto.Id))
                {
                    throw new status.NotFoundException();
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task PostUser(User userDto)
        {
            _context.Users.Add(userDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User userDto)
        {
            _context.Users.Remove(userDto);
            await _context.SaveChangesAsync();
        }

        public List<Event> GetEvents(User userDto)
        {
            return userDto.Events.ToList();
        }

        public List<Wager> GetWagers(User userDto)
        {
            return userDto.Wagers.ToList();
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}