using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakuchiApi.Models.Configuration
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(u => u.DiscordId).IsRequired(false);
            builder.Property(u => u.Name).IsRequired();
            builder.HasIndex(u => new { u.DiscordId }).IsUnique();        
        }
    }
}