export default class PlaneService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async getAll(): Promise<Response> {
    return await fetch(`${this.baseUrl}/planes`, {
      method: 'GET'
    });
  }

  public async getByFlightId(planeId: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/flights/${planeId}/planes`, {
      method: 'GET'
    });
  }

  public async getById(id: string): Promise<Response> {
    return await fetch(
      `${this.baseUrl}/planes/${id}`, {
        method: 'GET'
      }
    );
  }
}
