using AutoMapper;
using FlightPlanner.Core.DTO;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlightPlanner3rd.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        private readonly IFlightPlannerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;

        public ClientApiController(IFlightPlannerDbContext context, IMapper mapper, IAirportService airportService)
        {
            _context = context;
            _mapper = mapper;
            _airportService = airportService;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult FindAirport(string search)
        {
            var airportStorage = new AirportService(_context);
            var airport = airportStorage.FindAirport(search);
            var mappedAirport = _mapper.Map<AirportResponse>(airport);

            if (mappedAirport == null)
                return NotFound();

            List<AirportResponse> airportList = new List<AirportResponse>();

            airportList.Add(mappedAirport);

            return Ok(airportList);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightById(int id)
        {
            var flightStorage = new FlightService(_context);
            var flight = flightStorage.GetFullFlightById(id);
            var xx = _mapper.Map<FlightResponse>(flight);

            if (flight == null)
                return NotFound();

            return Ok(xx);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlights flight)
        {
            if (flight.From == flight.To)
            {
                return BadRequest();
            }

            var flightStorage = new FlightService(_context);

            return Ok(flightStorage.FindFlight(flight));
        }
    }
}
