using Microsoft.AspNetCore.Mvc;
using WaterSensors.Models;

namespace WaterSensors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly ILogger<ReadingsController> _logger;

        public ReadingsController(ILogger<ReadingsController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        public Task PostReading([FromBody] SensorReading reading)
        {
            //_logger.LogInformation($"Got a reading: Sensor ID: {reading.SensorId} at timestamp {reading.Timestamp.ToLocalTime()} with Temp: {reading.Temperature}, Press: {reading.Pressure}, pH: {reading.pH}.");

            return Task.CompletedTask;
        }
    }
}
