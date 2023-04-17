import { HttpClient } from '@angular/common/http';
import { Component, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingController, ToastController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';
declare var google: any;
@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage {
  isloggedIn = false;
  isLoading = false;

  constructor(
    private router: Router,
    private loadingCtrl: LoadingController,
    private http: HttpClient,
    private authService: AuthService,
    private ngZone: NgZone,
    private toastController: ToastController
  ) {}
  registrationModel: any;
  ngOnInit() {
    if (!this.authService.onLogin()) {
      console.log('google auth try');
      google.accounts.id.initialize({
        client_id:
          '519659077899-2v3cdi06n4jksqho063u67l0ppdmalgg.apps.googleusercontent.com',
        callback: this.handleGoogleSignIn.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });
      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById('buttonDiv'),
        { theme: 'outline', size: 'large', width: '100%' }
      );
      // @ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => {});
    } else {
      this.ngZone.run(() => {
        this.router.navigateByUrl('/places/tabs/discover');
      });
    }
  }

  onLogin() {
    this.isLoading = true;
    this.authService.onLogin();
    this.loadingCtrl
      .create({
        keyboardClose: true,
        message: 'Logging in ...',
        spinner: 'bubbles',
      })
      .then((loadingEl) => {
        loadingEl.present();
        this.isLoading = false;
        loadingEl.dismiss();
        this.ngZone.run(() => {
          this.presentToast('bottom');
          this.router.navigateByUrl('/places/tabs/discover');
        });
      });
  }
  async presentToast(position: 'top' | 'middle' | 'bottom') {
    const toast = await this.toastController.create({
      message: 'Welcome' + this.authService.userName,
      duration: 1500,
      position: position,
    });

    await toast.present();
  }
  handleGoogleSignIn(response: any) {
    // This next is for decoding the idToken to an object if you want to see the details.
    let base64Url = response.credential.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );
    var response = JSON.parse(jsonPayload);
    this.generateUserToken(
      this.authService.CreateAuthenticationModel(response)
    );
  }

  generateUserToken(response: any) {
    this.registrationModel = response;
    this.http
      .post(environment.baseUrl + 'Authentication', this.registrationModel, {
        responseType: 'text',
      })
      .subscribe((res) => {
        if (res != '') {
          localStorage.setItem('eToken', res);
          this.onLogin();
          console.log('Logged in Successfully');
        } else {
          this.ngZone.run(() => {
            this.router.navigateByUrl('/register');
          });
        }
      });
  }

  handleRefresh(event: any) {
    setTimeout(() => {
      this.ngOnInit();
      event.target.complete();
    }, 2000);
  }
}
