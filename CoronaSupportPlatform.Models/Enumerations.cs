using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    // TENDERS
    public enum TenderState
    {
        [Display(Name = "Yeni")]
        New = 0,

        [Display(Name = "Aktif")]
        Active = 1,

        [Display(Name = "Pasif")]
        Passive = -1,

        [Display(Name = "Süresi Doldu")]
        Expired = -99,

        [Display(Name = "Karşılandı")]
        Closed = 2
    }

    public enum TenderItemState
    {
        [Display(Name = "Yeni")]
        New = 0,

        [Display(Name = "Beklemede")]
        Pending = 1,

        [Display(Name = "Karşılandı")]
        Supplied = 2,

        [Display(Name = "Karşılanamadı")]
        Not_Supplied = 3
    }
}
