using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxWeb.Services;
using Microsoft.AspNetCore.Mvc;

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

        [Route("Dashboard")]
        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        // GET api/values
        [Route("test")]
        public ActionResult<IEnumerable<string>> Get()
                {
            _influxDb.Save();

            return new string[] {_influxDb.Now.ToString(), "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _influxDb.Save();
        }

        // PUT api/values/5
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