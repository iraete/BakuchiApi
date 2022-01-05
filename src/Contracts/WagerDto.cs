using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts
{
    public class WagerDto
    {
        public long UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public long Amount { get; set; }
        public BetType BetType { get; set; }
    }
}