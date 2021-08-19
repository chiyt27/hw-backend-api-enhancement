using Dapper;
using hw_backend_api_enhancement.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Dao
{
    public class BillDetailAdapter
    {
        private DBConnection dbConnectionc;

        public BillDetailAdapter(DBConnection _dbConnectionc)
        {
            dbConnectionc = _dbConnectionc;
        }

        public string DBSource { get; }

        public List<BillDetail> Get(Nullable<int > limit, Nullable<int> offset)
        {
            using (var connection = new SqliteConnection(dbConnectionc.DBSource))
            {
                string cmd = @"select 
                        [bill/PayerAccountId] as PayerAccountId, 
                        cast(ROUND([lineItem/UnblendedCost],10) as decimal) as UnblendedCost, 
                        cast(ROUND([lineItem/UnblendedRate],10) as decimal) as UnblendedRate, 
                        [lineItem/UsageAccountId] as UsageAccountId, 
                        cast(ROUND([lineItem/UsageAmount],10) as decimal) as UsageAmount, 
                        [lineItem/UsageStartDate] as UsageStartDate,
                        [lineItem/UsageEndDate] as UsageEndDate, 
                        [product/ProductName] as ProductName
                    from bill_detail";
                if (limit != null && limit > 0) cmd += (" limit " + limit.ToString());
                if (offset != null && offset > 0) cmd += (" offset " + offset.ToString());

                return connection.Query<BillDetail>(cmd).ToList();
            }
        }

        public List<BillDetail> GetCostSumByAccountid(string id)
        {
            using (var connection = new SqliteConnection(dbConnectionc.DBSource))
            {

                string cmd = string.Format(@"select 
                            [product/ProductName] as ProductName, 
                            sum([lineItem/UnblendedCost]) as UnblendedCost
                        from bill_detail
                        where [lineItem/UsageAccountId] = '"+ id + @"'
                        group by [product/ProductName]");
                return connection.Query<BillDetail>(cmd).ToList();
            }
        }

        public List<BillDetail> GetDailyAmountByAccountId(string id)
        {
            return new List<BillDetail>() {
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now.AddDays(-5), UsageAmount = 600},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now.AddDays(-4), UsageAmount = 500},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now.AddDays(-3), UsageAmount = 400},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now.AddDays(-2), UsageAmount = 300},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now.AddDays(-1), UsageAmount = 200},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = DateTime.Now, UsageAmount = 100},
                new BillDetail(){ ProductName ="AWS Direct Connect", UsageStartDate = new DateTime(), UsageAmount = 100},
                new BillDetail(){ ProductName ="Amazon Route 53", UsageStartDate = DateTime.Now.AddDays(-1), UsageAmount = 2222},
                new BillDetail(){ ProductName ="Amazon Route 53", UsageStartDate = DateTime.Now, UsageAmount = 1111}
            };
            /*using (var connection = new SqliteConnection(dbConnectionc.DBSource))
            {
                string cmd = string.Format(@"select 
                            [product/ProductName] as ProductName, 
                            sum([lineItem/UnblendedCost]) as UnblendedCost
                        from bill_detail
                        where [lineItem/UsageAccountId] = '" + id + @"'
                        group by [product/ProductName]");
                return connection.Query<BillDetail>(cmd).ToList();
            }*/
        }
    }
}
