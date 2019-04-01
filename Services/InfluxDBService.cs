using System;
using System.Threading.Tasks;
using InfluxWeb.Models;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Services
{
    public class InfluxDbService : IInfluxDB
    {
        private static async Task MainAsync()
        {
            // TODO: get params from env files
            const string influxHost = "http://localhost:8086";
            const string databaseName = "water_info";

            var client = new InfluxClient(new Uri(influxHost));


            await Should_Write_Typed_Rows_To_Database(databaseName, client);
        }

        public static async Task Should_Write_Typed_Rows_To_Database(string db, InfluxClient client)
        {
            var infos = CreateTypedRowsStartingAt(new DateTime(2010, 1, 1, 1, 1, 1, DateTimeKind.Utc), 500);
            await client.WriteAsync(db, "myMeasurementName", infos);
        }

        private static WaterInfo[] CreateTypedRowsStartingAt(DateTime start, int rows)
        {
            var rng = new Random();
            var regions = new[] {"west-eu", "north-eu", "west-us", "east-us", "asia"};

            var timestamp = start;
            var infos = new WaterInfo[rows];
            for (var i = 0; i < rows; i++)
            {
                var waterLevel = rng.NextDouble();
                var region = regions[rng.Next(regions.Length)];

                var info = new WaterInfo {Timestamp = timestamp, WaterLevel = waterLevel, Region = region};
                infos[i] = info;

                timestamp = timestamp.AddSeconds(1);
            }

            return infos;
        }

        public DateTime Now { get; }

        public void Save()
        {
            //TODO: change this
            MainAsync();
        }
    }
}