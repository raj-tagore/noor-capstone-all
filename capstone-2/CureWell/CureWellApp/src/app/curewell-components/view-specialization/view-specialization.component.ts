import { Component, OnInit } from '@angular/core';
import { Specialization } from '../../curewell-interfaces/specialization';
import { CurewellService } from '../../curewell-services/curewell.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Injectable } from '@angular/core';

@Component({
 templateUrl: './view-specialization.component.html',
})
export class ViewSpecializationComponent implements OnInit {

  specializationList: Specialization[] = [];
  showMsgDiv: boolean = false;
  errorMsg: string="";

  constructor(private _curewellService: CurewellService, private router: Router) { }

  ngOnInit() {
   this.getSpecialization();
  }

  getSpecialization() {

     this._curewellService.getAllSpecializations().subscribe({
     next: (res) => {
      this.specializationList = res;
     },
     error: (error) => {
      this.specializationList = null;
      this.errorMsg = error;
     }
     })

    //To do implement necessary logic
  }
}
