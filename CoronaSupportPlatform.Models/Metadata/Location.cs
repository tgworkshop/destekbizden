using CoronaSupportPlatform.Common;
using System.ComponentModel.DataAnnotations;

namespace CoronaSupportPlatform.Models.Metadata
{
    public class Location : Entity
    {
        [Key]
        public int LocationId { get; set; }

        public string Code { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Fullname { get; set; }
    }
}
