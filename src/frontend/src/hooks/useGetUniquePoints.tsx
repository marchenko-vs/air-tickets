import { useEffect, useState } from "react";
import FlightService from "../services/FlightService";

type PointsHookType = [
  status: number,
  points: string[],
  setPoints: (value: any) => void
]
  
export default function useGetUniquePoints(baseUrl: string, jwt: string): PointsHookType {
  const [points, setPoints] = useState([]);
  const [status, setStatus] = useState(200);

  const flightService = new FlightService(baseUrl, jwt);
  
  useEffect(() => {
    flightService.getUniquePoints()
    .then(res => {
      if (!res.ok) {
        throw res.status;
      }
      else {
        return res.json();
      }
    })
    .then(res => setPoints(res))
    .catch(err => setStatus(err))
  }, []);

  return [status, points, setPoints];
}
