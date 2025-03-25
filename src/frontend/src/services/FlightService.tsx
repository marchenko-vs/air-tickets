export default class FlightService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async getWithFilters(departurePoint: string | null,
                              arrivalPoint: string | null,
                              departureDate: string | null,): Promise<Response> {
    return await fetch(`${this.baseUrl}/flights?` 
    + "departurePoint=" + departurePoint
    + "&arrivalPoint=" + arrivalPoint
    + "&departureDateTime=" + departureDate, {
      method: 'GET',
    });
  }

  public async getUniquePoints(): Promise<Response> {
    return await fetch(`${this.baseUrl}/flights/uniquePoints`, {
      method: 'GET'
    });
  }
}
