import React from "react";
import { FlightModel } from "../models/FlightModel";
import { useNavigate } from "react-router-dom";
import './FlightInfo.css';

type Props = {
  flightInfo: FlightModel;
}

function FlightInfo({ flightInfo }: Props) {
  const departureDateTime = new Date(flightInfo.departureDateTime);
  const arrivalDateTime = new Date(flightInfo.arrivalDateTime);

  let navigate = useNavigate();

  return (
    <div className="flight-container">
      <div className="flight-text-info">
        <div className="flight-upper-text">
          <p>Пункт вылета: {flightInfo.departurePoint}</p>
          <p>Дата вылета: {departureDateTime.toLocaleDateString()}</p>
          <p>Время вылета: {departureDateTime.toLocaleTimeString()}</p>
        </div>
        <div className="flight-lower-text">
          <p>Пункт прибытия: {flightInfo.arrivalPoint}</p>
          <p>Дата прибытия: {arrivalDateTime.toLocaleDateString()}</p>
          <p>Время прибытия: {arrivalDateTime.toLocaleTimeString()}</p>
        </div>
      </div>
      <div className="flight-button-group">
        <div className="flight-upper-button">
          <button className="flight-small-button" onClick={() => 
            navigate(`/flights/${flightInfo.id}/planes/${flightInfo.planeId}`)}>Самолет</button>
        </div>
        <div className="flight-lower-button">
          <button className="flight-small-button" onClick={() => 
            navigate(`/flights/${flightInfo.id}/tickets`)}>Билеты</button>
        </div>
      </div>
    </div>
  );
}

export default FlightInfo;
