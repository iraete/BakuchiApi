using System;

namespace BakuchiApi.Models
{
    public class Result
    {
        public Guid EventId { get; set; }
        public uint OutcomeId { get; set; }
        public uint Rank { get; set; }
        public DateTime LastEdited { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
        public Outcome Outcome { get; set; }
    }
}