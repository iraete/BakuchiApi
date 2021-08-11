using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class WagerEntityTypeConfiguration: IEntityTypeConfiguration<Wager>
    {
        public void Configure(EntityTypeBuilder<Wager> builder)
        {
            builder.HasKey(u => new { u.UserId, u.PoolId });

            builder.HasOne(w => w.User)
                .WithMany(us => us.Wagers)
                .HasForeignKey(w => w.UserId)
                .IsRequired()
                .HasForeignKey(w => w.DiscordId);
            
            builder.HasOne(w => w.Pool)
                .WithMany(p => p.Wagers)
                .HasForeignKey(w => w.PoolId)
                .IsRequired();
        }
    }
}