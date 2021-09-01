using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models
{
    public class Wager
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public double Amount { get; set; }
        public BetType BetType { get; set; }
        
        // Navigation properties
        public User User { get; set; }
        public Pool Pool { get; set; }
        public Event Event { get; set; }
    }
}