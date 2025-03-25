import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import FlightModel from "../models/FlightModel";
import FlightInfo from "../components/FlightInfo";
import './Flights.css';
import FlightService from "../services/FlightService";
import { apiUrl } from "../contexts/ConfigContext";
import useGetFlightsWithFilters from "../hooks/useGetFlightsWithFilters";

export default function Flights () {
  const [searchParams, setSearchParams] = useSearchParams();

  const [status, flights, setFlights] = useGetFlightsWithFilters(apiUrl, 
                                            searchParams.get('departurePoint'),
                                            searchParams.get('arrivalPoint'),
                                            searchParams.get('departureDate'));

  return (
    <>
      <h1 className="page-title">{flights.length > 0 ? "Найденные рейсы" : "Рейсы не найдены"}</h1>
      <div>
        {
          flights.map((flight: FlightModel) => {
            return <FlightInfo key={flight.id} flightInfo={flight} />
          })
        }
      </div>
    </>
  )
}
