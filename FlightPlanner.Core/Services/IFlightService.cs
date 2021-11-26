using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlightById(int id);
        bool Exists(Flight flight);
        void DeleteFlight(int id);
        PageResult FindFlight(SearchFlights flight);
    }
}
