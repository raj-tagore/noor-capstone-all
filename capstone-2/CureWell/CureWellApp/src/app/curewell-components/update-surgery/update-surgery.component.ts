import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { CurewellService } from '../../curewell-services/curewell.service';
import { Surgery } from '../../curewell-interfaces/surgery';

@Component({
  templateUrl: './update-surgery.component.html'
})
export class UpdateSurgeryComponent implements OnInit {

  doctorId: number=0;
  surgeryId: number = 0;
  surgeryDate: Date = new Date();
  startTime: number = 0;
  endTime: number=0;
  surgeryCategory: string="";
  status: boolean=false;
  errorMsg: string = "";

  constructor(private route: ActivatedRoute, private _cureWellService: CurewellService, private router: Router) { }

  ngOnInit() {
    this.surgeryId = Number(this.route.snapshot.params['surgeryId']);
    this.surgeryCategory = this.route.snapshot.params['surgeryCategory'];
    this.surgeryDate = this.route.snapshot.params['surgeryDate'];
    this.startTime = Number(this.route.snapshot.params['startTime']);
    this.endTime = Number(this.route.snapshot.params['endTime']);
    this.doctorId = Number(this.route.snapshot.params['doctorId']);
  }

  editSurgery(startTime: number, endTime: number) {
    this._cureWellService.editSurgery(
      this.doctorId,
      endTime,
      startTime,
      this.surgeryCategory,
      this.surgeryDate,
      this.surgeryId
    ).subscribe({
      next: (response) => {
        this.status = response;
        if (response) {
          alert('Surgery details updated successfully!');
        } else {
          alert('Surgery details not updated');
        }
        this.router.navigate(['/viewTodaySurgery']);
      },
      error: (error) => {
        this.errorMsg = error;
        alert('Some error occurred');
        this.router.navigate(['/viewTodaySurgery']);
      },
      complete: () => {
        console.log('Updated surgery details successfully');
      }
    });
  }
}
