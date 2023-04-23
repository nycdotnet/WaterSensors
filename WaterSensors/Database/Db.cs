using System.Data;

namespace WaterSensors.Database
{
    public class Db
    {
        private readonly string connectionString;
        private const string waterSensorsConnectionStringName = "water-sensors";

        public Db(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString(waterSensorsConnectionStringName) ??
                throw new ArgumentNullException($"This application requires a configured connection string with the name \"${waterSensorsConnectionStringName}\".");
        }

        public IDbConnection GetConnection()
        {
            return new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
