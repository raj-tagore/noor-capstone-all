import { Component, OnInit, DoCheck } from '@angular/core';
import { Doctor } from '../../curewell-interfaces/doctor';
import { CurewellService } from '../../curewell-services/curewell.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Component({
  templateUrl: './view-doctor.component.html',
})
export class ViewDoctorComponent implements OnInit {

  doctorList: Doctor[]=[];
  showMsgDiv: boolean = false;
  doctorId: number=0;
  errorMsg: string="";
  status: boolean=false;

  constructor(private _curewellService: CurewellService, private router: Router) { }

  ngOnInit() {
    this.getDoctor();
  }

  getDoctor() {
    this._curewellService.getDoctors().subscribe({
      next: (res) => {
        this.doctorList = res;
        this.showMsgDiv = true;
        console.log('Doctors Fetched Successfully');
      },
      error: (error) => {
        this.doctorList = null;
        this.errorMsg = error;
        this.showMsgDiv = true;
        console.log('Error fetching doctors');
      }
    });
  }

  editDoctorDetails(doctor: Doctor) {
    this.router.navigate(['/editDoctorDetails', doctor.doctorId, doctor.doctorName]);
  }

  removeDoctor(doctor: Doctor) {
    this._curewellService.deleteDoctor(doctor).subscribe({
      next: (response) => {
        this.status = response;
        if (response) {
          alert('Doctor detailed deleted successfully!');
          this.getDoctor();
        } else {
          alert("Doctor's name not deleted");
        }
      },
      error: () => {
        this.errorMsg = 'Some error occured';
      }
    });
  }

}
