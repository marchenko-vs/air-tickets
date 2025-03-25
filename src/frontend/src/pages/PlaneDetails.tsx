import { useEffect, useState } from "react";
import { Outlet, useParams } from "react-router-dom";
import PlaneModel from "../models/PlaneModel";
import PlaneInfo from "../components/PlaneInfo";
import './Planes.css'
import PlaneService from "../services/PlaneService";
import { apiUrl } from "../contexts/ConfigContext";

export default function PlaneDetails() {
  const { planeId } = useParams();

  const [plane, setPlane] = useState({});
  const [error, setError] = useState({});

  const planeService: PlaneService = new PlaneService(apiUrl, '');

  useEffect(() => {
    planeService.getById(planeId?.toString()!)
    .then(response => response.json())
    .then(res => setPlane(res))
    .catch(err => setError(err))
  });
  
  return (
    <div className="plane-details-container">
      <ul>
        <li>
          <h1 className="page-title">Самолет, выполняющий данный рейс</h1>
        </li>
        <li>
          <PlaneInfo planeInfo={plane as PlaneModel} />
        </li>
      </ul>
    </div>
  )
}
