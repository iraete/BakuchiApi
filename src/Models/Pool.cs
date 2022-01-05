using System;
using System.Collections.Generic;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models
{
    public class Pool
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public long TotalWagers { get; set; }
        public string Description { get; set; }

        // Navigation property
        // Many-to-one relationship with Event
        public Event Event { get; set; }
        public virtual List<Wager> Wagers { get; set; }
    }
}