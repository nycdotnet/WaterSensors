namespace WaterSensors.Models
{
    public record struct SensorReading
    {
        public required DateTimeOffset Timestamp { get; init; }
        public required string SensorId { get; init; }
        /// <summary>
        /// Water pressure measured in Fahrenheit
        /// </summary>
        public float? Temperature { get; init; }
        /// <summary>
        /// Water pressure measured in PSI
        /// </summary>
        public float? Pressure { get; init; }
        public float? pH { get; init; }
        public float? Latitude { get; init; }
        public float? Longitude { get; init; }
    }
}
