import { useEffect, useState } from "react";
import TicketModel from "../models/TicketModel";
import OrderService from "../services/OrderService";

export type TicketsHookType = [
  status: number | undefined,
  tickets: TicketModel[], 
  setTickets: (value: any) => void
]

function createOrder(baseUrl: string, jwt: string) {
  const orderService: OrderService = new OrderService(baseUrl, jwt);

  orderService.create()
  .then(res => {
    if (res.ok) {
      return res.json();
    }
    else {
      throw res.status;
    }
  })
  .catch(err => console.log(err));
}

export default function useGetOrderedTickets(baseUrl: string, jwt: string): TicketsHookType {
  const [tickets, setTickets] = useState([]);
  const [status, setStatus] = useState(200);

  const orderService: OrderService = new OrderService(baseUrl, jwt);

  useEffect(() => {
    orderService.getOrderedTickets()
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => setTickets(res))
    .catch(err => {
      createOrder(baseUrl, jwt);
    });
  }, []);

  return [status, tickets, setTickets];
}
