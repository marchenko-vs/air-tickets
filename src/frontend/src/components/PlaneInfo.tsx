import React from "react";
import PlaneModel from "../models/PlaneModel";

type planeProps = {
  planeInfo: PlaneModel;
}

function PlaneInfo({ planeInfo }: planeProps) {
  const picPath: string = `/plane-${planeInfo.id}.jpg`;

  return (
    <div className="plane-container">
      <div className="plane-info">
        <p>Производитель: {planeInfo.manufacturer}</p>
        <p>Модель: {planeInfo.model}</p>
        <p>Количество мест экономкласса: {planeInfo.economyClassNum}</p>
        <p>Количество мест бизнес-класса: {planeInfo.businessClassNum}</p>
        <p>Количество мест первого класса: {planeInfo.firstClassNum}</p>
      </div>
      <div className="plane-pic-div">
        <img className="plane-pic" src={picPath} />
      </div>
    </div>
  );
}

export default PlaneInfo;
