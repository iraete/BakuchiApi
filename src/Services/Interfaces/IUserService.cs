using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        ///     Check that the user exists.
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>A boolean.</returns>
        Task<bool> UserExists(long id);
        
        /// <summary>
        ///     Retrieves a list of users.
        /// </summary>
        /// <returns>A list of user DTOs.</returns>
        Task<List<UserDto>> RetrieveUsers();
        
        /// <summary>
        ///     Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <returns>A user DTO</returns>
        Task<UserDto> RetrieveUser(long id);
        
        /// <summary>
        ///     Updates a specific user. Is only used by the service layer, and is
        ///     unavailable to the controller layer.
        /// </summary>
        /// <param name="userDto">A <c>UpdateUserDto</c> with user fields</param>
        /// <returns></returns>
        Task UpdateUser(UpdateUserDto userDto);
        
        /// <summary>
        ///     Updates a specific user's information.
        /// </summary>
        /// <param name="userDto">A DTO containing user information</param>
        /// <returns></returns>
        Task UpdateUserInfo(UpdateUserInfoDto userDto);
        
        /// <summary>
        ///     Creates a new user.
        /// </summary>
        /// <param name="userDto">A DTO with required user information</param>
        /// <returns></returns>
        Task CreateUser(CreateUserDto userDto);
        
        /// <summary>
        ///     Deletes a specific user.
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <returns></returns>
        Task DeleteUser(long id);
    }
}