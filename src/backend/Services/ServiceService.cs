using AirTickets.BlModels;
using AirTickets.Data;
using AirTickets.Exceptions;
using AirTickets.Repositories;
using AutoMapper;
using DevExpress.DirectX.Common.DirectWrite;
using DevExpress.Xpo.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.Models
{
    public class ServiceService
    {
        private IServiceRepository<Service> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public ServiceService(BlitzFlugContext context)
        {
            _db = new ServiceRepository(context);
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlService, Service>();
                cfg.CreateMap<Service, BlService>();
            });
            _mapper = new Mapper(_cfg);
        }

        public IEnumerable<BlService> GetActiveServices(Int64 ticketId)
        {
            return _mapper.Map<List<BlService>>(_db.ReadByTicketId(ticketId));
        }

        public IEnumerable<BlService> GetUnactiveServices(Int64 ticketId, string className)
        {
            List<Service> allServices = _db.ReadByClass(className).ToList();
            List<Service> activeServices = _db.ReadByTicketId(ticketId).ToList();
            List<Service> unactiveServices = new List<Service>();

            foreach (var service in allServices) 
            { 
                if (!activeServices.Contains(service))
                {
                    unactiveServices.Add(service);
                }
            }

            return _mapper.Map<List<BlService>>(unactiveServices);
        }

        public BlService? AddToTicket(Int64 ticketId, Int64 serviceId)
        {
            try
            {
                _db.AddToTicket(ticketId, serviceId);
                return _mapper.Map<BlService>(_db.Read(serviceId));
            }
            catch (Exception)
            {
                throw new Exception("Данная услуга уже выбрана для текущего билета");
            }
        }

        public void RemoveFromTicket(Int64 ticketId, Int64 serviceId)
        {
            _db.DeleteFromTicket(ticketId, serviceId);
        }

        public List<BlService> Get(string? className)
        {
            return _mapper.Map<List<BlService>>(_db.ReadByClass(className));
        }

        public BlService Create(BlService service)
        {
            if (service == null)
            {
                throw new Exception();
            }
            if (service.Name.IsNullOrEmpty() || Decimal.Compare(service.Price, Decimal.Zero) < 0)
            {
                throw new Exception();
            }

            var createdService = _db.Create(_mapper.Map<Service>(service));

            return _mapper.Map<BlService>(createdService);
        }

        public BlService Update(BlService service)
        {
            if (service == null)
            {
                throw new Exception();
            }
            if (Decimal.Compare(service.Price, Decimal.Zero) < 0)
            {
                throw new Exception();
            }

            var existingService = _db.Read(service.Id);

            if (existingService == null)
            {
                throw new NotFoundException();
            }

            if (!service.Name.IsNullOrEmpty())
            {
                existingService.Name = service.Name;
            }
            existingService.Price = service.Price;
            existingService.EconomyClass = service.EconomyClass;
            existingService.BusinessClass = service.BusinessClass;
            existingService.FirstClass = service.FirstClass;

            var updatedService = _db.Update(existingService);

            return _mapper.Map<BlService>(updatedService);
        }

        public void Delete(Int64 id)
        {
            _db.Delete(id);
        }
    }
}
