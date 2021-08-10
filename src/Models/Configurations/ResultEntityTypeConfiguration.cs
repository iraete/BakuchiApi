using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class ResultEntityTypeConfiguration: IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(r => new { r.EventId, r.OutcomeId });
            builder.HasIndex(r => new {r.EventId, r.Rank}).IsUnique();
        }
    }
}