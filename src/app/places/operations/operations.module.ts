import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { OperationsPageRoutingModule } from './operations-routing.module';

import { OperationsPage } from './operations.page';
import { OperationsItemComponent } from './operations-item/operations-item.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule,
    OperationsPageRoutingModule,
  ],
  declarations: [OperationsPage, OperationsItemComponent],
})
export class OperationsPageModule {}
