using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models.Dtos
{
    public class PoolDto : BaseIdDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public double TotalWagers { get; set; }
        public string Description { get; set; }
    }

    public class CreatePoolDto
    {
        public Guid EventId { get; set; }
        public BetType BetType { get; set; }
        public string Description { get; set; }
    }

    public class PoolDtoMapper : DtoMapper<Pool, PoolDto>
    {
        public override PoolDto MapEntityToDto(Pool p)
        {
            return new PoolDto
            {
                Id = p.Id,
                EventId = p.EventId,
                Alias = p.Alias,
                BetType = p.BetType,
                TotalWagers = p.TotalWagers,
                Description = p.Description
            };
        }

        public override Pool MapDtoToEntity(PoolDto dto)
        {
            throw new NotImplementedException();
        }
    }
}