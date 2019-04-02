using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxWeb.Models;
using Vibrant.InfluxDB.Client;

namespace InfluxWeb.Services
{
    public interface IInfluxDB
    {
        //TODO: add new methods
        Task<string> ReadMeasurement(string measurementName);
        void Save(string value, string measurementName);
    }
}