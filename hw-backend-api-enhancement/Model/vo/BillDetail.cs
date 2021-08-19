using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Model
{
    public class BillDetail
    {

        private Nullable<decimal> _unblendedCost = null;
        private Nullable<decimal> _unblendedRate = null;
        private Nullable<decimal> _usageAmount = null;

        public string PayerAccountId { get; set; }
        public Nullable<decimal> UnblendedCost
        {
            get { return _unblendedCost; }
            set { _unblendedCost = Convert.ToDecimal(value); }
        }
        public Nullable<decimal> UnblendedRate
        {
            get { return _unblendedRate; }
            set { _unblendedRate = Convert.ToDecimal(value); }
        }
        public string UsageAccountId { get; set; }
        public Nullable<decimal> UsageAmount
        {
            get { return _usageAmount; }
            set { _usageAmount = Convert.ToDecimal(value); }
        }

        public DateTime UsageStartDate { get; set; }
        public DateTime UsageEndDate { get; set; }
        public string ProductName { get; set; }

    }
}
