import { useState } from "react";
import { createSearchParams, useNavigate } from "react-router-dom";
import useGetUniquePoints from "../hooks/useGetUniquePoints";
import { apiUrl } from "../contexts/ConfigContext";
import './Home.css';

export default function Home () {
  const [formErr, setFormErr] = useState('');
  const [departurePoint, setDeparturePoint] = useState('');
  const [arrivalPoint, setArrivalPoint] = useState('');
  const [departureDate, setDepartureDate] = useState('');
  const [status, points, setPoints] = useGetUniquePoints(apiUrl, 
                                                         localStorage.getItem('jwt')!);

  let navigate = useNavigate();

  function handleSubmit(e: any) {
    e.preventDefault();

    const now: Date = new Date();
    const date: Date = new Date(departureDate);

    now.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);

    if (departurePoint === '') {
      setFormErr('Выберите пункт вылета');
      return;
    }
    if (arrivalPoint === '') {
      setFormErr('Выберите пункт прибытия');
      return;
    }
    if (departureDate === '') {
      setFormErr('Выберите дату вылета');
      return;
    }
    if (departurePoint === arrivalPoint) {
      setFormErr('Пункт вылета не должен совпадать с пунктом прибытия');
      return;
    }
    if (date.getTime() < now.getTime()) {
      setFormErr('Рейсов для данной даты нет');
      return;
    }

    navigate({
      pathname: '/flights',
      search: createSearchParams({
        departurePoint: departurePoint,
        arrivalPoint: arrivalPoint,
        departureDate: departureDate
      }).toString()
    });
  }

  return (
    <div className="home-form">
      <ul>
        <li>
          <h1 className="page-title">Поиск рейсов</h1>
        </li>
        <form onSubmit={handleSubmit}>
        <li>
          <select className="select-box" required name="departurePoint" 
            value={departurePoint} onChange={(e) => setDeparturePoint(e.target.value)}>
            <option>Пункт вылета</option>
            {
              status === 200 ?
              points.map((point: string) => {
                return <option value={point}>{point}</option>
              })
              : <></>
            }
          </select>
        </li>
        <li>
          <select className="select-box" required name="arrivalPoint" 
            value={arrivalPoint} onChange={(e) => setArrivalPoint(e.target.value)}>
            <option>Пункт прибытия</option>
            {
              status === 200 ?
              points.map((point: string) => {
                return <option value={point}>{point}</option>
              })
              : <></>
            }
          </select>
        </li>
        <li>
          <input className="date-picker" type="date" name="departureDate" 
            value={departureDate}
            onChange={(e) => setDepartureDate(e.target.value)}></input>
        </li>
        <li>
          <button className="big-button" type="submit">Найти рейсы</button>
        </li>
        </form>
        <li>
          {formErr === '' ? <></> : <p className="error-message">{formErr}</p>}
        </li>
      </ul>
    </div>
  )
}
