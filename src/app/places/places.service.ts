import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';
import { Place } from './place.model';

@Injectable({
  providedIn: 'root',
})
export class PlacesService {
  loadedPlaces: any;
  isLoaded: boolean = false;
  venueDetails: any;
  private _places: Place[] = [];
  get places() {
    return [...this._places];
  }
  constructor(private authService: AuthService, private http: HttpClient) {
    console.log('placeservice');
  }
  getPlace(id: any) {
    return { ...this._places.find((p) => p.id == id) };
  }

  addPlace(
    title: string,
    code: string,
    address: string,
    contactNumber: number
  ) {
    const newPlace = new Place(
      Math.random().toString(),
      title,
      code,
      'https://lonelyplanetimages.imgix.net/mastheads/GettyImages-538096543_medium.jpg?sharp=10&vib=20&w=1200',
      'https://lonelyplanetimages.imgix.net/mastheads/GettyImages-538096543_medium.jpg?sharp=10&vib=20&w=1200',
      'https://lonelyplanetimages.imgix.net/mastheads/GettyImages-538096543_medium.jpg?sharp=10&vib=20&w=1200',
      this.authService.userId,
      address,
      '',
      contactNumber,
      ''
    );
    this._places.push(newPlace);
  }
  getVenues() {
    this.http.get(environment.baseUrl + 'Venue').subscribe((res) => {
      this.venueDetails = res;
      for (const row of this.venueDetails) {
        if (row.isActive) {
          const newPlace = new Place(
            row.venueId,
            row.name,
            row.address + ', ' + row.city.name,
            'https://img1.khelomore.com/venues/880/images/1040x490/WhatsApp-Image-2021-06-30-at-10-59-22-PM.jpg',
            'http://blog.playo.co/wp-content/uploads/2017/02/the-Gamechanger-magrath-road.jpg',
            'https://media.istockphoto.com/id/520999573/photo/indoor-soccer-football-field.jpg?s=612x612&w=0&k=20&c=X2PinGm51YPcqCAFCqDh7GvJxoG2WnJ19aadfRYk2dI=',
            1,
            row.latLong,
            row.code,
            row.contactNumber,
            row.navigationLink
          );
          this._places.push(newPlace);
        }
      }
      this.loadedPlaces = this._places;
      this.isLoaded = true;
    });
  }
}
