using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Configuration
{
    public class CacheConfig
    {
        public static string Provider { get; set; }

        public static TimeSpan DefaultExpirationTime { get; set; }

        public static string Server { get; set; }

        public static string CacheBucket { get; set; }

        public static bool IsEnabled { get; set; }
    }
}
