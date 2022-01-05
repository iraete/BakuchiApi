using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts.Requests
{
    public class UpdateResultDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public uint Rank { get; set; }
    }
}