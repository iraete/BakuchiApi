using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql
                ("uuid_generate_v4()");
            builder.Property(e => e.Alias).HasMaxLength(50)
                .IsRequired();
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Created)
                .HasDefaultValueSql("localtimestamp");

            builder.HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(p => new {p.UserId});

            builder.HasOne(e => e.Server)
                .WithMany(s => s.Events)
                .HasForeignKey(p => new {p.ServerId})
                .IsRequired(false);

            builder.HasIndex(e => e.Alias).IsUnique();
        }
    }
}