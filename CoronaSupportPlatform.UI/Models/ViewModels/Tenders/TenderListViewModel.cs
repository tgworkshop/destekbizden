using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Tenders
{
    public class TenderListViewModel : BaseViewModel
    {
        public List<TenderViewModel> Tenders { get; set; } = new List<TenderViewModel>();
    }
}