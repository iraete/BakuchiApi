using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models.Dtos
{
    public class PoolDto : BaseIdDto
    {
        public Guid EventId { get; set; }
        public BetType BetType { get; set; }
        public double PoolNum { get; set; }
        public double TotalWagers { get; set; }
        public string Description { get; set; }
    }

    public class CreatePoolDto
    {
        public Guid EventId { get; set; }
        public BetType BetType { get; set; }
        public double PoolNum { get; set; }
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
                BetType = p.BetType,
                PoolNum = p.PoolNum,
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