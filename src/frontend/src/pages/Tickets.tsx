import { useState } from "react";
import { useParams } from "react-router-dom";
import TicketModel from "../models/TicketModel";
import TicketInfo from "../components/TicketInfo";
import { apiUrl } from "../contexts/ConfigContext";
import TicketService from "../services/TicketService";
import useGetTicketsByFlightId from "../hooks/useGetTicketsByFlightId";
import './Tickets.css';

export default function Tickets () {
  const { flightId } = useParams();

  const [status, tickets, setTickets] = useGetTicketsByFlightId(apiUrl, flightId?.toString()!);
  const [error, setError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [ticketsPerPage, setTicketsPerPage] = useState(10);

  const addTicketToOrder = (ticketToAdd: TicketModel) => {
    const ticketService: TicketService = new TicketService(apiUrl, localStorage.getItem('jwt')!);

    ticketService.addToOrder(ticketToAdd.id)
    .then(res => {
      if (!res.ok) {
        throw res.status;
      }
      else {
        const newTickets = tickets.filter((ticket) => ticket != ticketToAdd);
        setTickets(newTickets);
      }
    })
    .catch(err => {
      setError(err);
      alert('Войдите в личный кабинет, чтобы добавить билет в заказ');
      return;
    });
  }

  const handleNextPage = () => {

    if (currentPage < (tickets.length / ticketsPerPage)) {
      setCurrentPage(currentPage + 1);
    }
  }

  const handlePrevPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  }

  return (
    <>
      <h1 className="page-title">{tickets.length > 0 ? "Билеты на данный рейс" : "Билеты не найдены"}</h1>
      <div>
        {tickets.slice(currentPage * ticketsPerPage - ticketsPerPage, 
          currentPage * ticketsPerPage).map((ticket: TicketModel) => {
          return <>
          <div className="ticket-container">
            <TicketInfo ticketInfo={ticket} />
            <div className="button-group">
              <button className="small-button" onClick={() => 
                addTicketToOrder(ticket)}>Добавить в заказ</button>
            </div>
          </div>
          </>
        })}
      </div>
      <div className="paginate-buttons">
        <button onClick={handlePrevPage}>Пред.</button>
        <button onClick={handleNextPage}>След.</button>
      </div>
    </>
  )
}
