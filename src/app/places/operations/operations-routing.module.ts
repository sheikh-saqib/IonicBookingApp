import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OperationsPage } from './operations.page';

const routes: Routes = [
  {
    path: '',
    component: OperationsPage,
  },
  {
    path: 'add-place',
    loadChildren: () =>
      import('./add-place/add-place.module').then((m) => m.AddPlacePageModule),
  },
  {
    path: 'edit-place',
    loadChildren: () =>
      import('./edit-place/edit-place.module').then(
        (m) => m.EditPlacePageModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OperationsPageRoutingModule {}
