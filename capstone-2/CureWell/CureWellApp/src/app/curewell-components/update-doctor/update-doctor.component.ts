import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { CurewellService } from '../../curewell-services/curewell.service';
import { Doctor } from '../../curewell-interfaces/doctor';

@Component({
  templateUrl: './update-doctor.component.html'
})
export class UpdateDoctorComponent implements OnInit {

  doctorId: number=0;
  doctorName: string="";
  status: boolean=false;
  errorMsg: string ="";

  constructor(private route: ActivatedRoute, private _cureWellService: CurewellService, private router: Router) { }

  ngOnInit() {
    this.doctorId = Number(this.route.snapshot.params['doctorId']);
    this.doctorName = this.route.snapshot.params['doctorName'];
  }

  editDoctorDetails(doctorname: string) {
    this._cureWellService.editDoctorDetails(this.doctorId, doctorname).subscribe({
      next: (response) => {
        this.status = response;
        if (response) {
          alert("Doctor's name updated successfully!");
        } else {
          alert("Doctor's name not updated");
        }
        this.router.navigate(['/viewDoctors']);
      },
      error: (error) => {
        this.errorMsg = error;
        alert('Some error occurred');
        this.router.navigate(['/viewDoctors']);
      },
      complete: () => {
        console.log('Updated doctor details successfully.');
      }
    });
  }
}
