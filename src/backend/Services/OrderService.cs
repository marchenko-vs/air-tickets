using AirTickets.BlModels;
using AirTickets.Data;
using AirTickets.Exceptions;
using AirTickets.Models;
using AirTickets.Repositories;
using AutoMapper;
using System.Diagnostics;

namespace AirTickets.Services
{
    public class OrderService
    {
        private IOrderRepository<Order> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public OrderService(BlitzFlugContext context)
        {
            _db = new OrderRepository(context);
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlOrder, Order>();
                cfg.CreateMap<Order, BlOrder>();
            });
            _mapper = new Mapper(_cfg);
        }

        public BlOrder? GetActiveOrderByUserId(Int64 userId)
        {
            return _mapper.Map<BlOrder>(_db.ReadActiveByUserId(userId));
        }

        public BlOrder Create(Int64 userId)
        {
            var order = new BlOrder
            {
                UserId = userId,
                Status = "создан"
            };

            var createdOrder = _db.Create(_mapper.Map<Order>(order));

            return _mapper.Map<BlOrder>(createdOrder);
        }

        public BlOrder Change(BlOrder order)
        {
            if (order == null)
            {
                throw new Exception();
            }
            if (order.UserId < 0)
            {
                throw new Exception();
            }
            if (order.Status.Length > 0 && order.Status != "создан" && order.Status != "оплачен")
            {
                throw new Exception();
            }

            var existingOrder = _db.Read(order.Id);

            if (existingOrder == null)
            {
                throw new NotFoundException();
            }

            if (order.Status.Length > 0)
            {
                existingOrder.Status = order.Status;
            }
            if (order.UserId > 0)
            {
                existingOrder.UserId = order.UserId;
            }

            var updatedOrder = _db.Update(existingOrder);

            return _mapper.Map<BlOrder>(updatedOrder);
        }

        public List<BlOrder> GetHistory(Int64 userId)
        {
            return _mapper.Map<List<BlOrder>>(_db.ReadByUserId(userId).ToList());
        }
    }
}
