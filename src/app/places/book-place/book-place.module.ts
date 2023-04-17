import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { BookPlacePageRoutingModule } from './book-place-routing.module';

import { BookPlacePage } from './book-place.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    BookPlacePageRoutingModule
  ],
  declarations: [BookPlacePage]
})
export class BookPlacePageModule {}
