using hw_backend_api_enhancement.Model;
using hw_backend_api_enhancement.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace hw_backend_api_enhancement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : Controller
    {
        private IBillingService service { get; set; }

        public BillingController(IBillingService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IEnumerable<BillDetail> Get()
        {
            return service.Get(null, null);
        }

        [HttpGet("{limit}/{offset}")]
        public string Get(int limit, int offset)
        {
            return JsonConvert.SerializeObject(service.Get(limit, offset));
        }

        [HttpGet("getCostSumByAccountId/{accountid}")]
        public string getCostSumByAccountId(string accountid)
        {
            return JsonConvert.SerializeObject(service.GetCostSumByAccountid(accountid));
        }

        [HttpGet("getDailyAmountByAccountId{accountid}")]
        public string getDailyAmountByAccountId(string accountid)
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "yyyy/MM/dd";
            return JsonConvert.SerializeObject(service.GetDailyAmountByAccountId(accountid), jsonSettings);
        }

        //[HttpGet("ImportCsv/")]
        //public string ImportCsv([FromBody] string value)
        //{
        //    return "value";
        //}
    }
}
