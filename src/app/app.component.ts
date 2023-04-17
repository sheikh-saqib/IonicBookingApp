import { Component, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor(
    private authService: AuthService,
    private router: Router,
    private ngZone: NgZone
  ) {}
  onLogout() {
    this.authService.onLogout();
    this.ngZone.run(() => {
      this.router.navigateByUrl('/login');
    });
  }
}
