using System;
using System.Collections.Generic;

namespace BakuchiApi.Models
{
    public class Server
    {
        public long Id { get; set; }
        // Navigation property
        public virtual List<Event> Events { get; set; }
    }
}