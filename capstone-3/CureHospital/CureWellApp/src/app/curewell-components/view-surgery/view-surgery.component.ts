import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CurewellService } from '../../curewell-services/curewell.service';
import { SurgeryDetail } from '../../curewell-interfaces/surgery';

/*
  View Surgery screen.

  templateUrl points at view-surgery.component.html. Every name used
  in that HTML (surgeryDate, surgeryDates, getSurgery, editSurgery,
  surgeryList, showMsgDiv, errorMsg) is a member of this class.

  The class is created for the /viewSurgery route in app.routing.ts.
  AppModule in app.module.ts lists it in declarations so Angular
  knows the selector and the template belong together.
*/
@Component({
  selector: 'app-view-surgery',
  templateUrl: './view-surgery.component.html'
})
export class ViewSurgeryComponent implements OnInit {
  /* Distinct dates for the dropdown. Filled by getAllSurgeryDates(). */
  surgeryDates: string[] = [];

  /*
    The date currently chosen in the select. Starts undefined so the
    placeholder option in the HTML matches and Fetch can reject it.
  */
  surgeryDate: string = undefined;

  /* Rows for the table. null until Fetch succeeds. */
  surgeryList: SurgeryDetail[] = null;

  /* Turns the error div in the HTML on and off. */
  showMsgDiv: boolean = false;
  errorMsg: string = '';

  /*
    Angular injects both objects.
    curewellService is CurewellService (curewell.service.ts) and
    performs the HTTP calls.
    router is used by editSurgery() to open the update page.
  */
  constructor(private curewellService: CurewellService, private router: Router) { }

  /*
    Runs once after the component is created, before the user clicks
    anything. Loads the dropdown so the *ngFor in the HTML has data.
  */
  ngOnInit() {
    this.getAllSurgeryDates();
  }

  /*
    GET /api/CureWell/GetAllSurgeryDates through the service.
    subscribe() is how the component receives the HTTP result:
      next    runs when the body arrives and stores it in surgeryDates,
              which the template *ngFor is already watching
      error   runs when the call fails
  */
  getAllSurgeryDates() {
    this.curewellService.getAllSurgeryDates().subscribe({
      next: (dates) => {
        this.surgeryDates = dates;
      },
      error: (error) => {
        this.surgeryDates = null;
        this.errorMsg = error;
      }
    });
  }

  /*
    Called by the Fetch button, (click)="getSurgery()".
    The first branch matches the placeholder option ([ngValue]="undefined").
    Otherwise the selected string is sent as the surgeryDate query
    parameter. The returned array is surgeryList, which the table's
    *ngIf and *ngFor read.
    complete runs after a successful next and only writes to the console.
  */
  getSurgery() {
    if (this.surgeryDate == null || this.surgeryDate == undefined) {
      this.showMsgDiv = true;
      alert('Please select proper surgery date');
      return;
    }

    this.curewellService.getSurgeries(this.surgeryDate).subscribe({
      next: (response) => {
        this.surgeryList = response;
        this.showMsgDiv = false;
      },
      error: (error) => {
        this.surgeryList = null;
        this.errorMsg = error;
        this.showMsgDiv = true;
        alert('Some error occurred. Please try after sometime');
      },
      complete: () => {
        console.log('Surgery Fetched Successfully');
      }
    });
  }

  /*
    Called by Edit Surgery Time, (click)="editSurgery(item)".
    router.navigate builds one URL segment per argument. The order
    must match the :parameter names on the editSurgery path in
    app.routing.ts, because UpdateSurgeryComponent reads them back
    by those names in ngOnInit().

    surgeryDate is passed as the API sent it (2026-10-23T00:00:00).
    The update page keeps only the first 10 characters for the
    date input. doctorId and surgeryCategory are not shown as
    editable fields, but the update PUT body still needs them.
  */
  editSurgery(surgery: SurgeryDetail) {
    this.router.navigate([
      '/editSurgery',
      surgery.surgeryId,
      surgery.specializationName,
      surgery.surgeryDate,
      surgery.startTime,
      surgery.endTime,
      surgery.doctorName,
      surgery.doctorId,
      surgery.surgeryCategory
    ]);
  }
}
