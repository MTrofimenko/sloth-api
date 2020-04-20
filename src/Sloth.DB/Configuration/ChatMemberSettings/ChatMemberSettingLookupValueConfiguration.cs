using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models.ChatMemberSettings;

namespace Sloth.DB.Configuration.ChatMemberSettings
{
    public class ChatMemberSettingLookupValueConfiguration : IEntityTypeConfiguration<ChatMemberSettingLookupValue>
    {
        public void Configure(EntityTypeBuilder<ChatMemberSettingLookupValue> builder)
        {
            builder
                .ToTable("ChatMemberSettingLookupValue", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.SettingId)
                .HasColumnName("SettingId")
                .IsRequired();

            builder
                .Property(x => x.Value)
                .HasColumnName("Value");

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
                .HasOne(x => x.Setting);
        }
    }
}
