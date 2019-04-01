using System;

namespace InfluxWeb.Services
{
    public interface IInfluxDB
    {
        DateTime Now { get; }

        void Save();
    }
}