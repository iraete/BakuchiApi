using System;

namespace BakuchiApi.Models.Dtos
{
    public class EventDto : BaseIdDto
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid UserId { get; set; }
        public Guid ServerId { get; set; }
        public string description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
    }

    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid UserId { get; set; }
        public Guid ServerId { get; set; }
        public string description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}