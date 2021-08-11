using System;

namespace BakuchiApi.Models.Dtos
{
    public class UserDto : BaseIdDto
    {
        public string Name { get; set; }
        public long DiscordId { get; set; }
        public int Balance { get; set; }
        public DateTime LastRewardTime { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public long DiscordId { get; set; }
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
    }
}