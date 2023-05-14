# WaterSensors

This project demonstrates a hypothetical IoT water sensor system using a .NET 7 ASP.NET Core WebAPI project.  It is very simple and could potentially be optimized a lot further.

This project uses a Postgres database with schema controlled by Flyway.  To get the DB working, with Docker installed, run `docker-compose up -d db` and then `docker-compose run flyway`.

To simulate the sensors uploading data, with k6 and Node.js installed, run `npm run perf`.

TODO:
  * Add Web visualizations
  * Investigate streaming API such as web sockets
