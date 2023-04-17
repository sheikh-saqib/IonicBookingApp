export class Registration {
  constructor(
    public Email: string,
    public FirstName: string,
    public GoogleToken: string,
    public LastName: string,
    public Mobile: string,
    public Role: string
  ) {}
}
export class User {
  constructor(
    public id: string,
    public email: string,
    private _token: string,
    private tokenExpirationDate: Date
  ) {}

  get token() {
    if (!this.tokenExpirationDate || this.tokenExpirationDate <= new Date()) {
      return null;
    }
    return this._token;
  }
}
