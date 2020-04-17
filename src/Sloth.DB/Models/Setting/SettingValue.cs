using System;

namespace Sloth.DB.Models.Setting
{
    public class SettingValue : BaseEntity
    {
        public long NumberValue { get; set; }
        public string StringValue { get; set; }
        public DateTime DatetimeValue { get; set; }
        public bool BooleanValue { get; set; }
        public Guid LookupValueId { get; set; }
    }
}
