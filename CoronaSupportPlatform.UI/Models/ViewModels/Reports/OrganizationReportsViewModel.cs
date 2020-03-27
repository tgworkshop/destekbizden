using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.Data;
using CoronaSupportPlatform.UI.Models.ViewModels.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Reports
{
    public class OrganizationReportsViewModel : BaseViewModel
    {
        public List<OrganizationViewModel> Organizations { get; set; } = new List<OrganizationViewModel>();
    }
}