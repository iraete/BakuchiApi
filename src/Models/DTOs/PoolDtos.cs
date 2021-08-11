using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Models.Dtos
{
    public class PoolDto : BaseIdDto
    {
        public Guid EventId { get; set; }
        public BetType BetType { get; set; }
        public double PoolNum { get; set; }
        public double TotalWagers { get; set; }
        public string Description { get; set; }
    }

    public class CreatePoolDto
    {
        public Guid EventId { get; set; }
        public BetType BetType { get; set; }
        public double PoolNum { get; set; }
        public string Description { get; set; }
    }
}