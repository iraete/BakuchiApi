using System;
using BakuchiApi.Models;

namespace BakuchiApi.Contracts
{
    public class OutcomeDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        
        public DateTime Created { get; set; }
    }
}