import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ReplaySubject, filter } from "rxjs";
import { UserSessionService } from "./user-session.service";
import { UserLoginModel } from "../api/models";
import { UsersService } from "../api/services";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()
export class TokenService {
  public loggedIn$: ReplaySubject<boolean>;

  constructor(
    private readonly router: Router,
    private readonly jwtService: JwtHelperService,
    private readonly usersService: UsersService) {
    this.loggedIn$ = new ReplaySubject(1);

    this.loggedIn$.pipe(
      filter(x => !x)
    ).subscribe(() => this.router.navigate(['/login']));

    if (!this.tokenExpiredOrNotPresented()) {
      this.loggedIn$.next(true);
    } else {
      this.loggedIn$.next(false);
    }
  }

  public tokenExpiredOrNotPresented(): boolean | Promise<boolean> {
    return this.jwtService.isTokenExpired(undefined, 60);
  }

  public login(model: UserLoginModel) {
    this.usersService
      .userLoginPost({ body: model })
      .subscribe(model => {
          this.setAuthToken(model.token);
          this.router.navigate(['/']);
      });
  }

  public async logout() {
    UserSessionService.logout();
    this.loggedIn$.next(false);
  }

  private setAuthToken(token: string) {
    UserSessionService.login(token);
    this.loggedIn$.next(true);
  }
}
