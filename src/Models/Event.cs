using System;
using System.Collections.Generic;

namespace BakuchiApi.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Shortname of the event
        public string Alias { get; set; }

        // Owner / Creator
        public Guid UserId { get; set; }

        // Server it originated from
        public long? ServerId { get; set; }
        
        public string Description { get; set; }

        // Determines when betting pools close
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? Created { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Server Server { get; set; }
        /* Event is the principal entity in the one-to-many relationship with
           Pool */
        public virtual List<Pool> Pools { get; set; }
        public virtual List<Outcome> Outcomes { get; set; }
    }
}