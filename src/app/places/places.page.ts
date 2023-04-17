import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Place } from './place.model';

@Component({
  selector: 'app-places',
  templateUrl: './places.page.html',
  styleUrls: ['./places.page.scss'],
})
export class PlacesPage implements OnInit {
  loadedPlaces: any;
  isLoaded: boolean = false;
  venueDetails: any;
  venueDeatils: any;
  _places: Place[] = [];
  constructor(public authService: AuthService) {}

  ngOnInit() {
    console.log('placepage');
  }
}
