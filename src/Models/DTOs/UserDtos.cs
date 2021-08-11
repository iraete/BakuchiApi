using System;

namespace BakuchiApi.Models.Dtos
{
    public class UserDto : BaseIdDto
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public DateTime LastRewardTime { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
    }
}