using AirTickets.Models;
using AirTickets.Dto;
using AirTickets.Services;
using AirTickets.Data;
using Microsoft.AspNetCore.Mvc;
using AirTickets.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using AirTickets.BlModels;
using Microsoft.AspNetCore.Authorization;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Planes")]
    public class PlaneController : ControllerBase
    {
        private readonly ILogger<PlaneController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public PlaneController(ILogger<PlaneController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlaneDto, BlPlane>();
                cfg.CreateMap<BlPlane, PlaneDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [HttpGet]
        [Route("planes")]
        public IActionResult GetAll()
        {
            var planeService = new PlaneService(_context);
            var planes = planeService.ReadAll();

            return Ok(_mapper.Map<List<PlaneDto>>(planes));
        }

        [HttpGet]
        [Route("flights/{flightId}/planes")]
        [SwaggerResponse(404, "Flight is not in database.")]
        public IActionResult GetByFlight(Int64 flightId)
        {
            var flightService = new FlightService(_context);
            var flight = flightService.GetById(flightId);

            if (flight == null)
            {
                return NotFound();
            }

            var planeService = new PlaneService(_context);
            var plane = planeService.Read(flight.Id);

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlaneDto>(plane));
        }

        [HttpGet]
        [Route("planes/{planeId}")]
        [SwaggerResponse(404, "Flight is not in database.")]
        public IActionResult Get(Int64 planeId)
        {
            var planeService = new PlaneService(_context);
            var plane = planeService.Read(planeId);

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlaneDto>(plane));
        }

        [HttpPost]
        [Route("planes")]
        [SwaggerResponse(400, "Incorrect input data.")]
        public IActionResult Post(PlaneDto planeDto)
        {
            var planeService = new PlaneService(_context);

            try
            {
                var createdPlane = planeService.Create(_mapper.Map<BlPlane>(planeDto));
                return Ok(_mapper.Map<PlaneDto>(createdPlane));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("planes/{planeId}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "Flight is not in database.")]
        public IActionResult Patch(Int64 planeId, PlaneDto planeDto)
        {
            planeDto.Id = planeId;
            var planeService = new PlaneService(_context);

            try
            {
                var createdPlane = planeService.ChangeInfo(_mapper.Map<BlPlane>(planeDto));
                return Ok(_mapper.Map<PlaneDto>(createdPlane));
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

        [HttpDelete]
        [Route("planes/{planeId}")]
        public IActionResult Delete(Int64 planeId)
        {
            var planeService = new PlaneService(_context);
            planeService.Delete(planeId);

            return Ok();
        }
    }
}
