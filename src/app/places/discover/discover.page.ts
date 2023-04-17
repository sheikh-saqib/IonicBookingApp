import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { PlacesService } from '../places.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-discover',
  templateUrl: './discover.page.html',
  styleUrls: ['./discover.page.scss'],
})
export class DiscoverPage implements OnInit {
  isLoaded: boolean = false;
  userName: string = '';
  slideOptsOne = {
    initialSlide: 0,
    slidesPerView: 1,
    autoplay: true,
  };
  constructor(
    public placesService: PlacesService,
    public authService: AuthService,
    private router: Router
  ) {
    console.log('Discover Page');
  }

  ngOnInit() {
    if (!this.authService.onLogin()) {
      this.router.navigateByUrl('/login');
    } else {
      console.log('Hi');
      const userToken = localStorage.getItem('eToken');
      if (typeof userToken === 'string') {
        const helper = new JwtHelperService();
        const userDetails = helper.decodeToken(userToken);
        this.userName = userDetails.nameid[1];
        this.placesService.getVenues();
      }
    }
  }
}
