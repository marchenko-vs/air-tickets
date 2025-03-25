using AirTickets.Models;
using AirTickets.Dto;
using AirTickets.Services;
using AirTickets.Data;
using Microsoft.AspNetCore.Mvc;
using AirTickets.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using AutoMapper;
using AirTickets.BlModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Services")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public ServiceController(ILogger<ServiceController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ServiceDto, BlService>();
                cfg.CreateMap<BlService, ServiceDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [HttpGet]
        [Route("services")]
        public IActionResult Get([FromQuery] string? className)
        {
            var serviceService = new ServiceService(_context);
            var services = serviceService.Get(className);

            return Ok(_mapper.Map<List<ServiceDto>>(services));
        }

        [Authorize]
        [HttpGet]
        [Route("tickets/{ticketId}/services")]
        public IActionResult GetByTicket(Int64 ticketId)
        {
            var serviceService = new ServiceService(_context);
            var services = serviceService.GetActiveServices(ticketId);

            return Ok(_mapper.Map<List<ServiceDto>>(services));
        }

        [Authorize]
        [HttpGet]
        [Route("tickets/{ticketId}/services/inactive")]
        public IActionResult GetInactiveServices(Int64 ticketId, [FromQuery] string className)
        {
            var serviceService = new ServiceService(_context);
            var services = serviceService.GetUnactiveServices(ticketId, className);

            return Ok(_mapper.Map<List<ServiceDto>>(services));
        }

        [Authorize]
        [HttpPost]
        [Route("services")]
        [SwaggerResponse(400, "Incorrect input data.")]
        public IActionResult Post(ServiceDto serviceDto)
        {
            var serviceService = new ServiceService(_context);

            try
            {
                var createdService = serviceService.Create(_mapper.Map<BlService>(serviceDto));
                return Ok(_mapper.Map<ServiceDto>(createdService));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("tickets/{ticketId}/services/{serviceId}")]
        [SwaggerResponse(404, "Service is not added to the ticket.")]
        public IActionResult ServiceToTicket(Int64 ticketId, Int64 serviceId)
        {
            var serviceService = new ServiceService(_context);

            try
            {
                var createdService = serviceService.AddToTicket(ticketId, serviceId);
                return Ok(_mapper.Map<ServiceDto>(createdService));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("tickets/{ticketId}/services/{serviceId}")]
        [SwaggerResponse(404, "Service is not added to the ticket.")]
        public IActionResult ServiceFromTicket(Int64 ticketId, Int64 serviceId)
        {
            var serviceService = new ServiceService(_context);

            try
            {
                serviceService.RemoveFromTicket(ticketId, serviceId);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("services/{serviceId}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "Service is not in database.")]
        public IActionResult Patch(Int64 serviceId, ServiceDto serviceDto)
        {
            serviceDto.Id = serviceId;
            var serviceService = new ServiceService(_context);

            try
            {
                var createdService = serviceService.Update(_mapper.Map<BlService>(serviceDto));
                return Ok(_mapper.Map<ServiceDto>(createdService));
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
        [Route("services/{serviceId}")]
        public IActionResult Delete(Int64 serviceId)
        {
            var serviceService = new ServiceService(_context);
            serviceService.Delete(serviceId);

            return Ok();
        }
    }
}
