using System;

namespace BakuchiApi.Models.Dtos
{
    public class EventDto : BaseIdDto
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid UserId { get; set; }
        public long? ServerId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? Created { get; set; }
    }

    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid UserId { get; set; }
        public long? ServerId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class EventDtoMapper : DtoMapper<Event, EventDto>
    {
        public override EventDto MapEntityToDto(Event e)
        {
            return new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                UserId = e.UserId,
                ServerId = e.ServerId,
                Description = e.Description,
                Start = e.Start,
                End = e.End,
                Created = e.Created
            };
        }

        public override Event MapDtoToEntity(EventDto e)
        {
            return new Event
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                UserId = e.UserId,
                ServerId = e.ServerId,
                Description = e.Description,
                Start = e.Start,
                End = e.End,
                Created = e.Created
            };
        }

    }
}