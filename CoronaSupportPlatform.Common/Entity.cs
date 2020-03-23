using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Common
{
    public class Entity
    {
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public EntityStatus Status { get; set; }
    }
}
