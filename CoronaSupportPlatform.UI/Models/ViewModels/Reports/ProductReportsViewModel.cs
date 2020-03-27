using CoronaSupportPlatform.UI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Reports
{
    public class ProductReportsViewModel : BaseViewModel
    {
        public List<TenderProductQuantity> ProductQuantities { get; set; } = new List<TenderProductQuantity>();
    }
}