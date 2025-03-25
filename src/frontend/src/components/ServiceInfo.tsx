import React from "react";
import ServiceModel from "../models/ServiceModel";
import './ServiceInfo.css';

type serviceProps = {
  serviceInfo: ServiceModel;
}

function PlaneInfo({ serviceInfo }: serviceProps) {
  return (
      <div className="service-text-info">
        <p>{serviceInfo.name}</p>
        <p>Стоимость: {serviceInfo.price.toFixed(2)} &#8381;</p>
        <p>{serviceInfo.economyClass ? "Доступна" : "Недоступна"} для билетов экономкласса</p>
        <p>{serviceInfo.businessClass ? "Доступна" : "Недоступна"} для билетов бизнес-класса</p>
        <p>{serviceInfo.firstClass ? "Доступна" : "Недоступна"} для билетов первого класса</p>
      </div>
  );
}

export default PlaneInfo;
