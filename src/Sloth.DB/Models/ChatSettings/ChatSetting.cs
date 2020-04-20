using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.ChatSettings
{
    public class ChatSetting : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
