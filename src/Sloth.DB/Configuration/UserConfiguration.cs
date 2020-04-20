using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models;

namespace Sloth.DB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.Login)
                .HasColumnName("Login")
                .IsRequired();

            builder
                .Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasColumnName("Password");

            builder
                .Property(x => x.IsActive)
                .HasColumnName("IsActive");

            builder
                .Property(x => x.LogoId)
                .HasColumnName("LogoId");

            builder
                .Property(x => x.CreatedOn)
                .HasColumnName("CreatedOn");

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("ModifiedOn");

            builder
                .HasOne(x => x.Logo);
        }
    }
}
