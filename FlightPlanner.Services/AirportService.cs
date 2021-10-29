using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System;
using System.Linq;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Flight>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Airport FindAirport(string searchValue)
        {
            var airportValues = searchValue.Trim().ToLower();

            if (!String.IsNullOrEmpty(airportValues))
            {
                if (_context.Airports.Count() != 0)
                {
                    var airport = _context.Airports.FirstOrDefault(f =>
                        f.AirportCode.ToLower().Contains(airportValues)
                        || f.Country.ToLower().Contains(airportValues)
                        || f.City.ToLower().Contains(airportValues));

                    return airport;
                }

                return null;
            }

            return null;
        }
    }
}
