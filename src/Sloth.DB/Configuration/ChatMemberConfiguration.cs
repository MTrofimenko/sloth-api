using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models;

namespace Sloth.DB.Configuration
{
    public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            builder
                .ToTable("ChatMember", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder
                .Property(x => x.ChatId)
                .HasColumnName("ChatId")
                .IsRequired();

            builder
                .Property(x => x.PublicKey)
                .HasColumnName("PublicKey");

            builder
                .Property(x => x.Status)
                .HasColumnName("Status");

            builder
                .Property(x => x.CreatedOn)
                .HasColumnName("CreatedOn");

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("ModifiedOn");

            builder
                .HasOne(x => x.Chat);

            builder
                .HasOne(x => x.User);
            builder
                .HasMany(x => x.Messages)
                .WithOne(x => x.Sender);
        }
    }
}
