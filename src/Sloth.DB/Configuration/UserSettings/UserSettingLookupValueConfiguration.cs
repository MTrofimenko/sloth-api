using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models.UserSettings;

namespace Sloth.DB.Configuration.UserSettings
{
    public class UserSettingLookupValueConfiguration : IEntityTypeConfiguration<UserSettingLookupValue>
    {
        public void Configure(EntityTypeBuilder<UserSettingLookupValue> builder)
        {
            builder
                .ToTable("UserSettingLookupValue", "dbo")
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
