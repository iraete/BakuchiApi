using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class OutcomeEntityTypeConfiguration: IEntityTypeConfiguration<Outcome>
    {
        public void Configure(EntityTypeBuilder<Outcome> builder)
        {
            builder.HasOne(o => o.Event)
                .WithMany(e => e.Outcomes)
                .HasForeignKey(o => o.EventId)
                .IsRequired();
        }
    }
}