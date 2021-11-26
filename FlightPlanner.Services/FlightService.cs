using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return Query().Any(f => f.ArrivalTime == flight.ArrivalTime
                                    && f.DepartureTime == flight.DepartureTime
                                    && f.Carrier == flight.Carrier
                                    && f.From.AirportCode == flight.From.AirportCode
                                    && f.To.AirportCode == flight.To.AirportCode);
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights.Include(a => a.To)
                    .Include(a => a.From)
                    .FirstOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Flights.Remove(_context.Flights.Include(a => a.From)
                    .Include(a => a.To).First(s => s.Id == id));
                _context.SaveChanges();
            }
        }

        public PageResult FindFlight(SearchFlights flight)
        {
            var flights = _context.Flights.SingleOrDefault(f =>
                f.From.AirportCode == flight.From && f.To.AirportCode == flight.To
                                                  && f.DepartureTime.Substring(0, 10) == flight.DepartureDate);

            List<int?> actingList = new List<int?>();
            actingList.Add(flights?.Id);

            PageResult value = new PageResult();
            value.Items = new List<Flight>();

            if (flights == null)
            {
                value.Page = 0;
                value.TotalItems = 0;
                var targetType = value.Items.Select(fx => new int()).ToList();
                targetType.Add(0);
            }
            else
            {
                value.Page = 0;
                value.TotalItems = actingList.Count;
                value.Items.Add(flights);
            }

            return value;
        }
    }
}
