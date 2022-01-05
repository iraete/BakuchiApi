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

        public async Task<UserDto> RetrieveUser(long id)
        {
            return _mapper.Map<UserDto>(await _context.Users.FindAsync(id));
        }

        public async Task<UserDto> UpdateUser(UpdateUserDto userDto)
        {
            var entity = await _context.Users.FindAsync(userDto.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(userDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> UpdateUserInfo(UpdateUserInfoDto userDto)
        {
            var entity = await _context.Users.FindAsync(userDto.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(userDto, entity);
            await _validator.ValidateAndThrowAsync(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _validator.ValidateAndThrowAsync(user);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserDto>(user);
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

        public async Task DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExists(long id)
        {
            var result = await _context.Users.FindAsync(id);
            return result != null;
        }
    }
}