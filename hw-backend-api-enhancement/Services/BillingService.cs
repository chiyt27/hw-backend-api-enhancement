using Dapper;
using hw_backend_api_enhancement.Dao;
using hw_backend_api_enhancement.Model;
using hw_backend_api_enhancement.Services.Interface;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Services
{
    public class BillingService : IBillingService
    {
        private DBConnection dbConnection { get; set; }
        private BillDetailAdapter dbAdapter;
        public BillingService(IOptions<DBConnection> _dbConnection)
        {
            dbConnection = _dbConnection.Value;
            dbAdapter = new BillDetailAdapter(_dbConnection.Value);
        }

        public List<BillDetail> Get(Nullable<int> limit, Nullable<int> offset)
		{
            List<BillDetail> result = null;
            try
            {
                result = dbAdapter.Get(limit, offset);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public List<BillDetail> GetCostSumByAccountid(string id)
        {
            List<BillDetail> result = null;
            try
            {
                result = dbAdapter.GetCostSumByAccountid(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public dynamic GetDailyAmountByAccountId(string id)
        {
            try
            {
                List <BillDetail> tmp = dbAdapter.GetDailyAmountByAccountId(id);
                return tmp.GroupBy(o => o.ProductName)
                .ToDictionary(
                    o => o.Key,
                    o => o.ToDictionary(a => a.UsageStartDate, a => a.UsageAmount)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
