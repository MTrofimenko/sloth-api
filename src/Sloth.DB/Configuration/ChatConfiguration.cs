using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models;

namespace Sloth.DB.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder
                .ToTable("Chat", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property(x => x.Status)
                .HasColumnName("Status");

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
