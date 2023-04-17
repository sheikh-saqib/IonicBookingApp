import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BookPlacePage } from './book-place.page';

const routes: Routes = [
  {
    path: '',
    component: BookPlacePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BookPlacePageRoutingModule {}
