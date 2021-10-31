using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class OutcomeEntityTypeConfiguration : IEntityTypeConfiguration<Outcome>
    {
        public void Configure(EntityTypeBuilder<Outcome> builder)
        {
            builder.HasKey(o => new {o.EventId, o.Alias});
            builder.Property(o => o.Alias).IsRequired()
                .HasMaxLength(50);
            builder.Property(o => o.Created)
                .HasDefaultValueSql("localtimestamp");

            builder.HasOne(o => o.Event)
                .WithMany(e => e.Outcomes)
                .HasForeignKey(o => o.EventId)
                .IsRequired();
        }
    }
}