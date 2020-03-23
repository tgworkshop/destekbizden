using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System.Collections.Generic;

namespace CoronaSupportPlatform.UI.Models.ViewModels
{
    public class BaseViewModel
    {
        public string CurrentCulture { get; set; }

        public CSPUser CurrentUser { get; set; }

        public Organization CurrentOrganization { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public bool HasErrors { get; set; }

        public string Result { get; set; }
    }
}