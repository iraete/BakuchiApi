using System;

namespace BakuchiApi.Contracts.Requests
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Alias { get; set; }
        public long UserId { get; set; }
        public long ServerId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}