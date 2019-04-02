using System;
using System.Configuration;
using System.Threading.Tasks;
using InfluxWeb.Models;
using InfluxWeb.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Services
{
    public class InfluxDbService : IInfluxDB
    {
        private AppSettings AppSettings { set; get; }

        public InfluxDbService(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
        }

        private static async Task ShouldWriteTypedRowsToDatabase(string db, InfluxClient client)
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

        private async Task<WaterInfo[]> ShouldQueryTypedData(string db, InfluxClient client,
            string measurementName)
        {
            var resultSet = await client.ReadAsync<WaterInfo>(db, "SELECT * FROM " + measurementName);

            // resultSet will contain 1 result in the Results collection (or multiple if you execute multiple queries at once)
            var result = resultSet.Results[0];

            // result will contain 1 series in the Series collection (or potentially multiple if you specify a GROUP BY clause)
            var series = result.Series[0].Rows.ToArray();

            // series.Rows will be the list of ComputerInfo that you queried for
         
            return series;
        }

        public async Task<WaterInfo[]> ReadMeasurement(string measurementName)
        {
            string db = AppSettings.InfluxDatabase;
            string host = AppSettings.InfluxHostName;

            var client = new InfluxClient(new Uri(host));

            return await ShouldQueryTypedData(db, client, measurementName);
        }

        public async void Save()
        {
            string db = AppSettings.InfluxDatabase;
            string host = AppSettings.InfluxHostName;
            
            var client = new InfluxClient(new Uri(host));

            await ShouldWriteTypedRowsToDatabase(db, client);
        }
    }
}