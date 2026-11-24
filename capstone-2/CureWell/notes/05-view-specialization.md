# Question 5 — Show the specializations

View Specializations is the doctor list again, with different names. The page shell already has the table, the `*ngFor`, and the empty heading.

From the method table:

- `ngOnInit` calls `getSpecialization()`.
- `getSpecialization` calls the service. On success, store the array in `specializationList`. On failure, set `specializationList` to `null`, store the failure in `errorMsg`, and set `showMsgDiv`. On completion, `console.log('Specialization Fetched Successfully')`.

The figure has three columns: Specialization Code, Specialization Name, and a View Doctors button. No method in the Angular tables describes that button. Render it. It does not need a `(click)`.

The service method is `getAllSpecializations`. The API action is `GetSpecializations` (the action name and the service method name differ). The JSON matches the interface already in the project:

```typescript
export interface Specialization {
  specializationName: string;
  specializationCode: string;
}
```

Sample rows: `GYN` Gynecologist, `CAR` Cardiologist, `ANE` Anesthesiologist.

Already used: GET, `catchError`, `subscribe`, the table, `*ngFor`, `{{ }}`.

## What actually changes

The GET is question 1’s `getDoctors` with a new type and a new action name:

```typescript
getAllSpecializations(): Observable<Specialization[]> {
  return this.http
    .get<Specialization[]>(this.baseUrl + 'GetSpecializations')
    .pipe(catchError(this.errorHandler));
}
```

The component follows `getDoctor`, writing different fields:

```typescript
ngOnInit() {
  this.getSpecialization();
}

getSpecialization() {
  this._curewellService.getAllSpecializations().subscribe({
    next: (res) => {
      this.specializationList = res;
      this.showMsgDiv = !(res && res.length > 0);
    },
    error: (error) => {
      this.specializationList = null;
      this.errorMsg = error;
      this.showMsgDiv = true;
    },
    complete: () => {
      console.log('Specialization Fetched Successfully');
    }
  });
}
```

`showMsgDiv` is the new part. On the doctor page the empty heading is `*ngIf="!(doctorList?.length > 0)"`, so the flag did not change the screen. This template is:

```html
<div *ngIf="showMsgDiv">
  <h3 class="jumbotron" style="text-align:center;">No Specialization Available</h3>
</div>
```

The heading is visible only when `showMsgDiv` is true. Set it true when the list failed or came back empty. Leave it false when `res` has rows, or the heading sits under a full table. `!(res && res.length > 0)` is that condition: no array, or an array of length 0.

The error row still assigns `null` and `errorMsg`, as in question 1.

## The row

The loop variable in the shell is `singlespec`, not `doctor`. Property names come from the interface.

```html
<tr *ngFor="let singlespec of specializationList" id="get_specialization" style="background-color:white">
  <td>{{singlespec.specializationCode}}</td>
  <td>{{singlespec.specializationName}}</td>
  <td><button class="btn">View Doctors</button></td>
</tr>
```

A button with no `(click)` still renders and does nothing. That matches this figure. Adding a click that filters doctors would be a new feature. The service method list has no “doctors by specialization” call.

Open View Specializations. The three sample rows appear, each with a View Doctors button, and the console shows `Specialization Fetched Successfully`. The empty heading stays hidden.
