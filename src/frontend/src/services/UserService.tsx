export default class UserService {
  private baseUrl: string;
  private jwt: string;

  constructor(baseUrl: string, jwt: string) {
    this.baseUrl = baseUrl;
    this.jwt = jwt;
  }

  public async login(email: string, password: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/users/login`, {
      method: 'POST', 
      headers: { 'content-type': 'application/json' },
      body: JSON.stringify({
        "email": email,
        "password": password
      })
    });
  }

  public async signup(email: string, password: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/users`, {
      method: 'POST', 
      headers: { 'content-type': 'application/json' },
      body: JSON.stringify({
        "email": email,
        "password": password,
        "firstName": "",
        "lastName": ""
      })
    });
  }

  public async changeSettings(email: string, oldPassword: string, newPassword: string,
                              firstName: string, lastName: string): Promise<Response> {
    return await fetch(`${this.baseUrl}/users`, {
      method: 'PATCH', 
      headers: { 'content-type': 'application/json',
                 'Authorization': `Bearer ${this.jwt}` },
      body: JSON.stringify({
        "email": email,
        "password": oldPassword,
        "newPassword": newPassword,
        "firstName": firstName,
        "lastName": lastName
      })
    });
  }
}
