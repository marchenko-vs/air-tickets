import ServiceInfo from "../components/ServiceInfo";
import ServiceModel from "../models/ServiceModel";
import { useParams, useSearchParams } from "react-router-dom";
import useGetUnactiveServices from "../hooks/useGetUnactiveServices";
import { apiUrl } from "../contexts/ConfigContext";
import useGetActiveServices from "../hooks/useGetActiveServices";
import ServiceService from "../services/ServiceService";
import '../components/ServiceInfo.css';

export default function ServicesForTicket () {
  const jwt: string | null = localStorage.getItem('jwt');

  const { ticketId } = useParams();
  const [searchParams, setSearchParams] = useSearchParams();

  const [services, setServices] = useGetUnactiveServices(apiUrl, jwt!, ticketId, searchParams.get('className'));
  const [activeServices, setActiveServices] = useGetActiveServices(apiUrl, jwt!, ticketId);

  const serviceService: ServiceService = new ServiceService(apiUrl, jwt!);

  const handleAddService = (serviceToAdd: ServiceModel) => {
    serviceService.addToTicket(ticketId!, serviceToAdd.id.toString());
    
    const newServices = services.filter(ticket => ticket != serviceToAdd);
    setServices(newServices);

    let newActiveServices = activeServices;
    newActiveServices.push(serviceToAdd);
    setActiveServices(newActiveServices);
  }

  const handleRemoveService = (serviceToRemove: ServiceModel) => {
    serviceService.removeFromTicket(ticketId!, serviceToRemove.id.toString());
    
    const newActiveServices = activeServices.filter(ticket => ticket != serviceToRemove);
    setActiveServices(newActiveServices);

    let newServices = services;
    newServices.push(serviceToRemove);
    setServices(newServices);
  }

  return (
    <>
      <h1 className="page-title">Дополнительные услуги</h1>
      <div>
        {
          activeServices.map((service: ServiceModel) => {
            return true ?
             <div className="service-container">
              <ServiceInfo key={service.id} serviceInfo={service} />
              {
                <div className="service-small-button-div"><button className="service-small-button" 
                  onClick={() => handleRemoveService(service)}>Удалить</button></div>
              }
              </div> : <></>
          })
        }
        {
          services.map((service: ServiceModel) => {
            return true ?
             <div className="service-container">
              <ServiceInfo key={service.id} serviceInfo={service} />
              {
                <div className="service-small-button-div"><button className="service-small-button"
                  onClick={() => handleAddService(service)}>Добавить</button></div>
              }
              </div> : <></>
          })
        }
    </div>
    </>
  )
}
