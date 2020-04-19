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
        New = 0,

        InProgress = 10,

        Scheduled = 11,

        Produced = 12,

        Procured = 13,

        Packaged = 20,

        Invoiced = 21,

        Delivery = 31,

        Delivered = 32,

        Closed = 40,

        Passive = -1,

        Expired = -99,

        Cancelled = -999,
    }

    public enum TenderChannel
    {
        Platform = 0,

        SM_Facebook = 10,

        SM_Twitter = 11,

        SM_Instagram = 12,

        Phone = 20,

        Email = 30
    }

    public enum TenderPriority
    {
        Low = -10,

        Medium = 0,

        High = 10,

        Urgent = 20,

        Critical = 30
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

    public enum TenderNoteType
    {
        Public = 0,

        Administrative = 1,
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
