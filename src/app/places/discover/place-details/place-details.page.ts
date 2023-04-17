import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalController, NavController } from '@ionic/angular';
import { CreateBookingComponent } from '../../../bookings/create-booking/create-booking.component';
import { PlacesService } from '../../places.service';
import { Share } from '@capacitor/share';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.page.html',
  styleUrls: ['./place-details.page.scss'],
})
export class PlaceDetailsPage implements OnInit {
  place: any;
  slideValue = 0;
  showDeleteSlot: boolean = false;
  constructor(
    private navCtrl: NavController,
    private route: ActivatedRoute,
    private placesService: PlacesService,
    private modalCtrl: ModalController,
    private ngZone: NgZone
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((paramMap) => {
      if (!paramMap.has('placeId')) {
        this.ngZone.run(() => {
          this.navCtrl.navigateBack('/places/tabs/discover');
        });
        return;
      }
      this.place = this.placesService.getPlace(paramMap.get('placeId'));
      if (!this.place.id) {
        this.ngZone.run(() => {
          this.navCtrl.navigateBack('/places/tabs/discover');
        });
      }
    });
  }
  onBookPlace() {
    this.modalCtrl
      .create({
        component: CreateBookingComponent,
        componentProps: { selectedPlace: this.place },
      })
      .then((modalEl) => {
        modalEl.present();
        return modalEl.onDidDismiss();
      })
      .then((resultData) => {
        if (resultData.role === 'confirm') {
          console.log('Booked!');
        }
      });
  }
  onNavigate() {
    var url = this.place.navigationLink;
    window.open(url, '_blank');
  }
  openSocial() {
    Share.share({
      url: 'http://ionicframework.com/',
      dialogTitle: 'Share',
    });
  }
}
