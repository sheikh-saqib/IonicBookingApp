import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, Router, CanMatch } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanMatch {
  constructor(private authService: AuthService, private router: Router) {}

  canMatch(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.authService.onLogin) {
      this.router.navigateByUrl('/login');
    }
    return this.authService.isLoggedIn;
  }
}
