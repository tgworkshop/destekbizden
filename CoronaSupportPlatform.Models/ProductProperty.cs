using CoronaSupportPlatform.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class ProductProperty : Property
    {
        public int ProductId { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion

    }
}
