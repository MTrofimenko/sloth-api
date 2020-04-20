using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models.ChatMemberSettings;

namespace Sloth.DB.Configuration.ChatMemberSettings
{
    public class ChatMemberDefSettingValueConfiguration : IEntityTypeConfiguration<ChatMemberDefSettingValue>
    {
        public void Configure(EntityTypeBuilder<ChatMemberDefSettingValue> builder)
        {
            builder
                .ToTable("ChatMemberDefSettingValue", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.SettingId)
                .HasColumnName("SettingId")
                .IsRequired();

            builder
                .Property(x => x.NumberValue)
                .HasColumnName("NumberValue");

            builder
                .Property(x => x.StringValue)
                .HasColumnName("StringValue");

            builder
                .Property(x => x.DateTimeValue)
                .HasColumnName("DateTimeValue");

            builder
                .Property(x => x.BooleanValue)
                .HasColumnName("BooleanValue");

            builder
                .Property(x => x.LookupValueId)
                .HasColumnName("LookupValueId");

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
