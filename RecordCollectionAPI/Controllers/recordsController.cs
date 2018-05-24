using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RecordCollectionAPI.Controllers
{
    [RoutePrefix("api/records")]
    public class recordsController : ApiController
    {
        MemoryCache cache = MemoryCache.Default;

        [Route("{sortType}")]
        public List<Record> GetByGender(string sortType)
        {
            var cacheObj = cache.Get("recordStore");
            List<Record> recordStore = new List<Record>();
            if (cacheObj != null)
            {
                recordStore = (List<Record>)cacheObj;
            }
            List<Record> sortedRecords = new List<Record>();
            sortedRecords = Record.fileSort(sortType, recordStore);
            return sortedRecords;
        }
        
        public string Post([FromBody] string value)
        {
            bool success = Record.AddRecord(value, cache);

            if (success)
                return "Record added successfully!";
            else
                return "Record was not added!";
        }
    }
}
