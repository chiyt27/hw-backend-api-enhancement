using hw_backend_api_enhancement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Services.Interface
{
    public interface IBillingService
    {
        List<BillDetail> Get(Nullable<int> limit, Nullable<int> offset);
        List<BillDetail> GetCostSumByAccountid(string id);
        dynamic GetDailyAmountByAccountId(string id);
    }
}
