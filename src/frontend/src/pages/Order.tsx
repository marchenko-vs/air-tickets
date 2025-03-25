import { useEffect, useState } from "react";
import TicketModel from "../models/TicketModel";
import TicketInfo from "../components/TicketInfo";
import { createSearchParams, useNavigate } from "react-router-dom";
import useGetOrderedTickets from "../hooks/useGetOrderedTickets";
import { apiUrl } from "../contexts/ConfigContext";
import OrderService from "../services/OrderService";
import './Order.css';
import useGetCurrentOrderSum from "../hooks/useGetCurrentOrderSum";

export default function Order() {
  const jwt: string | null = localStorage.getItem('jwt');

  const [status, tickets, setTickets] = useGetOrderedTickets(apiUrl, jwt!);
  const [priceStatus, price, setPrice] = useGetCurrentOrderSum(apiUrl, jwt!);

  const orderService: OrderService = new OrderService(apiUrl, jwt!);

  let navigate = useNavigate();

  const handleDeleteTicket = (ticketToDelete: TicketModel) => {
    orderService.removeFromOrder(ticketToDelete);
    const newTickets = tickets.filter(ticket => ticket != ticketToDelete);
    setTickets(newTickets);
  }

  const handleDeleteAllTickets = () => {
    tickets.forEach(ticket => {
      orderService.removeFromOrder(ticket);
    })
    setTickets([]);
  }

  const handlePayOrder = () => {
    orderService.pay();
    setTickets([]);
  }

  return (
    <div>
      <div className="order-header">
        <ul>
          <li>
            <h1 className="page-title">Текущий заказ</h1>
          </li>
          <li>
            <h2 className="page-title">Сумма: {price.toFixed(2)} &#8381;</h2>
          </li>
          {
            tickets.length > 0 ?
            <>
              <li>
                <button className="order-big-button" 
                  onClick={() => handlePayOrder()}>Оплатить заказ</button>
              </li>
              <li><button className="order-big-button" 
                onClick={() => handleDeleteAllTickets()}>Удалить все билеты</button>
              </li>
            </>
            : <></>
          }
          <li>
            <button className="order-big-button" 
                onClick={() => navigate('/orders/history')}>История заказов</button>
          </li>
        </ul>
      </div>
      <div className="order-body">
        {
          tickets.map((ticket: TicketModel) => {
            return (
            <>
            <div className="order-container">
              <TicketInfo ticketInfo={ticket} />
              <div className="order-small-buttons">
              <div className="order-upper-button">
                <button className="order-small-button" 
                  onClick={() => navigate({
                    pathname: `/orders/current/tickets/${ticket.id}/services`,
                    search: createSearchParams({
                      "className": `${ticket.class}`
                    }).toString()
                  })}>Услуги</button>
                </div>
                <div className="order-lower-button"><button className="order-small-button" 
                  onClick={() => handleDeleteTicket(ticket)}>Удалить</button>
                  </div>
              </div>
            </div>
            </>
            )
          })
        }
      </div>
    </div>
  );
}
