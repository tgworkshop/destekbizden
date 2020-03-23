using CoronaSupportPlatform.Common;
using System.ComponentModel.DataAnnotations;

namespace CoronaSupportPlatform.Models
{
    public class Tag : Entity
    {
        [Key]
        public int TagId { get; set; }

        public TagType Type { get; set; }

        public string Value { get; set; }
    }
}
