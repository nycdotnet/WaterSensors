﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using WaterSensors.Database;
using WaterSensors.Models;

namespace WaterSensors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly Db dbProvider;

        public ReadingsController(Db dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        [HttpPost]
        public async Task PostReading([FromBody] SensorReading reading)
        {
            using var conn = dbProvider.GetConnection();
            await conn.ExecuteAsync(InsertReadingSql, reading);
        }

        private const string InsertReadingSql = """
            INSERT INTO public.reading (sensor_id, timestamp, temperature, pressure, ph, latitude, longitude)
            VALUES (@SensorId, @Timestamp, @Temperature, @Pressure, @pH, @Latitude, @Longitude);
            """;
    }
}
