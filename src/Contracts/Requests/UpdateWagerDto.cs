using System;

namespace BakuchiApi.Contracts.Requests
{
    public class UpdateWagerDto
    {
        public long UserId { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public long Amount { get; set; }
    }
}