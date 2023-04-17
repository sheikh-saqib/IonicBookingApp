import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { PlacesService } from '../places.service';

@Component({
  selector: 'app-book-place',
  templateUrl: './book-place.page.html',
  styleUrls: ['./book-place.page.scss'],
})
export class BookPlacePage implements OnInit {
  // place: any;

  // constructor(
  //   private route: ActivatedRoute,
  //   private navCtrl: NavController,
  //   private placesService: PlacesService
  // ) {}

  ngOnInit() {
    // this.route.paramMap.subscribe((paramMap) => {
    //   if (!paramMap.has('placeId')) {
    //     this.navCtrl.navigateBack('/places/tabs/offers');
    //     return;
    //   }
    //   this.place = this.placesService.getPlace(paramMap.get('placeId'));
    // });
  }
}
