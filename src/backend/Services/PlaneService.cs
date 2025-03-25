using AirTickets.Models;
using AirTickets.Data;
using AirTickets.Repositories;
using System.ComponentModel.DataAnnotations;
using AirTickets.Exceptions;
using AirTickets.BlModels;
using AutoMapper;
using System.Collections.Generic;

namespace AirTickets.Services
{
    public class PlaneService
    {
        private readonly IPlaneRepository<Plane> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public PlaneService(BlitzFlugContext context)
        {
            _db = new PlaneRepository(context);
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlPlane, Plane>();
                cfg.CreateMap<Plane, BlPlane>();
            });
            _mapper = new Mapper(_cfg);
        }

        public BlPlane Create(BlPlane plane)
        {
            if (plane == null)
            {
                throw new Exception();
            }
            else if (plane.Manufacturer.Length < 1 || plane.Model.Length < 1)
            {
                throw new Exception();
            }
            else if (plane.EconomyClassNum < 0 || plane.BusinessClassNum < 0 
                     || plane.FirstClassNum < 0)
            {
                throw new Exception();
            }

            var createdPlane = _db.Create(_mapper.Map<Plane>(plane));

            return _mapper.Map<BlPlane>(createdPlane);
        }

        public BlPlane ChangeInfo(BlPlane plane)
        {
            if (plane == null)
            {
                throw new Exception();
            }
            else if (plane.EconomyClassNum < 0 || plane.BusinessClassNum < 0
                     || plane.FirstClassNum < 0)
            {
                throw new Exception();
            }

            var existingPlane = _db.Read(plane.Id);

            if (existingPlane == null)
            {
                throw new NotFoundException();
            }

            if (plane.Manufacturer.Length > 0)
            {
                existingPlane.Manufacturer = plane.Manufacturer;
            }
            if (plane.Model.Length > 0)
            {
                existingPlane.Model = plane.Model;
            }

            existingPlane.EconomyClassNum = plane.EconomyClassNum;
            existingPlane.BusinessClassNum = plane.BusinessClassNum;
            existingPlane.FirstClassNum = plane.FirstClassNum;

            var updatedPlane = _db.Update(existingPlane);

            return _mapper.Map<BlPlane>(updatedPlane);
        }

        public IEnumerable<BlPlane> ReadAll()
        {
            return _mapper.Map<List<BlPlane>>(_db.ReadAll());
        }

        public BlPlane? Read(Int64 id)
        {
            return _mapper.Map<BlPlane>(_db.Read(id));
        }

        public void Delete(Int64 id)
        {
            _db.Delete(id);
        }
    }
}
