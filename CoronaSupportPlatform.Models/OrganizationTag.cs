using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class OrganizationTag : Tag
    {
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
    }
}
