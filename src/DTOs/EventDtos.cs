using System;
using System.ComponentModel.DataAnnotations;
using BakuchiApi.Models.Dtos.Validators;

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
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public long? DiscordId { get; set; }
        public long? ServerId { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required, DateIsNotMoreThanOneYearLater]
        public DateTime Start { get; set; }

        [Required, DateIsNotMoreThanOneYearLater]
        public DateTime End { get; set; }
    }

    public class UpdateEventDto
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        [DateIsNotMoreThanOneYearLater]
        public DateTime End { get; set; }
    }

    public class EventDtoMapper : DtoMapper<Event, EventDto,
        UpdateEventDto, CreateEventDto>
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

        public override Event MapCreateDtoToEntity(CreateEventDto e)
        {
            return new Event
            {
                Name = e.Name,
                Alias = e.Alias,
                UserId = e.UserId,
                ServerId = e.ServerId,
                Description = e.Description,
                Start = e.Start,
                End = e.End
            };
        }

        public override Event MapUpdateDtoToEntity(UpdateEventDto e)
        {
            return new Event
            {
                Id = e.Id,
                Description = e.Description,
                Start = e.Start,
                End = e.End
            };
        }

    }
}