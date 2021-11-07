using System;

namespace BakuchiApi.Contracts.Requests
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public DateTime LastRewardTime { get; set; }
    }
}