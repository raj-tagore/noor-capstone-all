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
    //To do implement necessary logic
    return null;
  }

  //GetDoctorById — GET api/CureWell/GetDoctorById/{doctorId}
  getDoctorById(doctorId: number): Observable<Doctor> {
    //To do implement necessary logic
    return null;
  }

  //GetDoctorByEmail — GET api/CureWell/GetDoctorByEmail?email=
  getDoctorByEmail(email: string): Observable<Doctor> {
    //To do implement necessary logic
    return null;
  }

  //GetDoctorByObject — POST api/CureWell/GetDoctorByObject with the doctor JSON
  getDoctorByObject(doctor: Doctor): Observable<Doctor> {
    //To do implement necessary logic
    return null;
  }

  //GetSpecialization
  getAllSpecializations(): Observable<Specialization[]> {
    //To do implement necessary logic
    return null;
  }

  //GetSurgeries
  getAllSurgeriesForToday(): Observable<Surgery[]> {
    //To do implement necessary logic
    return null;
  }

  //AddDoctor
  addDoctor(doctorName: string): Observable<boolean> {
    //To do implement necessary logic
    return null;
  }

  //EditDoctor
  editDoctorDetails(doctorId: number, doctorName: string): Observable<boolean> {
    //To do implement necessary logic
    return null;
  }

  //editSurgery
  editSurgery(doctorId: number, endTime: number, startTime: number, surgeryCategory: string, surgeryDate: Date, surgeryId: number): Observable<boolean> {
    //To do implement necessary logic
    return null;
  }

  //RemoveDoctor
  deleteDoctor(doctor: Doctor): Observable<boolean> {
    //To do implement necessary logic
    return null;
  }

  //ErrorHandler
  errorHandler(error: HttpErrorResponse) {
    //To do implement necessary logic
    return null;
  }

}
