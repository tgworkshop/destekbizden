using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Organizations
{
    public class OrganizationListViewModel
    {
        public List<OrganizationViewModel> Organizations { get; set; } = new List<OrganizationViewModel>();
    }
}