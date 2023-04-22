namespace WaterSensors.Models
{
    public record struct SensorReading
    {
        public required DateTimeOffset Timestamp { get; init; }
        public required string SensorId { get; init; }
        public float? Temperature { get; init; }
        public float? Pressure { get; init; }
        public float? pH { get; init; }

    }
}
