import { useEffect, useState } from "react";
import ServiceModel from "../models/ServiceModel";
import ServiceService from "../services/ServiceService";

type ServicesHookType = [
  status: number,
  services: ServiceModel[],
  setServices: (value: any) => void
]
  
export default function useGetAllServices(baseUrl: string, jwt: string): ServicesHookType {
  const [services, setServices] = useState([]);
  const [status, setStatus] = useState(200);

  const serviceService = new ServiceService(baseUrl, jwt);
  
  useEffect(() => {
    serviceService.getAll()
    .then(res => {
      if (!res.ok) {
        throw res.status;
      }
      else {
        return res.json();
      }
    })
    .then(res => setServices(res))
    .catch(err => setStatus(err))
  }, []);

  return [status, services, setServices];
}
