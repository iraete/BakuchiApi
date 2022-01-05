using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class PoolEntityTypeConfiguration : IEntityTypeConfiguration<Pool>
    {
        public void Configure(EntityTypeBuilder<Pool> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(p => p.TotalWagers).HasDefaultValue(0);
            builder.Property(p => p.Alias)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.HasOne(p => p.Event)
                .WithMany(ev => ev.Pools)
                .HasForeignKey(p => p.EventId)
                .IsRequired();
            builder.HasIndex(p => new { p.Alias, p.EventId }).IsUnique();
            builder.Property(e => e.BetType)
                .IsRequired();
        }
    }
}