using AirTickets.Data;
using AirTickets.Exceptions;
using AirTickets.Models;
using AirTickets.BlModels;
using AirTickets.Repositories;
using AutoMapper;
using BlitzFlug.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace AirTickets.Services
{
    public class FlightService
    {
        private readonly IFlightRepository<Flight> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public FlightService(BlitzFlugContext context)
        {
            _db = new FlightRepository(context);
            _cfg = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<BlFlight, Flight>();
                cfg.CreateMap<Flight, BlFlight>(); 
            });
            _mapper = new Mapper(_cfg);
        }

        public IEnumerable<BlFlight> GetAll()
        {
            return _mapper.Map<List<BlFlight>>(_db.ReadAll());
        }

        public List<string> GetUniquePoints()
        {
            List<string> res = _db.ReadUniquePoints().ToList();
            res.Sort();

            return res;
        }

        public IEnumerable<BlFlight> GetFlights(string? departurePoint,
            string? arrivalPoint, DateTime? departureDateTime)
        {
            return _mapper.Map<List<BlFlight>>(_db.ReadWithFilters(departurePoint, arrivalPoint, 
                                       departureDateTime));            
        }

        public BlFlight? GetById(Int64 id)
        {
            return _mapper.Map<BlFlight>(_db.Read(id));
        }

        public BlFlight Create(BlFlight flight)
        {
            if (flight.DeparturePoint == flight.ArrivalPoint)
            {
                throw new Exception();
            }
            if (flight.DepartureDateTime > flight.ArrivalDateTime)
            {
                throw new Exception();
            }

            var createdUser = _db.Create(_mapper.Map<Flight>(flight));

            return _mapper.Map<BlFlight>(createdUser);
        }

        public BlFlight Update(BlFlight flight)
        {
            if (flight == null)
            {
                throw new Exception();
            }
            if (flight.PlaneId < 0 ||
                flight.DeparturePoint == flight.ArrivalPoint ||
                flight.DepartureDateTime > flight.ArrivalDateTime)
            {
                throw new Exception();
            }

            var existingFlight = _db.Read(flight.Id);

            if (existingFlight == null)
            {
                throw new NotFoundException();
            }

            if (flight.PlaneId > 0)
            {
                existingFlight.PlaneId = flight.PlaneId;
            }
            if (!flight.DeparturePoint.IsNullOrEmpty())
            {
                existingFlight.DeparturePoint = flight.DeparturePoint;
            }
            if (!flight.ArrivalPoint.IsNullOrEmpty())
            {
                existingFlight.ArrivalPoint = flight.ArrivalPoint;
            }

            existingFlight.DepartureDateTime = flight.DepartureDateTime;
            existingFlight.ArrivalDateTime = flight.ArrivalDateTime;

            var updatedUser = _db.Update(existingFlight);

            return _mapper.Map<BlFlight>(updatedUser);
        }

        public void Delete(Int64 id)
        {
            _db.Delete(id);
        }
    }
}
