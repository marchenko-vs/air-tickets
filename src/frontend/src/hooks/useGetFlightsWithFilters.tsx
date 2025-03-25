import { useEffect, useState } from "react";
import FlightService from "../services/FlightService";
import FlightModel from "../models/FlightModel";

type FlightsHookType = [
  status: number,
  flights: FlightModel[],
  setFlights: (value: any) => void
]

export default function useGetFlightsWithFilters(baseUrl: string, departurePoint: string | null, 
                                                 arrivalPoint: string | null,
                                                 departureDate: string | null): FlightsHookType {
  const [flights, setFlights] = useState([]);
  const [status, setStatus] = useState(200);

  const flightService = new FlightService(baseUrl, '');
  
  useEffect(() => {
    flightService.getWithFilters(departurePoint, arrivalPoint, departureDate)
    .then(res => res.json())
    .then(res => {
      setFlights(res);
    })
    .catch(err => {
      setStatus(err);
      return;
    })
  }, []);

  return [status, flights, setFlights];
}
