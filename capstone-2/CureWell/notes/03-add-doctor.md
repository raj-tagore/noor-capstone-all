# Question 3 — Add a doctor

Add Doctor is a separate page. The user types a name. The page posts it and shows a line of text under the form. It does not navigate away.

From the method table for `addDoctor(doctorName)`:

- Call the service.
- On success, store the boolean in `status`, set `showDiv`, and set `msg` to `Doctor successfully added`.
- On failure, store the error in `errorAddMsg` and set `msg` to `Some error occured`.
- On completion, `console.log('Add doctor completed')`.

The template requires the name. The message is `Name is required`, shown when the box is touched and dirty. After a successful add, the figure shows `Doctor successfully added` in red under the buttons.

The service method receives the name, builds a `Doctor`, and POSTs it to `AddDoctor`.

Already used: GET and PUT (questions 1 and 2), `subscribe`, `ngForm`, `[(ngModel)]`, `required`, touched, dirty, `[disabled]`.

## POST

POST creates a row. The database assigns `DoctorId` (identity, starting at 1001), so the client sends `0`:

```typescript
addDoctor(doctorName: string): Observable<boolean> {
  const doctor: Doctor = { doctorId: 0, doctorName: doctorName };
  return this.http
    .post<boolean>(this.baseUrl + 'AddDoctor', doctor)
    .pipe(catchError(this.errorHandler));
}
```

This is the PUT from question 2 with three changes: the verb is `post`, the URL action is `AddDoctor`, and `doctorId` is `0`. The pipe is unchanged.

Sending `{ doctorName }` only would drop `doctorId`. The `Doctor` interface has both properties, and the API model has both. Send both.

## A message on the page

Question 2 reported the result with `alert` and then left the page. This question writes a string into `msg` and lets the template show it. `showDiv` is the switch.

```typescript
addDoctor(doctorName: string) {
  this._curewellService.addDoctor(doctorName).subscribe({
    next: (response) => {
      this.status = response;
      this.showDiv = true;
      if (response) {
        this.msg = 'Doctor successfully added';
      } else {
        this.msg = 'Some error occured';
      }
    },
    error: (error) => {
      this.errorAddMsg = error;
      this.showDiv = true;
      this.msg = 'Some error occured';
    },
    complete: () => {
      console.log('Add doctor completed');
    }
  });
}
```

The table says the success text is set “on successful addition”. `response === false` is an HTTP success whose body says the insert did not happen, so that branch uses the failure text. The error spelling is `occured`, the one in this method table.

`complete` still runs after `next`, including when `response` is false, because the HTTP call itself succeeded. It does not run in `error`.

The template prints `msg` only after `showDiv` is true. The red colour is the inline style from the figure:

```html
<div *ngIf="showDiv" style="color:red;text-align:center;">
  {{msg}}
</div>
```

`*ngIf` was on the doctor table in question 1. Here it wraps one line of text.

## What is different on this form

The shell’s form has a reference and no submit handler:

```html
<form align="center" #addDoctorForm="ngForm">
```

Add the handler. The argument is the component field the input is bound to:

```html
<form align="center" #addDoctorForm="ngForm" (ngSubmit)="addDoctor(doctorName)">
```

The name box gains the same three pieces as Update Doctor: two-way binding, `required`, and a template reference.

```html
<input type="text" id="add_doctorName" class="form-control cwinput" name="doctorName"
       required [(ngModel)]="doctorName" #doctorNameField="ngModel">

<div id="add_validate_doctorName"
     *ngIf="doctorNameField.invalid && doctorNameField.touched && doctorNameField.dirty">
  Name is required
</div>
```

Keep `id="add_doctorName"`. The validation element’s id `add_validate_doctorName` is the usual id for this page. The condition is the same touched-and-dirty rule as question 2.

One extra behaviour is on this page only. The class already has `toggleButton`, starting at `true`, so the button starts disabled. Each change of the model refreshes it:

```html
<input ... (ngModelChange)="toggleButton = doctorNameField.invalid">
```

`(ngModelChange)` fires with the new string. You do not have to use `$event` if what you need is the control’s `invalid` flag. After one character, `invalid` is false and the button enables. Clearing the box disables it again.

```html
<button class="btn" type="submit" [disabled]="toggleButton">Add Doctor</button>
<a routerLink="/viewDoctors">Cancel</a>
```

Question 2 disabled the button with `!updateDoctorForm['form'].valid`. This page disables it with `toggleButton`. Both mean “the name is not currently valid”. Use `toggleButton` here because the field is already on the class.

`ngModelChange` is also the long form of two-way binding from question 2. You can listen to it and still use `[(ngModel)]`.

## Code for this question

Service: the POST block above.

Component: `addDoctor` as above. `ngOnInit` can stay empty. Nothing is loaded from a route.

Template: `ngSubmit`, the bound required input, the validation div, the button, Cancel, and the red `msg` div.

Submit a name. A red `Doctor successfully added` appears under the buttons, and the console shows `Add doctor completed`. The URL stays on Add Doctor. View Doctors shows the new row. Submit with the name cleared, after the box has been touched and dirty, and the page shows `Name is required` instead of calling the API.
