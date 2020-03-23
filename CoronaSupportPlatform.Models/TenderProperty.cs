using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSupportPlatform.Models
{
    public class TenderProperty : Property
    {
        public int TenderId { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }

        #endregion
    }
}