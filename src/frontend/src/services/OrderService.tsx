import TicketModel from "../models/TicketModel";

export default class OrderService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async getOrderedTickets(): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/current/tickets`, {
      method: 'GET', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }

  public async getCurrentSum(): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/current/sum`, {
      method: 'GET', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }

  public async getSumByOrderid(orderId: any): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/${orderId}/sum`, {
      method: 'GET', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }

  public async getTicketsByOrderId(orderId: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/${orderId}/tickets`, {
      method: 'GET', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }

  public async create(): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders`, {
      method: 'POST', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }

  public async pay(): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/current`, {
      method: 'PATCH', 
      headers: { 
        'Authorization': `Bearer ${this.jwt}`,
        'content-type': 'application/json'
      },
      body: JSON.stringify({
        'userId': 0,
        'status': 'оплачен'
      })
    });
  }

  public async removeFromOrder(ticket: TicketModel): Promise<Response> {
    return await fetch(`${this.baseUrl}/tickets/${ticket.id}`, {
      method: 'PATCH', 
      headers: { 
        'Authorization': `Bearer ${this.jwt}`,
        'content-type': 'application/json'
      },
      body: JSON.stringify({
        "flightId": ticket.flightId,
        "orderId": 0,
        "row": ticket.row,
        "place": ticket.place,
        "class": ticket.class,
        "refund": ticket.refund,
        "price": ticket.price
      })
    })
  }

  public async getHistory(): Promise<Response> {
    return await fetch(`${this.baseUrl}/orders/history`, {
      method: 'GET', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }
}
