using AutoMapper;
using FlightPlanner.Core.DTO;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlightPlanner3rd.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;

        public ClientApiController(IMapper mapper, 
            IAirportService airportService, IFlightService flightService)
        {
            _mapper = mapper;
            _airportService = airportService;
            _flightService = flightService;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult FindAirport(string search)
        {
            var airport = _airportService.FindAirport(search);
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
            var flightById = _flightService.GetFullFlightById(id);
            var flight = _mapper.Map<FlightResponse>(flightById);

            if (flightById == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlights flight)
        {
            if (flight.From == flight.To)
            {
                return BadRequest();
            }

            return Ok(_flightService.FindFlight(flight));
        }
    }
}
