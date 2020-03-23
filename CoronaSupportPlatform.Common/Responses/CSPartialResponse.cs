using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Common.Response
{
    public class CSPartialResponse
    {
        public int ReturnCode { get; set; }

        public int TotalCount { get; set; }

        public int ResultCount { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Html { get; set; }

        public string Html2 { get; set; }

        public string Html3 { get; set; }
    }
}
