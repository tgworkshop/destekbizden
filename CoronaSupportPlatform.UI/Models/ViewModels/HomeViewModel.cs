using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<TenderViewModel> Tenders { get; set; } = new List<TenderViewModel>();
    }
}