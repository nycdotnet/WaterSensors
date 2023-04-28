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

        [HttpGet]
        public async Task<IEnumerable<SensorReading>> GetHistory([FromQuery] GetHistoryQuery request)
        {
            request.Validate();

            using var conn = dbProvider.GetConnection();

            var sql = $"""
                {GetSensoryHistorySqlBase}
                {(request.AtOrAfter.HasValue ? " AND timestamp >= @AtOrAfter" : "")}
                {(request.Before.HasValue ? " AND timestamp <= @Before" : "")}
                ORDER BY timestamp DESC
                {(request.Count.HasValue ? " LIMIT @Count" : " LIMIT 100")};
                """;
            return await conn.QueryAsync<SensorReading>(sql, request);
        }

        private const string GetSensoryHistorySqlBase = """
            SELECT sensor_id as SensorId, timestamp, temperature, pressure, pH, latitude, longitude
            FROM public.reading WHERE sensor_id = @SensorId
            """;

        public record GetHistoryQuery
        {
            public string? SensorId { get; init; }
            public DateTimeOffset? AtOrAfter { get; init; }
            public DateTimeOffset? Before { get; init; }
            public int? Count { get; init; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(SensorId))
                {
                    throw new ArgumentNullException("SensorId is required.");
                }
            }
        }
    }
}
