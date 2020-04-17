namespace Sloth.DB.Models
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
