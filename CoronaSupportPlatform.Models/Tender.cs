using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class Tender : Entity
    {
        [Key]
        public int TenderId { get; set; }

        public string RefNumber { get; set; }

        public int UserId { get; set; }

        public int? OrganizationId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Address { get; set; }

        public TenderState State { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("UserId")]
        public virtual CSPUser User { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        public virtual List<TenderItem> Items { get; set; } = new List<TenderItem>();

        public virtual List<TenderProperty> Properties { get; set; } = new List<TenderProperty>();

        public virtual List<TenderTag> Tags { get; set; } = new List<TenderTag>();

        #endregion
    }
}
