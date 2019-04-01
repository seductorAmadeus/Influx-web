using System;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Models
{
    public class WaterInfo
    {
        [InfluxTimestamp] public DateTime Timestamp { get; set; }
        [InfluxTag("water_level")] public double WaterLevel { get; set; }

        [InfluxTag("region")] public string Region { get; set; }
    }
}