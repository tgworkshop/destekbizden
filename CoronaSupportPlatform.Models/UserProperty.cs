using CoronaSupportPlatform.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class UserProperty : Property
    {
        public int UserId { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("UserId")]
        public CSPUser User { get; set; }

        #endregion

    }
}
