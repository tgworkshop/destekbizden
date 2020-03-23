using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Common
{
    public class BreadcrumbViewModel
    {
        public string PageName { get; set; }

        public Dictionary<string, string> Items { get; set; } = new Dictionary<string, string>();
    }
}