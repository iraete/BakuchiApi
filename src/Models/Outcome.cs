using System;
using System.Collections.Generic;

namespace BakuchiApi.Models
{
    public class Outcome
    {
        public uint Id { get; set; }
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
        /* Event is the principal entity in the one-to-many relationship with
           Pool */
        public virtual List<Pool> Pools { get; set; }
    }
}