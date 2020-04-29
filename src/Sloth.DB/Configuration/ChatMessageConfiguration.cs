using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models;

namespace Sloth.DB.Configuration
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .ToTable("ChatMessage", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.ChatMemberId)
                .HasColumnName("ChatMemberId")
                .IsRequired();

            builder
                .Property(x => x.Message)
                .HasColumnName("Message");

            builder
                .Property(x => x.ReplyToMessageId)
                .HasColumnName("ReplyToMessageId");

            builder
                .Property(x => x.ForwardFromUserId)
                .HasColumnName("ForwardFromUserId");

            builder
               .Property(x => x.CreatedOn)
               .HasColumnName("CreatedOn");

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("ModifiedOn");

            builder
                .HasOne(x => x.Sender);
            builder
                .HasOne(x => x.ForwardFromUser);

            builder
                .HasOne(x => x.ReplyToMessage);
        }
    }
}
