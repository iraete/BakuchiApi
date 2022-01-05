using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts
{
    public class PoolDto : BaseIdDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public long TotalWagers { get; set; }
        public string Description { get; set; }
    }
}