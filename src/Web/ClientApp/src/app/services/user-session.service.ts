const AUTH_TOKEN_LOCALSTORAGE = 'auth-token';

export class UserSessionService {
  static login(token: string) {
    localStorage.setItem(AUTH_TOKEN_LOCALSTORAGE, token);
  }

  static getJwtToken() {
    return localStorage.getItem(AUTH_TOKEN_LOCALSTORAGE) || '';
  }

  static logout() {
    localStorage.removeItem(AUTH_TOKEN_LOCALSTORAGE);
  }
}
