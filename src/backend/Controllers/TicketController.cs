using AirTickets.Dto;
using AirTickets.Data;
using AirTickets.Services;
using Microsoft.AspNetCore.Mvc;
using AirTickets.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using AirTickets.BlModels;
using Microsoft.AspNetCore.Authorization;
using AirTickets.Identity;
using System.Security.Claims;
using AirTickets.Models;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public TicketController(ILogger<TicketController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDto, BlTicket>();
                cfg.CreateMap<BlTicket, TicketDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [HttpGet]
        [Route("flights/{flightId}/tickets")]
        public IActionResult GetByFlight(Int64 flightId)
        {
            var ticketService = new TicketService(_context);
            var tickets = ticketService.GetAvailableTickets(flightId);

            return Ok(_mapper.Map<List<TicketDto>>(tickets));
        }

        [Authorize]
        [HttpGet]
        [Route("orders/current/tickets")]
        [SwaggerResponse(404, "Order is not in database.")]
        public IActionResult GetCurrentTickets()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var order = orderService.GetActiveOrderByUserId(userId);

            if (order == null)
            {
                return NotFound();
            }

            var ticketService = new TicketService(_context);
            var tickets = ticketService.GetByOrderId(order.Id);

            return Ok(_mapper.Map<List<TicketDto>>(tickets));
        }

        [Authorize]
        [HttpGet]
        [Route("orders/{orderId}/tickets")]
        public IActionResult GetTicketsByOrderId(Int64 orderId)
        {
            var ticketService = new TicketService(_context);
            var tickets = ticketService.GetByOrderId(orderId);

            return Ok(_mapper.Map<List<TicketDto>>(tickets));
        }

        [HttpPost]
        [Route("tickets")]
        [SwaggerResponse(400, "Incorrect input data.")]
        public IActionResult Post(TicketDto ticketDto)
        {
            var ticketService = new TicketService(_context);

            try
            {
                var createdTicket = ticketService.Create(_mapper.Map<BlTicket>(ticketDto));
                return Ok(_mapper.Map<TicketDto>(createdTicket));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("tickets/{ticketId}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        public IActionResult TicketToOrder(Int64 ticketId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var order = orderService.GetActiveOrderByUserId(userId);

            var ticketService = new TicketService(_context);

            try
            {
                var createdTicket = ticketService.AddToOrder(order.Id, ticketId);
                return Ok(_mapper.Map<TicketDto>(createdTicket));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("tickets/{ticketId}")]
        [SwaggerResponse(400, "Incorrect input data.")]
        [SwaggerResponse(404, "Ticket is not in database.")]
        public IActionResult Patch(Int64 ticketId, TicketDto ticketDto)
        {
            ticketDto.Id = ticketId;
            var ticketService = new TicketService(_context);

            try
            {
                var createdTicket = ticketService.Update(_mapper.Map<BlTicket>(ticketDto));
                return Ok(_mapper.Map<TicketDto>(createdTicket));
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
        [Route("tickets/{ticketId}")]
        public IActionResult Delete(Int64 ticketId)
        {
            var ticketService = new TicketService(_context);
            ticketService.Delete(ticketId);

            return Ok();
        }
    }
}
