using System;

namespace Sloth.DB.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
