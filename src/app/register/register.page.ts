import { Component, Input, NgZone, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoadingController } from '@ionic/angular';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {
  isLoading = false;
  userRegistrationModel: any;
  @Input() registerModel: any;

  constructor(
    private authService: AuthService,
    private router: Router,
    private loadingCtrl: LoadingController,
    private http: HttpClient,
    private ngZone: NgZone
  ) {}

  ngOnInit() {
    this.userRegistrationModel = this.authService.registrationModel;
  }

  onLogin() {
    this.isLoading = true;
    this.authService.onLogin();
    this.loadingCtrl
      .create({ keyboardClose: true, message: 'Logging in...' })
      .then((loadingEl) => {
        loadingEl.present();
        this.isLoading = false;
        loadingEl.dismiss();
        this.ngZone.run(() => {
          this.router.navigateByUrl('/places/tabs/discover');
        });
      });
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      return;
    }
    this.userRegistrationModel.Mobile =
      this.userRegistrationModel.Mobile.toString();
    return this.http
      .post(environment.baseUrl + 'Users', this.userRegistrationModel, {
        responseType: 'text',
      })
      .subscribe((res) => {
        if (res) {
          this.onLogin();
        } else {
          return;
        }
      });
  }
}
