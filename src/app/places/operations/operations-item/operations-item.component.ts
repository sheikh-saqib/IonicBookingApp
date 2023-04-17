import { Component, Input, OnInit } from '@angular/core';
import { Place } from '../../place.model';

@Component({
  selector: 'app-operations-item',
  templateUrl: './operations-item.component.html',
  styleUrls: ['./operations-item.component.scss'],
})
export class OperationsItemComponent implements OnInit {
  @Input() operations: any;
  constructor() {}

  ngOnInit() {}
  getDummyDate() {
    return new Date();
  }
}
