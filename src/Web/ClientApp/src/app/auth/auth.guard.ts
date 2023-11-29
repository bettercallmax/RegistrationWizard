import { inject } from '@angular/core';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

export const authGuard = () => {
  const tokenService = inject(TokenService);
  const router = inject(Router);

    return tokenService.loggedIn$.pipe(
      tap(logged => {
        if (!logged) {
          router.navigate(['/login']);
        }
      })
    );
};
