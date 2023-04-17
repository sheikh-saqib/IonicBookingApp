import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonItemSliding } from '@ionic/angular';
import { PlacesService } from '../places.service';

@Component({
  selector: 'app-operations',
  templateUrl: './operations.page.html',
  styleUrls: ['./operations.page.scss'],
})
export class OperationsPage implements OnInit {
  myPlaces: any;

  constructor(
    private placesService: PlacesService,
    private router: Router,
    private ngZone: NgZone
  ) {}

  ngOnInit() {
    this.myPlaces = this.placesService.places;
  }
  onEdit(placeId: string, slidingItem: IonItemSliding) {
    slidingItem.close();
    this.ngZone.run(() => {
      this.router.navigate([
        '/',
        'places',
        'tabs',
        'operations',
        'edit',
        placeId,
      ]);
    });
  }
}
