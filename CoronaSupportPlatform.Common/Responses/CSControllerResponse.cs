using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Common.Response
{
    public class CSControllerResponse<T>
    {
        public int ReturnCode { get; set; }

        public string Html { get; set; }

        public List<T> Result { get; set; } = new List<T>();
    }
    public class CSControllerResponse
    {
        public int ReturnCode { get; set; }

        public string Html { get; set; }

    }
}
