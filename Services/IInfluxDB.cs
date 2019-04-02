using System;
using System.Threading.Tasks;
using InfluxWeb.Models;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Services
{
    public interface IInfluxDB
    {
        //TODO: add new methods
        Task<WaterInfo[]> ReadMeasurement(string measurementName);
        void Save(); 
    }
}