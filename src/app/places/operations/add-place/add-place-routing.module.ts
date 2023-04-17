import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddPlacePage } from './add-place.page';

const routes: Routes = [
  {
    path: '',
    component: AddPlacePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AddPlacePageRoutingModule {}
