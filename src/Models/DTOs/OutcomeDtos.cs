using System;

namespace BakuchiApi.Models.Dtos
{
    public class OutcomeDto
    {
        public uint Id { get; set; }
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }

    public class CreateOutcomeDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
    }
}