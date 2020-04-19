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
    public class TenderNote: Entity
    {
        [Key]
        public int NoteId { get; set; }

        public int TenderId { get; set; }

        public TenderNoteType Type { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }

        #endregion

    }
}
