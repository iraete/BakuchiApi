using System;

namespace BakuchiApi.Contracts.Requests
{
    public class CreateWagerDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public Guid PoolId { get; set; }
        public Guid OutcomeId { get; set; }
        public long Amount { get; set; }
    }
}