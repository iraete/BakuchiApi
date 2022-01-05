using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class ResultEntityTypeConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.Property(r => r.LastEdited)
                .HasDefaultValueSql("localtimestamp");
            builder.HasKey(r => new {r.EventId, r.Alias});
            builder.HasIndex(r => new {r.EventId, r.Rank}).IsUnique();
        }
    }
}