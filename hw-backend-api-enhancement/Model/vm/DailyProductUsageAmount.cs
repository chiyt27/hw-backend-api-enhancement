using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Model
{
    public class DailyProductUsageAmount
    {
        public string ProductName { get; set; }
        KeyValuePair<DateTime, decimal> DailyAmount { get; set; }
    }
}
