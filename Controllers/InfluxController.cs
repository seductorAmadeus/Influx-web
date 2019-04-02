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

        [Route("measurement/{measurementName}")]
        public string GetMeasurement(string measurementName)
        {
            return _influxDb.ReadMeasurement(measurementName).Result;
        }

        [Route("measurement/{measurementName}")]
        [HttpPost]
        public void Post([FromBody] string value, string measurementName)
        {
            _influxDb.Save(value, measurementName);
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