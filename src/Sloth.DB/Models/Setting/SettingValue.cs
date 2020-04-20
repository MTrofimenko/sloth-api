using System;

namespace Sloth.DB.Models.Setting
{
    public class SettingValue : BaseEntity
    {
        public Guid SettingId { get; set; }
        public long NumberValue { get; set; }
        public string StringValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public bool BooleanValue { get; set; }
        public Guid LookupValueId { get; set; }
    }
}
