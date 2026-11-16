import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SurgeryDetail } from '../curewell-interfaces/surgery';

/*
  The only class that talks to the Web API.

  ViewSurgeryComponent and UpdateSurgeryComponent call these methods.
  They do not build URLs themselves.

  providedIn: 'root' registers one shared instance for the whole app,
  so the constructors can ask for CurewellService without listing it
  in app.module.ts. HttpClient still needs HttpClientModule there.

  baseUrl is the CureWellController route prefix:
  [Route("api/[controller]/[action]")] in
  CureHospitalWebService/Controllers/CureWellController.cs.
  The API listens on http://localhost:5110 (launchSettings.json).
*/
@Injectable({
  providedIn: 'root'
})
export class CurewellService {
  private readonly baseUrl = 'http://localhost:5110/api/CureWell/';

  constructor(private http: HttpClient) { }

  /*
    Called from ViewSurgeryComponent.ngOnInit via getAllSurgeryDates().
    Fills the date dropdown. The action returns List<string> of yyyy-MM-dd
    values from CureHospitalRepositoryEF.GetAllSurgeryDates.
    pipe(catchError(...)) turns a failed HTTP call into the error
    callback of subscribe().
  */
  getAllSurgeryDates(): Observable<string[]> {
    return this.http
      .get<string[]>(this.baseUrl + 'GetAllSurgeryDates')
      .pipe(catchError(this.errorHandler));
  }

  /*
    Called from ViewSurgeryComponent.getSurgery() when Fetch is clicked.
    { params: { surgeryDate } } becomes the query string
    ?surgeryDate=2026-10-23, which binds to the DateTime argument of
    CureWellController.GetSurgeryDetails.
    The JSON array is SurgeryDetail[] for the table.
  */
  getSurgeries(surgeryDate: string): Observable<SurgeryDetail[]> {
    return this.http
      .get<SurgeryDetail[]>(this.baseUrl + 'GetSurgeryDetails', { params: { surgeryDate } })
      .pipe(catchError(this.errorHandler));
  }

  /*
    Called from UpdateSurgeryComponent.editSurgery() on submit.
    PUT sends the SurgeryDetail object as JSON. Property names are
    camelCase here and on the C# model; ASP.NET binds them.
    The action returns bool from the repository's UpdateSurgeryDetails.
  */
  editSurgery(surgery: SurgeryDetail): Observable<boolean> {
    return this.http
      .put<boolean>(this.baseUrl + 'UpdateSurgeryDetails', surgery)
      .pipe(catchError(this.errorHandler));
  }

  /*
    Shared by every call above. throwError makes subscribe()'s error
    path run in the component, which shows the alert and sets errorMsg.
  */
  errorHandler(error: HttpErrorResponse) {
    return throwError(() => error.message || 'ERROR');
  }
}
