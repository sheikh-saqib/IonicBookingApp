import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlacesService } from '../../places.service';

@Component({
  selector: 'app-add-place',
  templateUrl: './add-place.page.html',
  styleUrls: ['./add-place.page.scss'],
})
export class AddPlacePage implements OnInit {
  form: any;
  constructor(
    private placesService: PlacesService,
    private router: Router,
    private ngZone: NgZone
  ) {}

  ngOnInit() {
    this.form = new FormGroup({
      title: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required],
      }),
      code: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required, Validators.maxLength(180)],
      }),
      address: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required, Validators.min(1)],
      }),
      contactNumber: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required],
      }),
    });
  }

  onCreateVenue() {
    if (!this.form.valid) {
      return;
    }
    this.placesService.addPlace(
      this.form.value.title,
      this.form.value.code,
      this.form.value.address,
      +this.form.value.contactNumber
    );
    this.form.reset();
    this.ngZone.run(() => {
      this.router.navigate(['/places/tabs/operations']);
    });
  }
}
