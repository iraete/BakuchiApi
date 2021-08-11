using System;

namespace BakuchiApi.Models.Dtos
{
    public class OutcomeDto
    {
        public uint Id { get; set; }
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }

    public class CreateOutcomeDto
    {
        public Guid OutcomeId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
    }

    public class OutcomeDtoMapper : DtoMapper<Outcome, OutcomeDto>
    {
        public override OutcomeDto MapEntityToDto(Outcome outcome)
        {
            return new OutcomeDto
            {
                Id = outcome.Id,
                EventId = outcome.EventId,
                Alias = outcome.Alias,
                Name = outcome.Name,
                Created = outcome.Created
            };
        }
    }
}