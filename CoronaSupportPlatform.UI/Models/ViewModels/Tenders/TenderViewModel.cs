﻿using CoronaSupportPlatform.Common;
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

        [Required(ErrorMessage = "Talep adını giriniz.")]
        public string ShortDescription { get; set; }


        [Required(ErrorMessage = "Talebe ait mesaj giriniz")]
        public string LongDescription { get; set; }


        public TenderState State { get; set; }

        public string StateName
        {
            get
            {
                switch (State)
                {
                    case TenderState.New:
                        return "Yeni";
                    case TenderState.Active:
                        return "Aktif";
                    case TenderState.Closed:
                        return "Kapandı";
                    case TenderState.Expired:
                        return "Süresi Doldu";
                    case TenderState.Passive:
                        return "Pasif";
                    default:
                        return "";
                }
            }
        }


        public int ProductId { get; set; }


        public int Quantity { get; set; }

        public string ProductName { get; set; }

        [Required(ErrorMessage = "En az bir ürüne ait adet bilgisi giriniz.")]
        public string ProductQuantities { get; set; }
        
        public List<Product> Products { get; set; } = new List<Product>();

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public EntityStatus Status { get; set; }

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
            this.Created = tender.Created.ToLocalTime();
            this.Updated = tender.Updated.GetValueOrDefault().ToLocalTime();
            this.Status = tender.Status;

            // Set the organization
            this.OrganizationId = tender.OrganizationId;
            this.Organization = tender.Organization;

            // Set the items
            this.Items = tender.Items;

            return this;
        }

        #endregion
    }
}