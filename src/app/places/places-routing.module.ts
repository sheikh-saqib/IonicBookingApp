import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PlacesPage } from './places.page';

const routes: Routes = [
  {
    path: 'tabs',
    component: PlacesPage,
    children: [
      {
        path: 'discover',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('./discover/discover.module').then(
                (m) => m.DiscoverPageModule
              ),
          },
          {
            path: ':placeId',
            loadChildren: () =>
              import('./discover/place-details/place-details.module').then(
                (m) => m.PlaceDetailsPageModule
              ),
          },
        ],
      },
      {
        path: 'operations',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('./operations/operations.module').then(
                (m) => m.OperationsPageModule
              ),
          },
          {
            path: 'new',
            loadChildren: () =>
              import('./operations/add-place/add-place.module').then(
                (m) => m.AddPlacePageModule
              ),
          },
          {
            path: 'edit/:placeId',
            loadChildren: () =>
              import('./operations/edit-place/edit-place.module').then(
                (m) => m.EditPlacePageModule
              ),
          },
        ],
      },
      {
        path: '',
        redirectTo: '/places/tabs/discover',
        pathMatch: 'full',
      },
    ],
  },
  {
    path: '',
    redirectTo: '/places/tabs/discover',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PlacesPageRoutingModule {}
