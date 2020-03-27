using CoronaSupportPlatform.UI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Reports
{
    public class CityReportsViewModel : BaseViewModel
    {
        public List<TenderProductQuantity> TenderQuantities { get; set; } = new List<TenderProductQuantity>();
    }
}