using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Flight>
    {
        Airport FindAirport(string searchValue);
    }
}
