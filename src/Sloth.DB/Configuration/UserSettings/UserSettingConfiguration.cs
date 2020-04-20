using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sloth.DB.Models.ChatMemberSettings;
using Sloth.DB.Models.UserSettings;

namespace Sloth.DB.Configuration.UserSettings
{
    public class UserSettingConfiguration : IEntityTypeConfiguration<UserSetting>
    {
        public void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            builder
                .ToTable("UserSetting", "dbo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");

            builder
                .Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasColumnName("Description");

            builder
                .Property(x => x.Type)
                .HasColumnName("Type");

            builder
                .Property(x => x.IsActive)
                .HasColumnName("IsActive");

            builder
                .Property(x => x.CreatedOn)
                .HasColumnName("CreatedOn");

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("ModifiedOn");            
        }
    }
}
