using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models.Dtos
{
    public class WagerDto
    {
        public Guid UserId { get; set; }
        public Guid DiscordId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public int Amount { get; set; }
        public BetType BetType { get; set; }
    }

    public class CreateWagerDto
    {
        public Guid UserId { get; set; }
        public Guid DiscordId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public double Amount { get; set; }
    }

    public class WagerDtoMapper : DtoMapper<Wager, WagerDto>
    {
        public override WagerDto MapEntityToDto(Wager w)
        {
            return new WagerDto
            {
                UserId = w.UserId,
                DiscordId = w.DiscordId,
                EventId = w.EventId,
                PoolId = w.PoolId,
                OutcomeId = w.OutcomeId,
                Amount = (int) w.Amount,
                BetType = w.BetType
            };
        }
    }
}