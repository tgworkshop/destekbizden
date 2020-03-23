using CoronaSupportPlatform.Common;
using System.ComponentModel.DataAnnotations;

namespace CoronaSupportPlatform.Models
{
    public class Property : Entity
    {
        [Key]
        public int PropertyId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Extra { get; set; }
    }
}
