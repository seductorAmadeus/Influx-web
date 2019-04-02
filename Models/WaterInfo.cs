using System;
using System.Xml.Serialization;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Models
{
    public class WaterInfo
    {
        [XmlElement(ElementName = "Timestamp")]
        [InfluxTimestamp]
        public DateTime Timestamp { get; set; }

        [XmlElement(ElementName = "WaterLevel")]
        [InfluxTag("water_level")]
        public double WaterLevel { get; set; }

        [XmlElement(ElementName = "Region")]
        [InfluxTag("region")]
        public string Region { get; set; }
    }
}