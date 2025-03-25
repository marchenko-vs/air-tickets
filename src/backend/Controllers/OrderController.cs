using AirTickets.Models;
using AirTickets.Data;
using AirTickets.Services;
using Microsoft.AspNetCore.Mvc;
using AirTickets.Exceptions;
using AirTickets.Dto;
using Swashbuckle.AspNetCore.Annotations;
using DevExpress.Xpo.Helpers;
using AutoMapper;
using AirTickets.BlModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AirTickets.Controllers
{
    [ApiController]
    [Tags("Orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly BlitzFlugContext _context;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public OrderController(ILogger<OrderController> logger, BlitzFlugContext context)
        {
            _logger = logger;
            _context = context;
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDto, BlOrder>();
                cfg.CreateMap<BlOrder, OrderDto>();
            });
            _mapper = new Mapper(_cfg);
        }

        [Authorize]
        [HttpGet]
        [Route("orders/current")]
        [SwaggerResponse(404, "Order is not in database.")]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var order = orderService.GetActiveOrderByUserId(userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [Authorize]
        [HttpGet]
        [Route("orders/history")]
        public IActionResult GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var orders = orderService.GetHistory(userId);

            return Ok(_mapper.Map<List<OrderDto>>(orders));
        }

        [Authorize]
        [HttpGet]
        [Route("orders/current/sum")]
        public IActionResult CurrentOrderSum()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var ticketService = new TicketService(_context);
            var serviceService = new ServiceService(_context);

            var order = orderService.GetActiveOrderByUserId(userId);

            decimal result = 0.0M;

            List<BlTicket> tickets = ticketService.GetByOrderId(order.Id).ToList();

            foreach (var ticket in tickets)
            {
                result += ticket.Price;

                List<BlService> services = serviceService.GetActiveServices(ticket.Id).ToList();

                foreach (var service in services)
                {   
                    result += service.Price;
                }
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("orders/{orderId}/sum")]
        public IActionResult GetOrderSum(Int64 orderId)
        {
            var ticketService = new TicketService(_context);
            var serviceService = new ServiceService(_context);

            decimal result = 0.0M;

            List<BlTicket> tickets = ticketService.GetByOrderId(orderId).ToList();

            foreach (var ticket in tickets)
            {
                result += ticket.Price;

                List<BlService> services = serviceService.GetActiveServices(ticket.Id).ToList();

                foreach (var service in services)
                {   
                    result += service.Price;
                }
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("orders")]
        public IActionResult Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);

            try
            {
                var createdOrder = orderService.Create(userId);
                return Ok(_mapper.Map<OrderDto>(createdOrder));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("orders/current")]
        public IActionResult UpdateCurrent(OrderDto orderDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(identity.FindFirst("userId").Value);

            var orderService = new OrderService(_context);
            var order = orderService.GetActiveOrderByUserId(userId);
            orderDto.Id = order.Id;

            try
            {
                var createdOrder = orderService.Change(_mapper.Map<BlOrder>(orderDto));
                return Ok(_mapper.Map<OrderDto>(createdOrder));
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
        [HttpPatch]
        [Route("orders/{orderId}")]
        public IActionResult Update(Int64 orderId, OrderDto orderDto)
        {
            orderDto.Id = orderId;
            var orderService = new OrderService(_context);

            try
            {
                var createdOrder = orderService.Change(_mapper.Map<BlOrder>(orderDto));
                return Ok(_mapper.Map<OrderDto>(createdOrder));
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
    }
}
