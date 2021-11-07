using System;

namespace BakuchiApi.Contracts
{
    public class EventDto : BaseIdDto
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public long UserId { get; set; }
        public long ServerId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
    }
}