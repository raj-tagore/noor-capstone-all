# 06 — CurewellService

When you need this: `ps/17`. File: `src/app/curewell-services/curewell.service.ts`. HTTP shape is in [02](02-http-and-catcherror.md).

## Bodies the commits send

Add — id is `0`; the database generates it:

```typescript
addDoctor(doctorName: string): Observable<boolean> {
  const doctor: Doctor = { doctorId: 0, doctorName: doctorName };
  return this.http.post<boolean>(this.baseUrl + 'AddDoctor', doctor)
    .pipe(catchError(this.errorHandler));
}
```

Edit doctor:

```typescript
const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName };
return this.http.put<boolean>(this.baseUrl + 'UpdateDoctorDetails', doctor)
  .pipe(catchError(this.errorHandler));
```

Edit surgery — parameter order is `endTime` then `startTime`, matching the route:

```typescript
editSurgery(doctorId: number, endTime: number, startTime: number,
            surgeryCategory: string, surgeryDate: Date, surgeryId: number) {
  const surgery: Surgery = {
    doctorId, endTime, startTime, surgeryCategory,
    surgeryDate: surgeryDate as unknown as string,
    surgeryId
  };
  return this.http.put<boolean>(this.baseUrl + 'UpdateSurgery', surgery)
    .pipe(catchError(this.errorHandler));
}
```

`Surgery.surgeryDate` on the interface is `string`. The route param is already a string, so the cast only satisfies the `Date` parameter type.

Delete — body is the whole `Doctor`, not an id in the URL:

```typescript
deleteDoctor(doctor: Doctor): Observable<boolean> {
  return this.http
    .request<boolean>('delete', this.baseUrl + 'DeleteDoctor', { body: doctor })
    .pipe(catchError(this.errorHandler));
}
```

Gets are `http.get<T[]>(this.baseUrl + 'ActionName')` with the same `pipe`.

## Exam variations

1. Post `{ doctorName }` only — the API model also has `DoctorId`; sending `doctorId: 0` matches the class.
2. PUT the surgery with start/end swapped — the C# properties are named, so the **object** keys matter, not the argument order. The argument order matters when the component calls `editSurgery`.
3. `DELETE` with no body — the action signature takes `Doctor dObj`, so the id never arrives.
