import { useEffect, useState } from "react";
import TicketModel from "../models/TicketModel";
import OrderService from "../services/OrderService";

export type TicketsHookType = [
  tickets: TicketModel[], 
  setTickets: (value: any) => void
]

export default function useGetPaidTickets(baseUrl: string, jwt: string,
                                          orderId: any): TicketsHookType {
  const [tickets, setTickets] = useState([]);

  const orderService: OrderService = new OrderService(baseUrl, jwt!);

  useEffect(() => {
    orderService.getTicketsByOrderId(orderId)
    .then(res => {
      if (res.ok) {
        return res.json();
      }
      else {
        throw res.status;
      }
    })
    .then(res => setTickets(res));
  }, []);

  return [tickets, setTickets];
}
