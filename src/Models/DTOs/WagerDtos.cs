using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models.Dtos
{
    public class WagerDto
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public double Amount { get; set; }
        public BetType BetType { get; set; }
    }

    public class CreateWagerDto
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public double Amount { get; set; }
    }
}