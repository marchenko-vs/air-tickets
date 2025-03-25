import ServiceInfo from "../components/ServiceInfo";
import { useState, useEffect } from "react";
import ServiceModel from "../models/ServiceModel";
import useGetAllServices from "../hooks/useGetAllServices";
import { apiUrl } from "../contexts/ConfigContext";

export default function Services () {
  const [status, services, setServices] = useGetAllServices(apiUrl, localStorage.getItem('jwt')!);

  return (
    <>
      <h1 className="page-title">Услуги нашей компании</h1>
      <div>
        {
          status === 200 ?
          services.map((service: ServiceModel) => {
            return <div key={service.id} className="service-container">
              <ServiceInfo serviceInfo={service} />
              </div>
          })
          : <p className='error-message'>Ошибка сервера</p>
        }
      </div>
    </>
  )
}
