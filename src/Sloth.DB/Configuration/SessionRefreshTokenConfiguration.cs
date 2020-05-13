using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models;

namespace Sloth.DB.Configuration
{
    public class SessionRefreshTokenConfiguration : IEntityTypeConfiguration<SessionRefreshToken>
    {
        public void Configure(EntityTypeBuilder<SessionRefreshToken> builder)
        {
            builder
                .ToTable("SessionRefreshToken", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder
                .Property(x => x.Token)
                .HasColumnName("Token")
                .IsRequired();

            builder
                .Property(x => x.ExpiredTime)
                .HasColumnName("ExpiredTime")
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .HasColumnName("IsActive");

            builder
                .Property(x => x.CreatedOn)
                .HasColumnName("CreatedOn");

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("ModifiedOn");

            builder
                .HasOne(x => x.User);
        }
    }
}