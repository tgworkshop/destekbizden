using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSupportPlatform.Models
{
    public class TenderItemProperty : Property
    {
        public int ItemId { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("ItemId")]
        public virtual TenderItem TenderItem { get; set; }

        #endregion
    }
}