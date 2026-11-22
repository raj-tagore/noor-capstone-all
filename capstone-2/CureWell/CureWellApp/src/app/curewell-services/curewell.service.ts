import { Injectable } from '@angular/core';
import { Doctor } from '../curewell-interfaces/doctor';
import { DoctorSpecialization } from '../curewell-interfaces/doctorspecialization';
import { Specialization } from '../curewell-interfaces/specialization';
import { Surgery } from '../curewell-interfaces/surgery';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CurewellService {
  private readonly baseUrl = "http://localhost:5109/api/CureWell/"

  doctorList: Doctor[]=[];
  surgeryList: Surgery[]=[];
  specializationList: Specialization[]=[];
  doctorSpecializationList: DoctorSpecialization[]=[];

  constructor(private http: HttpClient) { }
  
  //GetDoctor
  getDoctors(): Observable<Doctor[]> {
    return this.http
      .get<Doctor[]>(this.baseUrl+'GetDoctors')
      .pipe(catchError(this.errorHandler));
  }

  //GetSpecialization
  getAllSpecializations(): Observable<Specialization[]> {
   //To do implement necessary logic
    return this.http
      .get<Specialization[]>(this.baseUrl+'GetSpecializations').pipe(catchError(this.errorHandler));
  }

  //GetSurgeries
  getAllSurgeriesForToday(): Observable<Surgery[]> {
    return this.http
      .get<Surgery[]>(this.baseUrl + 'GetAllSurgeryTypeForToday')
      .pipe(catchError(this.errorHandler));
  }

  //AddDoctor
  addDoctor(doctorName: string): Observable<boolean> {
    const doctor: Doctor = { doctorId: 0, doctorName: doctorName };
    return this.http
      .post<boolean>(this.baseUrl + 'AddDoctor', doctor)
      .pipe(catchError(this.errorHandler));
  }

  //EditDoctor
  editDoctorDetails(doctorId: number, doctorName: string): Observable<boolean> {
    const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName };
    return this.http
      .put<boolean>(this.baseUrl + 'UpdateDoctorDetails', doctor)
      .pipe(catchError(this.errorHandler));
  }

  //editSurgery
  editSurgery(doctorId: number, endTime: number, startTime: number, surgeryCategory: string, surgeryDate: Date, surgeryId: number): Observable<boolean> {
    const surgery: Surgery = {
      doctorId: doctorId,
      endTime: endTime,
      startTime: startTime,
      surgeryCategory: surgeryCategory,
      surgeryDate: surgeryDate as unknown as string,
      surgeryId: surgeryId
    };
    return this.http
      .put<boolean>(this.baseUrl + 'UpdateSurgery', surgery)
      .pipe(catchError(this.errorHandler));
  }

  //RemoveDoctor
  deleteDoctor(doctor: Doctor): Observable<boolean> {
    return this.http
      .request<boolean>('delete', this.baseUrl + 'DeleteDoctor', { body: doctor })
      .pipe(catchError(this.errorHandler));
  }

  //ErrorHandler
  errorHandler(error: HttpErrorResponse) {
    //To do implement necessary logic
    return throwError(error.message || 'ERROR')

  }

}
