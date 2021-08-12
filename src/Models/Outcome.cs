using System;
using System.Collections.Generic;

namespace BakuchiApi.Models
{
    public class Outcome
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
    }
}