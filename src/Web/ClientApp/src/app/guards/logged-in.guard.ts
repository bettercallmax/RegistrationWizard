import { Injectable } from "@angular/core";
import { TokenService } from "../services/token.service";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { tap } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class LoggedInGuard  {
  constructor(private readonly tokenService: TokenService, private readonly router: Router) { }

  canActivate() {
    return this.tokenService.loggedIn$.pipe(
      tap(logged => {
        if (!logged) {
          this.router.navigate(['/auth/login']);
        }
      })
    );
  }
}
