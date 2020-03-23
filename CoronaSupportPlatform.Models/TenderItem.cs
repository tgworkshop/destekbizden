using CoronaSupportPlatform.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSupportPlatform.Models
{
    public class TenderItem : Entity
    {
        [Key]
        public int ItemId { get; set; }

        public int TenderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public TenderItemState State { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public virtual List<TenderItemProperty> Properties { get; set; } = new List<TenderItemProperty>();

        public virtual List<TenderItemTag> Tags { get; set; } = new List<TenderItemTag>();

        #endregion
    }
}