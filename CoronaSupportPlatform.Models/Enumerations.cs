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

    public enum TenderEventType
    {
        Created = 0,

        Assigned = 1,

        Accepted = 2,
    }

    // ASSIGNMENTS
    public enum AssignmentState
    {
        [Display(Name = "Yeni")]
        New = 0,

        [Display(Name = "İşleme Alındı")]
        InProgress = 1,

        [Display(Name = "Üretimde")]
        InProduction = 2,

        [Display(Name = "Teslimatta")]
        Delivery = 3,

        [Display(Name = "Teslim Edildi")]
        Delivered = 4,

        [Display(Name = "İptal Edildi")]
        Expired = -99,
    }


    // ORGANIZATIONS
    public enum OrganizationType
    {
        [Display(Name = "Bireysel")]
        None = 0,

        [Display(Name = "Hastane")]
        Hospital = 10,

        [Display(Name = "İl Sağlık Müdürlüğü")]
        RegionalHealthAdministration = 11,

        [Display(Name = "Tedarikçi")]
        Supplier = 20
    }
}
