using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Tenders
{
    public class TenderViewModel : BaseViewModel
    {
        public int TenderId { get; set; }

        public string RefNumber { get; set; }

        public int UserId { get; set; }

        public int? OrganizationId { get; set; }

        //[Required(ErrorMessage = "Talep adını giriniz.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Talebe ait mesaj giriniz")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Talebin teslim edilmesini istediğiniz adresi giriniz")]
        public string Address { get; set; }

        public TenderState State { get; set; }

        public string StateName
        {
            get
            {
                switch (State)
                {
                    case TenderState.New:
                        return "Yeni";
                    case TenderState.InProgress:
                        return "İşleme Alındı";
                    case TenderState.Scheduled:
                        return "Üretim Planında";
                    case TenderState.Produced:
                        return "Üretildi";
                    case TenderState.Procured:
                        return "Tedarik Edildi";
                    case TenderState.Packaged:
                        return "Paketlendi";
                    case TenderState.Invoiced:
                        return "Faturalandı";
                    case TenderState.Delivery:
                        return "Kargoya Teslim Edildi";
                    case TenderState.Delivered:
                        return "Teslim Edildi";
                    case TenderState.Closed:
                        return "Karşılandı";
                    case TenderState.Expired:
                        return "Süresi Doldu";
                    case TenderState.Passive:
                        return "Pasif";
                    case TenderState.Cancelled:
                        return "İptal Edildi";
                    default:
                        return "";
                }
            }
        }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public EntityStatus Status { get; set; }

        public bool IsUpdated { get; set; }

        #region [ Navigation properties ]


        public CSPUser User { get; set; }


        public Organization Organization { get; set; }


        public List<TenderItem> Items { get; set; } = new List<TenderItem>();


        public List<TenderProperty> Properties { get; set; } = new List<TenderProperty>();


        public List<TenderTag> Tags { get; set; } = new List<TenderTag>();

        #endregion

        #region [ Converters ]

        public TenderViewModel From(Tender tender)
        {
            // Set basic fields
            this.TenderId = tender.TenderId;
            this.LongDescription = tender.LongDescription;
            this.ShortDescription = tender.ShortDescription;
            this.RefNumber = tender.RefNumber;
            this.UserId = tender.UserId;
            this.State = tender.State;
            this.Address = tender.Address;
            this.Created = tender.Created.ToLocalTime();
            this.Updated = tender.Updated.GetValueOrDefault().ToLocalTime();
            this.Status = tender.Status;

            // Set the organization
            this.OrganizationId = tender.OrganizationId;
            this.Organization = tender.Organization;

            // Set the user
            this.UserId = tender.UserId;
            this.User = tender.User;

            // Set the items
            this.Items = tender.Items;

            return this;
        }

        #endregion
    }
}