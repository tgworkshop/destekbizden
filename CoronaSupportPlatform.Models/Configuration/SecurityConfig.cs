using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Configuration
{
    public class SecurityConfig
    {
        public static string Salt { get; set; }

        public static string Encryption { get; set; }
    }
}
