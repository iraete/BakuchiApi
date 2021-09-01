using System;
using System.ComponentModel.DataAnnotations;

namespace BakuchiApi.Models.Dtos
{
    public class OutcomeDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }

    public class CreateOutcomeDto
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class UpdateOutcomeDto : CreateOutcomeDto
    { }

    public class OutcomeDtoMapper : DtoMapper<Outcome, OutcomeDto,
        UpdateOutcomeDto, CreateOutcomeDto>
    {
        public override OutcomeDto MapEntityToDto(Outcome outcome)
        {
            return new OutcomeDto
            {
                EventId = outcome.EventId,
                Alias = outcome.Alias,
                Name = outcome.Name,
                Created = outcome.Created
            };
        }

        public override Outcome MapDtoToEntity(OutcomeDto dto)
        {
            return new Outcome
            {
                EventId = dto.EventId,
                Alias = dto.Alias,
                Name = dto.Name,
            };
        }

        public override Outcome MapCreateDtoToEntity(CreateOutcomeDto dto)
        {
            return new Outcome
            {
                EventId = dto.EventId,
                Alias = dto.Alias,
                Name = dto.Name,
            };
        }

        public override Outcome MapUpdateDtoToEntity(UpdateOutcomeDto dto) 
            => MapCreateDtoToEntity(dto);
    }
}