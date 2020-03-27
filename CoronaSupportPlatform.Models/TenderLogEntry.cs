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
    [Table("TenderLog")]
    public class TenderLogEntry: Entity
    {
        [Key]
        public int EntryId { get; set; }

        public int TenderId { get; set; }

        public TenderEventType Type { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }

        #endregion

    }
}
