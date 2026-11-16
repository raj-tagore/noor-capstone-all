import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CurewellService } from '../../curewell-services/curewell.service';
import { SurgeryDetail } from '../../curewell-interfaces/surgery';

/*
  Update Surgery screen.

  templateUrl is update-surgery.component.html. The inputs there bind
  to the properties below, and the buttons call canUpdate, editSurgery,
  and cancel.

  This component is not opened by a selector in another template.
  app.routing.ts loads it when the URL matches the editSurgery path.
  ViewSurgeryComponent.editSurgery() is the code that builds that URL.
*/
@Component({
  selector: 'app-update-surgery',
  templateUrl: './update-surgery.component.html'
})
export class UpdateSurgeryComponent implements OnInit {
  /* Shown in the read-only inputs, and sent again on update. */
  surgeryId: number = 0;
  specializationName: string = '';
  doctorName: string = '';

  /*
    Bound to the date input. Must stay yyyy-MM-dd.
    ngOnInit cuts the route value down to that shape.
  */
  surgeryDate: string = '';

  /* Bound to the number inputs. null means "still empty". */
  startTime: number = null;
  endTime: number = null;

  /*
    Not edited on this page. Carried in the route so the PUT body
    still matches SurgeryDetail in curewell-interfaces/surgery.ts.
  */
  doctorId: number = 0;
  surgeryCategory: string = '';

  /* Remembered from the PUT response. The alerts read it too. */
  status: boolean = false;
  errorMsg: string = '';

  /*
    route is ActivatedRoute: the current URL and its parameters.
    curewellService performs PUT UpdateSurgeryDetails.
    router sends the user back to the list.
  */
  constructor(private route: ActivatedRoute, private curewellService: CurewellService, private router: Router) { }

  /*
    snapshot.params is the dictionary of :names from app.routing.ts.
    The names and the order were chosen to match the navigate() call
    in view-surgery.component.ts.

    Example URL:
    /editSurgery/5000/Anesthesiologist/2026-10-23T00:00:00/9/14/Albert/1001/ANE

    substring(0, 10) turns 2026-10-23T00:00:00 into 2026-10-23 so the
    <input type="date"> in the HTML can display it.
    Number() converts the time and id segments from strings.
  */
  ngOnInit() {
    this.surgeryId = Number(this.route.snapshot.params['surgeryId']);
    this.specializationName = this.route.snapshot.params['specializationName'];
    this.surgeryDate = String(this.route.snapshot.params['surgeryDate']).substring(0, 10);
    this.startTime = Number(this.route.snapshot.params['startTime']);
    this.endTime = Number(this.route.snapshot.params['endTime']);
    this.doctorName = this.route.snapshot.params['doctorName'];
    this.doctorId = Number(this.route.snapshot.params['doctorId']);
    this.surgeryCategory = this.route.snapshot.params['surgeryCategory'];
  }

  /*
    Used by the range *ngIf under each time input.
    +value forces a number, because a number input can hand back a string.
    0.00 through 23.59 is the allowed 24-hour range from the problem statement.
  */
  timeOutOfRange(value: number): boolean {
    return value !== null && value !== undefined && (+value < 0 || +value > 23.59);
  }

  /*
    Used by the date *ngIf. The problem statement requires the surgery
    date to be greater than today, so today itself is rejected.

    The comparison uses yyyy-MM-dd text, the same text the date input
    stores. new Date("2026-10-23") would be UTC midnight, which in
    India is 05:30 on that calendar day and would treat "today"
    incorrectly against local midnight.
  */
  dateNotAfterToday(): boolean {
    if (!this.surgeryDate) {
      return false;
    }
    const now = new Date();
    const today = `${now.getFullYear()}-${String(now.getMonth() + 1).padStart(2, '0')}-${String(now.getDate()).padStart(2, '0')}`;
    return this.surgeryDate.substring(0, 10) <= today;
  }

  /*
    Used by the "End Time should not be less than Start Time" div.
    Equal times are allowed. Only a start that is after the end fails.
  */
  endBeforeStart(): boolean {
    return this.startTime !== null && this.endTime !== null && +this.startTime > +this.endTime;
  }

  /*
    Bound in the HTML as [disabled]="!canUpdate()".
    Returns true when every rule above passes, whether or not the
    user has clicked into each box. touched and dirty stay in the
    template, where they decide when the red text appears.
  */
  canUpdate(): boolean {
    return this.startTime !== null && this.endTime !== null && !!this.surgeryDate
      && !this.timeOutOfRange(this.startTime)
      && !this.timeOutOfRange(this.endTime)
      && !this.dateNotAfterToday()
      && !this.endBeforeStart();
  }

  /*
    Called by (ngSubmit) on the form, which the Update button triggers.
    Builds one SurgeryDetail and hands it to CurewellService.editSurgery().
    That method PUTs the object to /api/CureWell/UpdateSurgeryDetails.
    The API returns true or false (CureWellController.UpdateSurgeryDetails).

    next:
      true  shows the success alert
      false shows "not updated" (the repository found no row, or save failed)
      either way the list is shown again with router.navigate
    error:
      the service's errorHandler ran (network or HTTP failure)
  */
  editSurgery() {
    const surgery: SurgeryDetail = {
      surgeryId: this.surgeryId,
      doctorId: this.doctorId,
      doctorName: this.doctorName,
      surgeryCategory: this.surgeryCategory,
      specializationName: this.specializationName,
      surgeryDate: this.surgeryDate,
      startTime: +this.startTime,
      endTime: +this.endTime
    };

    this.curewellService.editSurgery(surgery).subscribe({
      next: (response) => {
        this.status = response;
        if (response) {
          alert('Surgery details updated successfully!');
        } else {
          alert('Surgery details not updated');
        }
        this.router.navigate(['/viewSurgery']);
      },
      error: (error) => {
        this.errorMsg = error;
        alert('Some error occurred');
        this.router.navigate(['/viewSurgery']);
      },
      complete: () => {
        console.log('Updated surgery details successfully');
      }
    });
  }

  /* Cancel button. Leaves the form without calling the service. */
  cancel() {
    this.router.navigate(['/viewSurgery']);
  }
}
