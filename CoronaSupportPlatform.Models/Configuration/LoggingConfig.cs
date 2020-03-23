using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaSupportPlatform.Models.Configuration
{
    public class LoggingConfig
    {
        public static string Channel { get; set; }

        public static string Mode { get; set; }

        public static string LogDirectory { get; set; }

        public static string LogBucket { get; set; }

        public static string CloudService { get; set; }

        public static string CloudServiceKey { get; set; }
    }
}
