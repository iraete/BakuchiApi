using System;

namespace BakuchiApi.Models.Dtos
{
    public class ResultDto
    {
        public Guid EventId { get; set; }
        public uint OutcomeId { get; set; }
        public uint Rank { get; set; }
        public DateTime LastEdited { get; set; }
    }

    public class CreateResultDto
    {
        public Guid EventId { get; set; }
        public uint OutcomeId { get; set; }
        public uint Rank { get; set; }
    }
}