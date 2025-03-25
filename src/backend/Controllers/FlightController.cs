using AirTickets.Dto;
using AirTickets.BlModels;
using AirTickets.Services;
using AirTickets.Data;
using AirTickets.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Flights")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public FlightController(ILogger<FlightController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDto, BlFlight>();
                cfg.CreateMap<BlFlight, FlightDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [HttpGet]
        [Route("flights")]
        public IActionResult GetWithFilters(string? departurePoint, string? arrivalPoint,
                                            DateTime? departureDateTime)
        {
            var flightService = new FlightService(_context);
            var flights = flightService.GetFlights(departurePoint, arrivalPoint, 
                                                  departureDateTime);

            return Ok(_mapper.Map<List<FlightDto>>(flights));
        }

        [HttpGet]
        [Route("flights/uniquePoints")]
        public IActionResult GetUniquePoints()
        {
            var flightService = new FlightService(_context);
            var points = flightService.GetUniquePoints();

            return Ok(points);
        }

        [HttpGet]
        [Route("flights/{flightId}")]
        [SwaggerResponse(404, "Flight is not in database.")]
        public IActionResult Get(Int64 flightId)
        {
            var flightService = new FlightService(_context);
            var flight = flightService.GetById(flightId);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightDto>(flight));
        }

        [Authorize]
        [HttpPost]
        [Route("flights")]
        [SwaggerResponse(400, "Incorrect input data.")]
        public IActionResult Post(FlightDto flightDto)
        {
            var flightService = new FlightService(_context);

            try
            {
                var createdFlight = flightService.Create(_mapper.Map<BlFlight>(flightDto));
                return Ok(_mapper.Map<FlightDto>(createdFlight));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("flights/{flightId}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "Flight is not in database.")]
        public IActionResult Patch(Int64 flightId, FlightDto flightDto)
        {
            flightDto.Id = flightId;
            var flightService = new FlightService(_context);

            try
            {
                var createdFlight = flightService.Update(_mapper.Map<BlFlight>(flightDto));
                return Ok(_mapper.Map<FlightDto>(createdFlight));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("flights/{flightId}")]
        public IActionResult Delete(Int64 flightId)
        {
            var flightService = new FlightService(_context);
            flightService.Delete(flightId);

            return Ok();
        }
    }
}
