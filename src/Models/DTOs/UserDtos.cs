using System;

namespace BakuchiApi.Models.Dtos
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
        public string Email { get; set; }
        public long? DiscordId { get; set; }
    }

    public class InputUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long? DiscordId { get; set; }
    }


    public class UserDtoMapper : DtoMapper<User, UserDto>
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
                Balance = (double) dto.Balance,
                Name = dto.Name,
                LastRewardTime = dto.LastRewardTime
            };
        }

        public User MapInputDtoToEntity(InputUserDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                DiscordId = dto.DiscordId
            };
        }

        public User MapUpdateDtoToEntity(UpdateUserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                DiscordId = dto.DiscordId
            };
        }
    }
}