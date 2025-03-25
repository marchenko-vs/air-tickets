import TicketModel from "../models/TicketModel";
import TicketInfo from "../components/TicketInfo";
import { useParams } from "react-router-dom";
import { apiUrl } from "../contexts/ConfigContext";
import OrderService from "../services/OrderService";
import useGetPaidTickets from "../hooks/useGetBoughtTickets";

export default function PreviousOrder() {
  const jwt: string | null = localStorage.getItem('jwt');
  
  const { orderId } = useParams();
  
  const [tickets, setTickets] = useGetPaidTickets(apiUrl, jwt!, orderId);

  const orderService: OrderService = new OrderService(apiUrl, jwt!);

  const handleRefundTicket = (ticketToDelete: TicketModel) => {
    orderService.removeFromOrder(ticketToDelete);

    const newTickets = tickets.filter(ticket => ticket != ticketToDelete);
    setTickets(newTickets);
  }

  return (
    <div>
      <h1 className="page-title">Заказ #{orderId}</h1>
      {
        tickets.map((ticket: TicketModel) => {
          return (
              <div className="ticket-container">
                <TicketInfo ticketInfo={ticket} />
                <div className="ticket-button-div">
                  {ticket.refund ? <button className="ticket-small-button" 
                    onClick={() => handleRefundTicket(ticket)}>Вернуть</button>
                  : <></>}
                </div>
              </div>
          )
        })
      }
    </div>
  );
}
