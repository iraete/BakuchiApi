using System;
using BakuchiApi.Models;

namespace BakuchiApi.Controllers.Dtos
{
    public class UserDto : BaseIdDto
    {
        public string Name { get; set; }
        public long? DiscordId { get; set; }
        public int Balance { get; set; }
        public DateTime LastRewardTime { get; set; }
    }

    public class UpdateUserDto : BaseIdDto
    {
        public string Name { get; set; }
        public long? DiscordId { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public long? DiscordId { get; set; }
    }


    public class UserDtoMapper : DtoMapper<User, UserDto,
        UpdateUserDto, CreateUserDto>
    {
        public override UserDto MapEntityToDto(User u)
        {
            return new UserDto
            {
                Id = u.Id,
                DiscordId = u.DiscordId,
                Name = u.Name,
                Balance = (int) u.Balance,
                LastRewardTime = u.LastRewardTime
            };
        }

        public override User MapDtoToEntity(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                DiscordId = dto.DiscordId,
                Balance = dto.Balance,
                Name = dto.Name,
                LastRewardTime = dto.LastRewardTime
            };
        }

        public override User MapCreateDtoToEntity(CreateUserDto dto)
        {
            return new User
            {
                Name = dto.Name,
                DiscordId = dto.DiscordId
            };
        }

        public override User MapUpdateDtoToEntity(UpdateUserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                DiscordId = dto.DiscordId
            };
        }
    }
}