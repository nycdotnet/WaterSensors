# WaterSensors

This project demonstrates a hypothetical IoT water sensor system using a .NET 7 ASP.NET Core WebAPI project.  It is very simple and could potentially be optimized a lot further.

To get the DB working, with Docker installed, run `docker-compose up -d db` and then `docker-compose run flyway`.

To run the load test, with k6 and Node.js installed, run `npm run perf`.

TODO:
  * Add Lat/Long to readings
  * Add Web visualizations
  * Investigate streaming API such as web sockets
