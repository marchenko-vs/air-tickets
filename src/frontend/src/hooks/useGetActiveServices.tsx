import { useEffect, useState } from "react";
import ServiceModel from "../models/ServiceModel";
import ServiceService from "../services/ServiceService";

type ServicesHookType = [
  services: ServiceModel[], 
  setServices: (value: any) => void
]

export default function useGetActiveServices(baseUrl: string, jwt: string, 
                                               ticketId: any): ServicesHookType {
  const [services, setServices] = useState([]);

  const serviceService: ServiceService = new ServiceService(baseUrl, jwt);

  useEffect(() => {
    serviceService.getActiveByTicketId(ticketId)
    .then(res => res.json())
    .then(res => setServices(res))
    .catch(err => {
      throw err
    })
  }, []);

  return [services, setServices];
}
