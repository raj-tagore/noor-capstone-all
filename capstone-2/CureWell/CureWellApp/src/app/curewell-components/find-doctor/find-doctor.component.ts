import { Component } from '@angular/core';
import { Doctor } from '../../curewell-interfaces/doctor';
import { CurewellService } from '../../curewell-services/curewell.service';

@Component({
  templateUrl: './find-doctor.component.html',
})
export class FindDoctorComponent {

  doctorId: number = 0;
  email: string = '';
  doctor: Doctor = null;
  errorMsg: string = '';

  constructor(private _curewellService: CurewellService) { }

  getById(doctorId: number) {
    //To do implement necessary logic
  }

  getByEmail(email: string) {
    //To do implement necessary logic
  }

}
