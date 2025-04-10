export class User {
  constructor(
    public id: string,
    public givenName: string,
    public familyName: string,
    public email: string,
    public image: string,
  ) {}

  fullName(): string {
    return `${this.givenName} ${this.familyName}`;
  }

  initials(): string {
    return `${this.givenName.charAt(0)}${this.familyName.charAt(0)}`.toUpperCase();
  }
}
