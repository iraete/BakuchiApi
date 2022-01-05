using System;

namespace BakuchiApi.Contracts
{
    public class ResultDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public uint Rank { get; set; }
        public DateTime LastEdited { get; set; }
    }
}