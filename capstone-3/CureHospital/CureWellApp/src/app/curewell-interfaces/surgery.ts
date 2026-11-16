/*
  Shape of one surgery row on the Angular side.

  view-surgery.component.html reads these fields inside *ngFor
  (item.surgeryId, item.doctorName, and the rest).
  UpdateSurgeryComponent.editSurgery() builds an object of this
  shape and CurewellService.editSurgery() PUTs it.

  The names are camelCase because the API JSON is camelCase.
  They line up with CureHospitalDALCrossPlatform.Models.SurgeryDetail:
    surgeryId, doctorId, doctorName, surgeryCategory,
    specializationName, surgeryDate, startTime, endTime.
  surgeryDate travels as text. The list page receives
  2026-10-23T00:00:00 and the update page sends yyyy-MM-dd.
*/
export interface SurgeryDetail {
  surgeryId: number;
  doctorId: number;
  doctorName: string;
  surgeryCategory: string;
  specializationName: string;
  surgeryDate: string;
  startTime: number;
  endTime: number;
}
