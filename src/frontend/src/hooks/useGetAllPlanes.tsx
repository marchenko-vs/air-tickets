import { useEffect, useState } from "react";
import PlaneModel from "../models/PlaneModel";
import PlaneService from "../services/PlaneService";

type PlanesHookType = [
  status: number,
  planes: PlaneModel[],
  setPlanes: (value: any) => void
]
  
export default function useGetAllPlanes(baseUrl: string, jwt: string): PlanesHookType {
  const [planes, setPlanes] = useState([]);
  const [status, setStatus] = useState(200);

  const planeService = new PlaneService(baseUrl, jwt);
  
  useEffect(() => {
    planeService.getAll()
    .then(res => {
      if (!res.ok) {
        throw res.status;
      }
      else {
        return res.json();
      }
    })
    .then(res => setPlanes(res))
    .catch(err => setStatus(err))
  }, []);

  return [status, planes, setPlanes];
}
