using CoronaSupportPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class Assignment : Entity
    {
        [Key]
        public int AssignmentId { get; set; }

        public int TenderId { get; set; }

        public int? TenderItemId { get; set; }

        public int OrganizationId { get; set; }

        public AssignmentState State { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }

        [ForeignKey("TenderItemId")]
        public virtual TenderItem TenderItem { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        #endregion
    }
}
