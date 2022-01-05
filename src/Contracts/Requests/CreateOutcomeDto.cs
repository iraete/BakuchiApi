using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts.Requests
{
    public class CreateOutcomeDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
    }
}