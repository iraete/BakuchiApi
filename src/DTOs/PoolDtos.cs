using System;
using BakuchiApi.Models;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Controllers.Dtos
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
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePoolDto
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public string Description { get; set; }
    }

    public class PoolDtoMapper : DtoMapper<Pool, PoolDto,
        UpdatePoolDto, CreatePoolDto>
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
            return new Pool
            {
                Id = dto.Id,
                EventId = dto.EventId,
                BetType = dto.BetType,
                TotalWagers = dto.TotalWagers,
                Description = dto.Description
            };
        }

        public override Pool MapCreateDtoToEntity(CreatePoolDto dto)
        {
            return new Pool
            {
                EventId = dto.EventId,
                BetType = dto.BetType,
                Alias = dto.Alias,
                Description = dto.Description
            };
        }

        public override Pool MapUpdateDtoToEntity(UpdatePoolDto dto)
        {
            return new Pool
            {
                Id = dto.Id,
                Alias = dto.Alias,
                BetType = dto.BetType,
                Description = dto.Description
            };
        }
    }
}