using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BakuchiApi.Services
{
    public class UserService : IUserService
    {
        private readonly BakuchiContext _context;
        private readonly IValidator<User> _validator;
        private readonly IMapper _mapper;

        public UserService(
            BakuchiContext context,
            IValidator<User> validator,
            IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> RetrieveUsers()
        {
            var results = await _context.Users.ToArrayAsync();
            return _mapper.Map<User[], List<UserDto>>(results);
        }

        public async Task<UserDto> RetrieveUser(long discordId)
        {
            return _mapper.Map<UserDto>(await _context.Users.FindAsync(discordId));
        }

        public async Task UpdateUser(UpdateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _validator.ValidateAndThrowAsync(user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(userDto.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task UpdateUserInfo(UpdateUserInfoDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _validator.ValidateAndThrowAsync(user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(userDto.Id))
                {
                    throw new NotFoundException();
                }
                throw;
            }
        }

        public async Task CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _validator.ValidateAndThrowAsync(user);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await UserExists(userDto.Id))
                {
                    throw new ConflictException("A user with the provided "
                                                + "Discord ID already exists.");
                }
                throw;
            }
        }

        public async Task DeleteUser(long discordId)
        {
            var user = await _context.Users.FindAsync(discordId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExists(long discordId)
        {
            var result = await _context.Users.FindAsync(discordId);
            return result != null;
        }
    }
}