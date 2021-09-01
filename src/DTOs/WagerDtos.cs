using System;
using BakuchiApi.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace BakuchiApi.Models.Dtos
{
    public class WagerDto
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public int Amount { get; set; }
        public BetType BetType { get; set; }
    }

    public class CreateWagerDto
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public long? DiscordId { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid PoolId { get; set; }

        [Required]
        public Guid OutcomeId { get; set; }

        [Required]
        public double Amount { get; set; }
    }

    public class UpdateWagerDto : CreateWagerDto
    { }

    public class WagerDtoMapper : DtoMapper<Wager, WagerDto, 
        UpdateWagerDto, CreateWagerDto>
    {
        public override WagerDto MapEntityToDto(Wager w)
        {
            return new WagerDto
            {
                UserId = w.UserId,
                EventId = w.EventId,
                PoolId = w.PoolId,
                OutcomeId = w.OutcomeId,
                Amount = (int) w.Amount,
                BetType = w.BetType
            };
        }

        public override Wager MapDtoToEntity(WagerDto dto)
        {
            return new Wager
            {
                UserId = dto.UserId,
                EventId = dto.EventId,
                PoolId = dto.PoolId,
                OutcomeId = dto.OutcomeId,
                Amount = (int) dto.Amount,
                BetType = dto.BetType
            };
        }

        public override Wager MapCreateDtoToEntity(CreateWagerDto dto)
        {
            return new Wager
            {
                UserId = dto.UserId,
                EventId = dto.EventId,
                PoolId = dto.PoolId,
                OutcomeId = dto.OutcomeId,
                Amount = (int) dto.Amount,
            };
        }

        public override Wager MapUpdateDtoToEntity(UpdateWagerDto dto)
            => MapCreateDtoToEntity(dto);
    }
}