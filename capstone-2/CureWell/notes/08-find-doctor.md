# Question 8 — Find one doctor

Find Doctor is extra practice. It is not in the exam screenshots. The API actions are already written. The Angular methods are empty.

Three calls:

| Method | Request | Returns |
|---|---|---|
| `getDoctorById(doctorId)` | `GET http://localhost:5109/api/CureWell/GetDoctorById/1001` | one `Doctor`, or `null` |
| `getDoctorByEmail(email)` | `GET http://localhost:5109/api/CureWell/GetDoctorByEmail?email=albert@curewell.com` | one `Doctor`, or `null` |
| `getDoctorByObject(doctor)` | `POST http://localhost:5109/api/CureWell/GetDoctorByObject` with the doctor JSON as the body | one `Doctor`, or `null` |

The id is a path segment. The email is a query parameter, because it contains `@`. Question 1 explains both forms. The pipe and `subscribe` are the same ones you already use.

There is no Email column in the database. The API builds the address from the doctor’s name: lowercase name, then `@curewell.com`.

| Id | Name | Email |
|---|---|---|
| 1001 | Albert | `albert@curewell.com` |
| 1002 | Olivia | `olivia@curewell.com` |
| 1003 | Susan | `susan@curewell.com` |
| 1004 | Harry | `harry@curewell.com` |
| 1005 | Alice | `alice@curewell.com` |

The JSON for a hit looks like `{ "doctorId": 1001, "doctorName": "Albert", "email": "albert@curewell.com" }`. A miss is JSON `null`, and `next` still runs. `error` runs when the request itself fails.

## Service

```typescript
getDoctorById(doctorId: number): Observable<Doctor> {
  return this.http
    .get<Doctor>(this.baseUrl + 'GetDoctorById/' + doctorId)
    .pipe(catchError(this.errorHandler));
}

getDoctorByEmail(email: string): Observable<Doctor> {
  return this.http
    .get<Doctor>(this.baseUrl + 'GetDoctorByEmail', {
      params: { email: email }
    })
    .pipe(catchError(this.errorHandler));
}

getDoctorByObject(doctor: Doctor): Observable<Doctor> {
  return this.http
    .post<Doctor>(this.baseUrl + 'GetDoctorByObject', doctor)
    .pipe(catchError(this.errorHandler));
}
```

`params` produces `?email=albert%40curewell.com`. Angular encodes the `@`. Do not concatenate the email onto the path.

The object call is `post`, not `get`. A GET is not given a body: Swagger leaves a GET body off the page, and many servers discard it. The action still only looks a doctor up. Send the object you already have:

```typescript
const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName, email: email };
this._curewellService.getDoctorByObject(doctor)
```

The API reads the body in this order: `doctorId` when it is greater than 0, otherwise `email`, otherwise `doctorName`. `{ "doctorId": 1001, "doctorName": "", "email": "" }` returns Albert. `{ "doctorId": 0, "doctorName": "", "email": "olivia@curewell.com" }` returns Olivia. `{ "doctorId": 0, "doctorName": "Susan", "email": "" }` returns Susan.

## Component

`find-doctor.component.ts`. The forms already call these methods. Store the doctor, and clear the previous error. On a miss, `doctor` is `null` and there is no error string.

```typescript
getById(doctorId: number) {
  this._curewellService.getDoctorById(doctorId).subscribe({
    next: (doctor) => {
      this.doctor = doctor;
      this.errorMsg = '';
    },
    error: (error) => {
      this.doctor = null;
      this.errorMsg = error;
    }
  });
}

getByEmail(email: string) {
  this._curewellService.getDoctorByEmail(email).subscribe({
    next: (doctor) => {
      this.doctor = doctor;
      this.errorMsg = '';
    },
    error: (error) => {
      this.doctor = null;
      this.errorMsg = error;
    }
  });
}

getByObject(doctorId: number, doctorName: string, email: string) {
  const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName, email: email };
  this._curewellService.getDoctorByObject(doctor).subscribe({
    next: (found) => {
      this.doctor = found;
      this.errorMsg = '';
    },
    error: (error) => {
      this.doctor = null;
      this.errorMsg = error;
    }
  });
}
```

## Result markup

Replace `<!--Add your logic here-->` in `find-doctor.component.html`. `*ngIf="doctor"` hides the block when the call returned `null`.

```html
<div *ngIf="doctor">
  <p>Id: {{doctor.doctorId}}</p>
  <p>Name: {{doctor.doctorName}}</p>
  <p>Email: {{doctor.email}}</p>
</div>
<div *ngIf="!doctor && errorMsg">
  {{errorMsg}}
</div>
```

Swagger reads the actions from the process that is running. A page left open from before these actions were added still shows the old list. Stop that API, start it again, and reload `http://localhost:5109/swagger`. The CureWell section then lists `GET /api/CureWell/GetDoctorById/{doctorId}`, `GET /api/CureWell/GetDoctorByEmail`, and `POST /api/CureWell/GetDoctorByObject`.

Find by id with `1001` shows Albert. Find by email with `olivia@curewell.com` shows Olivia. Find by object with id `0`, an empty email, and name `Susan` shows Susan. An unknown id, email, or name leaves the result empty.
