using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts.Requests
{
    public class UpdateOutcomeDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
    }
}