import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { pki } from 'node-forge';
import { environment } from 'src/environments/environment';
import { Registration } from '../login/login.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isloggedIn = false;
  private _userId = 1;
  registrationModel: any;
  userName: any;
  jwtHelper = new JwtHelperService();

  get isLoggedIn() {
    return this._isloggedIn;
  }
  constructor() {}

  onLogin() {
    const token = localStorage.getItem('eToken');
    return !this.jwtHelper.isTokenExpired(token);
  }

  onLogout() {
    localStorage.removeItem('eToken');
  }
  get userId() {
    return this._userId;
  }
  // loggedIn() {
  //   const token = localStorage.getItem('eToken');
  //   return !this.jwtHelper.isTokenExpired(token);
  // }

  CreateAuthenticationModel(response: any) {
    this.userName = response.given_name;
    if (response != null) {
      this.registrationModel = new Registration(
        response.email,
        response.given_name,
        response.sub,
        response.family_name,
        '',
        'End User'
      );
      var rsa = pki.publicKeyFromPem(environment.encryptionKey);
      this.registrationModel.Email = window.btoa(rsa.encrypt(response.email));
      this.registrationModel.GoogleToken = window.btoa(
        rsa.encrypt(response.sub)
      );
      return this.registrationModel;
    }
  }
}
