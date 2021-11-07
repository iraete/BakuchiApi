using System;

namespace BakuchiApi.Contracts.Requests
{
    public class UpdateEventDto : BaseIdDto
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}