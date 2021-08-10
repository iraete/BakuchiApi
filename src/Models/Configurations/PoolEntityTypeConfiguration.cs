using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class PoolEntityTypeConfiguration: IEntityTypeConfiguration<Pool>
    {
        public void Configure(EntityTypeBuilder<Pool> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
            builder.HasOne(p => p.Event)
                .WithMany(ev => ev.Pools)
                .HasForeignKey(p => p.EventId)
                .IsRequired();
            
            builder.Property(e => e.BetType)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired();
        }
    }
}