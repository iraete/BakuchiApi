using System;
using System.Collections.Generic;

namespace BakuchiApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public DateTime LastRewardTime { get; set; }

        public virtual List<Event> Events { get; set; }
        public virtual List<Wager> Wagers { get; set; }
    }
}