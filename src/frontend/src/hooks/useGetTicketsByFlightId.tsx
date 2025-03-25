import { useEffect, useState } from "react";
import TicketModel from "../models/TicketModel";
import TicketService from "../services/TicketService";

type TicketsHookType = [
  status: number,
  tickets: TicketModel[],
  setTickets: (value: any) => void
]

export default function useGetTicketsByFlightId(baseUrl: string, flightId: string): TicketsHookType {
  const [tickets, setTickets] = useState([]);
  const [status, setStatus] = useState(200);

  const ticketService: TicketService = new TicketService(baseUrl, '');
  
  useEffect(() => {
    ticketService.getByFlightId(flightId)
    .then(res => res.json())
    .then(res => {
      setTickets(res);
    })
    .catch(err => {
      setStatus(err.status);
      return;
    })
  }, []);

  return [status, tickets, setTickets];
}
