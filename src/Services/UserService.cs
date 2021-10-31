using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakuchiApi.Models;
using BakuchiApi.Models.Validators;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.EntityFrameworkCore;

namespace BakuchiApi.Services
{
    public class UserService : IUserService
    {
        private readonly BakuchiContext _context;
        private readonly UserValidator _validator;

        public UserService(BakuchiContext context)
        {
            _validator = new UserValidator();
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
            Validate(user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    throw new NotFoundException();
                }

                if (user.DiscordId != null
                    && !DiscordIdExists(user.DiscordId))
                {
                    throw new ConflictException();
                }

                throw;
            }
        }

        public async Task CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            Validate(user);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (user.DiscordId != null
                    && !DiscordIdExists(user.DiscordId))
                {
                    throw new ConflictException("A user with the provided "
                                                + "Discord ID already exists.");
                }

                throw;
            }
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

        public bool DiscordIdExists(long? id)
        {
            return _context.Users.Any(e => e.DiscordId == id);
        }

        private void Validate(User userObj)
        {
            var validationResult = _validator.Validate(userObj);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.ToString());
            }
        }
    }
}