using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Common.Services
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            PreProcessingTook = 0;
            ServiceTook = 0;
            TotalTook = 0;
        }

        public long PreProcessingTook { get; set; }

        public long ServiceTook { get; set; }

        public long TotalTook { get; set; }

        public ServiceResponseTypes Type { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

        public List<string> Errors { get; set; }

        public List<ServiceLogRecord> LogRecords { get; set; }

        public ServiceResponse InnerResponse { get; set; }

    }

    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            PreProcessingTook = 0;
            ServiceTook = 0;
            TotalTook = 0;
            Result = new List<T>();
            Errors = new List<string>();
        }

        public List<T> Result { get; set; }

        public long PreProcessingTook { get; set; }

        public long ServiceTook { get; set; }

        public long TotalTook { get; set; }

        public ServiceResponseTypes Type { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

        public int ResultCount
        {
            get
            {
                return this.Result != null ? this.Result.Count() : 0;
            }
        }

        public int TotalCount { get; set; }

        public int CurrentPageIndex { get; set; }

        public List<string> Errors { get; set; }

        public List<ServiceLogRecord> LogRecords { get; set; }

        public ServiceResponse<T> InnerResponse { get; set; }
    }
}
