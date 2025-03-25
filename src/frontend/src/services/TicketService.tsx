export default class TicketService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async getByFlightId(flightId: string): Promise<Response> {
    return await fetch(
      `${this.baseUrl}/flights/${flightId}/tickets`, {
        method: 'GET'
      }
    );
  }

  public async addToOrder(ticketId: number): Promise<Response> {
    return await fetch(`${this.baseUrl}/tickets/${ticketId}`, {
      method: 'POST', 
      headers: { 'Authorization': `Bearer ${this.jwt}` },
    });
  }
}
