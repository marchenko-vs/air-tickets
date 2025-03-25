export default class ServiceService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async getAll(): Promise<Response> {
    return await fetch(`${this.baseUrl}/services`, {
      method: 'GET'
    });
  }

  public async getActiveByTicketId(ticketId: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/tickets/${ticketId}/services`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${this.jwt}`
      }
    });
  }

  public async getInactiveByTicketId(ticketId: string, className: string | null): Promise<Response> {
    return await fetch(`${this.baseUrl}/tickets/${ticketId}/services/inactive?className=${className}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${this.jwt}`
      }
    });
  }

  public async removeFromTicket(ticketId: string, serviceId: string): Promise<Response> {
    return await fetch(`http://localhost:5008/tickets/${ticketId}/services/${serviceId}`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${this.jwt}`
      }
    });
  }

  public async addToTicket(ticketId: string, serviceId: string): Promise<Response> {
    return await fetch(`http://localhost:5008/tickets/${ticketId}/services/${serviceId}`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${this.jwt}`
      }
    });
  }
}
