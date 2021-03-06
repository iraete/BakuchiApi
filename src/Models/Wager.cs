using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models
{
    public class Wager
    {
        public long UserId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public long Amount { get; set; }
        public BetType BetType { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Pool Pool { get; set; }
    }
}