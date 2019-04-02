using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxWeb.Models;
using InfluxWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Controllers
{
    [Route("api")]
    public class InfluxController : Controller
    {
        private readonly IInfluxDB _influxDb;

        public InfluxController(IInfluxDB influxDb)
        {
            _influxDb = influxDb;
        }

        [Route("test")]
        public ActionResult<IEnumerable<string>> Get()
        {
            _influxDb.Save();
            return new string[] {"test"};
        }

        [Route("measurement/{measurementName}")]
        public ActionResult<IEnumerable<string>> GetMeasurement(string measurementName)
        {
            var t = _influxDb.ReadMeasurement(measurementName);
       
            return t.ToString().Split();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            _influxDb.Save();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}