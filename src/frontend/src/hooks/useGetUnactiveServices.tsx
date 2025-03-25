import { useEffect, useState } from "react";
import ServiceModel from "../models/ServiceModel";
import ServiceService from "../services/ServiceService";

export type ServicesHookType = [
  services: ServiceModel[], 
  setServices: (value: any) => void
]

export default function useGetUnactiveServices(baseUrl: string, jwt: string, 
                                               ticketId: any, className: any): ServicesHookType {
  const [services, setServices] = useState([]);

  const serviceService: ServiceService = new ServiceService(baseUrl, jwt);

  useEffect(() => {
    serviceService.getInactiveByTicketId(ticketId, className)
    .then(res => res.json())
    .then(res => setServices(res))
    .catch(err => {
      throw err
    })
  }, []);

  return [services, setServices];
}
