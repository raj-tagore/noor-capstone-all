# Question 7 — Change a surgery’s times

Edit Surgery Time already puts six values in the URL. This page reads them, shows them, and lets the user change start and end time.

From the method table:

- `ngOnInit` sets `surgeryId`, `surgeryCategory`, `surgeryDate`, `startTime`, `endTime`, and `doctorId` from the route.
- `editSurgery(startTime, endTime)` runs from Update. It calls the service. Store the boolean in `status`. If it is true, `alert('Surgery details updated successfully!')` and go to View Today’s Surgery. If it is false, `alert('Surgery details not updated')` and go to the same page. On failure, store `errorMsg`, `alert('Some error occurred')`, and navigate there too. On completion, `console.log('Updated surgery details successfully')`.

The template, from the problem statement and the figure:

- Surgery Id, Doctor Id, Surgery Date, and Surgery Category are filled in and not editable. The statement names Doctor Id, Surgery Date, and Surgery Category as read-only. The figure also shows Surgery Id as a fixed value, so mark that `readonly` too.
- Start Time and End Time are required. The messages are `Start time is required` and `End time is required`, shown when that box is touched and dirty.
- Start time must be earlier than end time. Use 24-hour numbers (`9.00`, `17`, `19.30`), the same encoding as question 6.

The service method is already declared with this parameter order:

```typescript
editSurgery(doctorId: number, endTime: number, startTime: number,
            surgeryCategory: string, surgeryDate: Date, surgeryId: number)
```

End time is the second parameter. Start time is the third. That matches the route.

Already used: route `snapshot.params`, `Number(...)`, PUT, `ngForm`, `required`, touched, dirty, `readonly`, `alert`, then `navigate`.

## Reading six names

Question 2 read two params. These six use the same `snapshot.params['name']`. The name is the `:name` in the route, so you can assign them in any order inside `ngOnInit`:

```text
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
```

```typescript
ngOnInit() {
  this.surgeryId = Number(this.route.snapshot.params['surgeryId']);
  this.surgeryCategory = this.route.snapshot.params['surgeryCategory'];
  this.surgeryDate = this.route.snapshot.params['surgeryDate'];
  this.startTime = Number(this.route.snapshot.params['startTime']);
  this.endTime = Number(this.route.snapshot.params['endTime']);
  this.doctorId = Number(this.route.snapshot.params['doctorId']);
}
```

`doctorId`, `startTime`, `endTime`, and `surgeryId` are numbers, so they go through `Number`. `surgeryCategory` and `surgeryDate` stay strings. The date must not go through `Number`. `2011-01-01T00:00:00` is not a number, and the update page shows that string in the date box.

`params['startTime']` is correct even though start time is the third segment. Position mattered when question 6 built the URL. Here the route’s name matters.

## The PUT body

The component method takes the two times the user can edit. The other four values are already on the class.

```typescript
editSurgery(startTime: number, endTime: number) {
  this._cureWellService.editSurgery(
    this.doctorId,
    endTime,
    startTime,
    this.surgeryCategory,
    this.surgeryDate,
    this.surgeryId
  ).subscribe({
    next: (response) => {
      this.status = response;
      if (response) {
        alert('Surgery details updated successfully!');
      } else {
        alert('Surgery details not updated');
      }
      this.router.navigate(['/viewTodaySurgery']);
    },
    error: (error) => {
      this.errorMsg = error;
      alert('Some error occurred');
      this.router.navigate(['/viewTodaySurgery']);
    },
    complete: () => {
      console.log('Updated surgery details successfully');
    }
  });
}
```

The call passes `endTime` and then `startTime`, because that is the service signature. The subscribe block is question 2’s update block: store `status`, alert on true and false, navigate in every branch, log in `complete`. The path is `/viewTodaySurgery`. The error spelling on this row is `occurred`.

The service builds a `Surgery` and PUTs it. Keys are the interface names. Argument order stops mattering once the object exists.

```typescript
editSurgery(doctorId: number, endTime: number, startTime: number,
            surgeryCategory: string, surgeryDate: Date, surgeryId: number): Observable<boolean> {
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
```

`Surgery.surgeryDate` is a `string`. The method parameter is typed as `Date`. The value you actually hold is the route string. `as unknown as string` tells the compiler to treat it as the field’s type. The JSON still contains the date string the API already returned.

Calling `editSurgery(this.doctorId, startTime, endTime, ...)` swaps the times in the body. The C# properties are filled by name from the JSON, and the JSON gets its values from this object. The object must put the end value in `endTime`.

## Two required boxes, then a comparison

Each time box is the name box from question 2: `name`, `[(ngModel)]`, `required`, and a template reference. The messages are different, and there are two of them.

```html
<form #updateSurgeryForm="ngForm" (ngSubmit)="editSurgery(startTime, endTime)">
```

`(ngSubmit)` passes the two component fields. The form reference is how the button knows the required fields are filled.

Read-only boxes have `[(ngModel)]` and `readonly`, and no `required`:

```html
<input type="text" name="surgeryId" id="surgeryID" class="form-control qkinput"
       [(ngModel)]="surgeryId" readonly>
<input type="text" name="doctorId" id="doctorID" class="form-control qkinput"
       [(ngModel)]="doctorId" readonly>
<input type="text" name="surgeryDate" id="surgeryDate" class="form-control qkinput"
       [(ngModel)]="surgeryDate" readonly>
<input type="text" name="surgeryCategory" id="surgeryCategory" class="form-control qkinput"
       [(ngModel)]="surgeryCategory" readonly>
```

Keep the ids already in the shell: `surgeryID`, `doctorID`, `surgeryDate`, `starttime`, `endtime`, `surgeryCategory`.

```html
<input type="text" name="starttime" id="starttime" class="form-control qkinput"
       required [(ngModel)]="startTime" #startTimeField="ngModel">
<div *ngIf="startTimeField.invalid && startTimeField.touched && startTimeField.dirty">
  Start time is required
</div>

<input type="text" name="endtime" id="endtime" class="form-control qkinput"
       required [(ngModel)]="endTime" #endTimeField="ngModel">
<div *ngIf="endTimeField.invalid && endTimeField.touched && endTimeField.dirty">
  End time is required
</div>
```

`required` only notices an empty box. It does not compare the two numbers. That comparison is a second `*ngIf`, shown when both boxes are valid and the numbers are out of order:

```html
<div *ngIf="startTimeField.valid && endTimeField.valid && +startTime >= +endTime">
  Start time must be less than end time
</div>
```

`+startTime` turns the value into a number before comparing. An input can hold the string `"17"`. `"9" > "14"` as text, because `"9"` sorts after `"1"`. `+` makes it `9 > 14`, which is false, so 9.00 before 14.00 is accepted. `>=` also rejects equal times. The problem says start must be less than end.

The same test disables Update. `required` can pass while the times are still in the wrong order:

```html
<button type="submit" class="btn"
        [disabled]="!updateSurgeryForm['form'].valid || +startTime >= +endTime">
  Update
</button>
<a routerLink="/viewTodaySurgery">Cancel</a>
```

`||` keeps the button off when the form is invalid or the times are out of order. Cancel uses the list path from question 6.

A validator on the form group could do this comparison instead of the template expression. That is a different style (`FormBuilder`, a custom validator function). These pages are template-driven. Keep the comparison in the `*ngIf` and on `[disabled]`.

## Code for this question

Service: the PUT block above.

Component: `ngOnInit` and `editSurgery(startTime, endTime)` as above.

Template: four readonly inputs, two required time inputs, both messages, the start-before-end message, Update, and Cancel.

From today’s list, Edit Surgery Time opens the form with every field filled. Clearing Start Time and leaving the box shows `Start time is required`. Entering a start that is greater than or equal to the end shows `Start time must be less than end time` and Update stays disabled. A start of 9 and an end of 14 saves, alerts `Surgery details updated successfully!`, and returns to today’s list.
