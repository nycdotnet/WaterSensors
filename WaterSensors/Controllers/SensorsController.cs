using Dapper;
using Microsoft.AspNetCore.Mvc;
using WaterSensors.Database;
using WaterSensors.Models;

namespace WaterSensors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly Db dbProvider;

        public SensorsController(Db dbProvider) {
            this.dbProvider = dbProvider;
        }

        [HttpGet("{SensorId}")]
        public async Task<IEnumerable<SensorReading>> GetHistory(string SensorId, [FromQuery] GetHistoryQuery request)
        {
            using var conn = dbProvider.GetConnection();

            var sql = $"""
                {GetSensorHistorySqlBase}
                {(request.AtOrAfter.HasValue ? " AND timestamp >= @AtOrAfter" : "")}
                {(request.Before.HasValue ? " AND timestamp <= @Before" : "")}
                ORDER BY timestamp DESC
                {(request.Count.HasValue ? " LIMIT @Count" : " LIMIT 100")};
                """;
            return await conn.QueryAsync<SensorReading>(sql, request with { SensorId = SensorId });
        }

        [HttpGet]
        public async Task<IEnumerable<SensorReading>> GetCurrent()
        {
            using var conn = dbProvider.GetConnection();
            var currentReadings = await conn.QueryAsync<SensorReading>(
                GetCurrentSql, new { Timestamp = DateTime.UtcNow.AddSeconds(-30) });

            return currentReadings
                .GroupBy(r => r.SensorId)
                .Select(g => new SensorReading {
                    SensorId = g.Key,
                    Timestamp = g.Max(x => x.Timestamp),
                    Temperature = g.Average(x => x.Temperature),
                    Pressure = g.Average(x => x.Pressure),
                    pH = g.Average(x => x.pH),
                    Latitude = g.Average(x => x.Latitude),
                    Longitude = g.Average(x => x.Longitude)
                });
        }

        private const string GetCurrentSql = """
            SELECT sensor_id as SensorId, timestamp, temperature, pressure, ph, latitude, longitude
            FROM public.reading
            WHERE timestamp > @Timestamp
            LIMIT 1000
            """;

        private const string GetSensorHistorySqlBase = """
            SELECT sensor_id as SensorId, timestamp, temperature, pressure, pH, latitude, longitude
            FROM public.reading WHERE sensor_id = @SensorId
            """;

        public record GetHistoryQuery
        {
            public string? SensorId { get; init; }
            public DateTimeOffset? AtOrAfter { get; init; }
            public DateTimeOffset? Before { get; init; }
            public int? Count { get; init; }
        }
    }
}
