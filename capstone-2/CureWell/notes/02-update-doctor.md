# Question 2 — Edit a doctor’s name

View Doctors already lists each doctor. This question adds Edit Doctor Details on each row, opens Update Doctor with that doctor’s id and name filled in, and saves the new name.

From the method tables:

- `editDoctorDetails(doctor)` on the list navigates to the update page and passes the doctor.
- `ngOnInit` on the update page copies `doctorId` and `doctorName` out of the URL.
- `editDoctorDetails(doctorname)` on the update page calls the service. Store the boolean in `status`. If it is true, `alert("Doctor's name updated successfully!")`. If it is false, `alert("Doctor's name not updated")`. Either way, navigate to View Doctors. On a failed request, store `errorMsg`, `alert('Some error occurred')`, and navigate to View Doctors. On completion, `console.log('Updated doctor details successfully.')`.

The update template must show the id, require the name, and show `Name is required` only when that box is touched and dirty. The figure’s button says Update. The method table calls the same button Save. Either label calls `editDoctorDetails`. Use Update so the page matches the figure. Cancel goes back to View Doctors.

`editDoctorDetails` in the service PUTs `{ doctorId, doctorName }` to `UpdateDoctorDetails` and returns `boolean`.

Already used, explained in question 1: `HttpClient`, `pipe(catchError(this.errorHandler))`, `subscribe`, `next` / `error` / `complete`, `{{ }}`.

## A click and a route

The row from question 1 gains a button. `(click)` calls a method on the component and can pass the row’s doctor:

```html
<td>
  <button class="btn" (click)="editDoctorDetails(doctor)">Edit Doctor Details</button>
</td>
```

| Form | When to use it |
|---|---|
| `(click)="editDoctorDetails(doctor)"` | pass the object `*ngFor` created |
| `(click)="editDoctorDetails(doctor.doctorId)"` | pass one field. The method parameter must then be a number |
| `(click)="editDoctorDetails()"` | the method closes over something else, or takes no argument |

`editDoctorDetails` on the list only changes the URL:

```typescript
editDoctorDetails(doctor: Doctor) {
  this.router.navigate(['/editDoctorDetails', doctor.doctorId, doctor.doctorName]);
}
```

`navigate` takes an array. The first entry is the path. Each later entry fills the next `:param` in the route that is already declared:

```text
editDoctorDetails/:doctorId/:doctorName
```

Albert becomes `/editDoctorDetails/1001/Albert`. Angular destroys the list component and creates `UpdateDoctorComponent`. The list’s `doctorList` is gone. The URL is the only copy of this doctor.

Other ways to move:

```html
<a routerLink="/viewDoctors">Cancel</a>
<a [routerLink]="['/editDoctorDetails', doctor.doctorId, doctor.doctorName]">Edit</a>
```

```typescript
this.router.navigate(['/viewDoctors']);
this.router.navigate(['/editDoctorDetails'], { queryParams: { doctorId: 1001 } });
this.router.navigateByUrl('/viewDoctors');
```

| Form | What it produces |
|---|---|
| array after the path | path segments: `/editDoctorDetails/1001/Albert` |
| `queryParams` | a query string: `/editDoctorDetails?doctorId=1001` |
| `routerLink` | the same navigation from an `<a>`, with no method |
| `navigateByUrl` | one string, already joined |

This exam uses path segments, because that is how `app.routing.ts` is written. Query params would not fill `:doctorId`.

Cancel is a link, not a save:

```html
<a routerLink="/viewDoctors">Cancel</a>
```

## Reading the URL

The update component has its own fields, already declared: `doctorId`, `doctorName`, `status`, `errorMsg`. `ActivatedRoute` is already injected as `this.route`.

```typescript
ngOnInit() {
  this.doctorId = Number(this.route.snapshot.params['doctorId']);
  this.doctorName = this.route.snapshot.params['doctorName'];
}
```

| Piece | Meaning |
|---|---|
| `this.route` | the current URL, for this component |
| `snapshot` | the URL at the moment `ngOnInit` runs |
| `params['doctorId']` | the segment named `:doctorId`. The key is the name in the route, not the left-to-right position |
| `Number(...)` | every param arrives as a string. `"1001"` must become `1001` before it is stored in a `number` field |

`doctorName` stays a string, so it is assigned directly.

The other way to read params stays listening if the URL changes without leaving the page:

```typescript
this.route.params.subscribe(p => {
  this.doctorId = Number(p['doctorId']);
  this.doctorName = p['doctorName'];
});
```

Opening this page always creates a new component, so `snapshot` is enough.

`snapshot.paramMap.get('doctorId')` is the same value as `snapshot.params['doctorId']`. `paramMap` returns strings and has a `.get` method. `queryParamMap` reads `?doctorId=1001` and is the wrong map for this route.

## PUT

Question 1 used GET with no body. An update sends the doctor:

```typescript
editDoctorDetails(doctorId: number, doctorName: string): Observable<boolean> {
  const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName };
  return this.http
    .put<boolean>(this.baseUrl + 'UpdateDoctorDetails', doctor)
    .pipe(catchError(this.errorHandler));
}
```

The object keys must be `doctorId` and `doctorName`, because those are the JSON names. Argument order of the method does not matter once the object is built. `put<boolean>` means a successful body is `true` or `false`, not a doctor. `catchError` is the same call as in `getDoctors`.

`post` would create. This action updates an existing row, so the verb is `put`.

## alert, then leave the page

`alert('text')` is a browser dialog. The method table fixes the two success-path sentences, including the apostrophe in `Doctor's`:

```typescript
editDoctorDetails(doctorname: string) {
  this._cureWellService.editDoctorDetails(this.doctorId, doctorname).subscribe({
    next: (response) => {
      this.status = response;
      if (response) {
        alert("Doctor's name updated successfully!");
      } else {
        alert("Doctor's name not updated");
      }
      this.router.navigate(['/viewDoctors']);
    },
    error: (error) => {
      this.errorMsg = error;
      alert('Some error occurred');
      this.router.navigate(['/viewDoctors']);
    },
    complete: () => {
      console.log('Updated doctor details successfully.');
    }
  });
}
```

`response` is the boolean. Both branches navigate, and the error branch navigates too. The list component is created again, so question 1’s `ngOnInit` loads the names once more.

`complete` logs only after a successful HTTP response. The period at the end of `Updated doctor details successfully.` is part of the text in the table.

The service field on this component is `_cureWellService`, with a capital W. The list component spells it `_curewellService`. Use the name already in each constructor.

## ngForm

The update shell has a `<form>` and two inputs, and no bindings. A template-driven form keeps its state in the template. You do not build a `FormGroup` in the class.

`FormsModule` is already imported in `app.module.ts`. That import is what makes `ngForm` and `ngModel` exist. Without it, the template names are unknown.

### The directive is already on the form

Writing `<form>` is enough for Angular to attach the `NgForm` directive. You do not add a separate `ngForm` attribute to turn that on. The word `ngForm` shows up when you want a name for the directive that is already there:

```html
<form #updateDoctorForm="ngForm" (ngSubmit)="editDoctorDetails(doctorName)">
```

| Piece | Meaning |
|---|---|
| `#updateDoctorForm` | a name that exists only in this template. It is not a field on the class |
| `="ngForm"` | export the `NgForm` directive into that name. `ngForm` is the directive’s export name |
| `(ngSubmit)` | Angular’s submit event. Enter in a field fires it, and so does a `type="submit"` button |
| `editDoctorDetails(doctorName)` | the argument is the component field. The input keeps that field up to date |

`(submit)` is the browser’s own event and also fires. Use `(ngSubmit)` with `ngForm`. A `(click)` on the button misses the Enter key.

`NgForm` also sets `novalidate` on the `<form>`. The browser then stays quiet. The `Name is required` element you write is the message the user sees, rather than the browser’s tooltip.

If you skip `#updateDoctorForm="ngForm"`, the directive still tracks the controls. You just have no name to read `.valid` from in the template.

### What gets registered

A control joins this form when it sits inside the `<form>` and has both `name` and `ngModel`. The `name` is the key.

```html
<form #updateDoctorForm="ngForm">
  <input name="doctorId" [(ngModel)]="doctorId">
  <input name="doctorname" [(ngModel)]="doctorName" required>
</form>
```

After the page has filled Albert in, `updateDoctorForm.value` is:

```text
{ doctorId: 1001, doctorname: 'Albert' }
```

The keys are the `name` attributes. They are not the class field names. Here the class field is `doctorName` and the form key is `doctorname`.

| Input | On the form? |
|---|---|
| inside the form, with `name` and `ngModel` | yes, under that `name` |
| inside the form, missing `name` | no. `ngModel` throws, because it cannot register |
| outside the `<form>` | no. It is a separate box |
| a second `<form>` lower on the page | that is a different `NgForm` |

### The directive and the model under it

`#updateDoctorForm` is the `NgForm` directive. `updateDoctorForm.form` is the `FormGroup` that directive owns. For this page they answer the same questions:

```html
updateDoctorForm.valid
updateDoctorForm.form.valid
updateDoctorForm['form'].valid
```

All three are true when every registered control is valid. The exam markup uses the bracket form, so keep `updateDoctorForm['form'].valid` on the button. `updateDoctorForm.valid` is the same flag read on the directive itself.

### What you can read

| Expression | Value |
|---|---|
| `updateDoctorForm.valid` / `.invalid` | every control passed / one of them failed |
| `updateDoctorForm.submitted` | the user has submitted this form once |
| `updateDoctorForm.dirty` / `.pristine` | some control changed / none has |
| `updateDoctorForm.touched` | some control was focused, then left |
| `updateDoctorForm.value` | the object of `name` → current value |
| `updateDoctorForm.controls['doctorname']` | that one control |
| `updateDoctorForm.form.controls` | the same map on the `FormGroup` |

`controls['doctorname']` is the control whose `name` is `doctorname`. `#doctorname="ngModel"` on the input is another handle to that same control. Use the `#` name in the template next to the input. Use `.controls[...]` when you only have the form.

### Which one to read

The form and one control answer different questions.

| You need | Read | Why |
|---|---|---|
| the “Name is required” line under one box | `#doctorname="ngModel"`, then `doctorname.invalid` | `updateDoctorForm.invalid` is also true when the id box fails. It cannot tell you which message to show |
| the Update button disabled | `updateDoctorForm.invalid` or `!updateDoctorForm['form'].valid` | you care that the whole form is ready, not which field failed |
| errors only after they click Update, even if they never touched the box | `updateDoctorForm.submitted && doctorname.invalid` | `submitted` flips on `ngSubmit` and stays true |
| the text the method should save | the class field `doctorName` | `ngModel` has already copied the box into the field. This exam’s method takes that string |
| every current value in one object | `updateDoctorForm.value` | keys are the `name`s. `doctorname` does not match the `Doctor` interface’s `doctorName`, so this page does not post `.value` |
| a blank form after a successful save | `updateDoctorForm.resetForm()` | this exam navigates away instead of clearing |

`submitted` is the variation for “they tried to save”. This problem statement wants the message when the box is touched and dirty, so the `*ngIf` below uses those two flags, not `submitted`.

Pass the form into the method when the method itself must check it or clear it:

```html
<form #updateDoctorForm="ngForm" (ngSubmit)="editDoctorDetails(updateDoctorForm)">
```

```typescript
editDoctorDetails(form: NgForm) {
  if (form.invalid) {
    return;
  }
  const name = form.value['doctorname'];
}
```

Import `NgForm` from `@angular/forms` if you do that. This exam’s method is already `editDoctorDetails(doctorname: string)`, so the template passes `doctorName` and the class never mentions `NgForm`.

Clearing is a method on the same object:

```html
<button type="button" (click)="updateDoctorForm.resetForm()">Clear</button>
```

| Call | What it clears |
|---|---|
| `reset()` | values, and the dirty / touched flags |
| `resetForm()` | the same, and `submitted` |
| `resetForm({ doctorId: 1001 })` | clears, then puts those values back |

`type="button"` keeps Clear from submitting the form.

Each input needs a `name` and `ngModel`, or it is just a box the form ignores:

```html
<input type="text" id="doctorID" name="doctorId" class="form-control qkinput"
       [(ngModel)]="doctorId" readonly>

<input type="text" id="docname" class="form-control qkinput" name="doctorname"
       required [(ngModel)]="doctorName" #doctorname="ngModel">
```

`[(ngModel)]="doctorName"` is two-way binding. It is the same as:

```html
<input [ngModel]="doctorName" (ngModelChange)="doctorName = $event">
```

After `ngOnInit`, the box shows `this.doctorName` (`[ngModel]` writes class to screen). Each keystroke writes the box back into `this.doctorName` (`(ngModelChange)`). Submit can pass `doctorName` without reading the DOM.

`name="doctorname"` registers the control. Without `name`, `ngModel` throws. The `name` does not have to equal the component field. Here the field is `doctorName` and the control name is `doctorname`.

`#doctorname="ngModel"` exports that one control, the same way `#updateDoctorForm="ngForm"` exports the form. From `#doctorname` you can read:

| Flag | True when |
|---|---|
| `valid` / `invalid` | the validators pass or fail |
| `touched` / `untouched` | the user focused the control, then left it (blur) |
| `dirty` / `pristine` | the user changed the value / the user has not changed it |
| `errors` | an object such as `{ required: true }`, or `null` |

`required` is the validator. An empty box is `invalid`. Other validators you can put on an input, and this page does not need:

```html
<input required minlength="2" maxlength="25" pattern="[A-Za-z ]+">
<input type="number" min="0" max="24">
<input type="email" email>
```

`minlength`, `maxlength`, `pattern`, `min`, `max`, and `email` each add their own key on `errors`. `required` is the only one in the problem statement.

The message is a separate element. It is shown only when all three flags are true, which is the sentence in the problem:

```html
<div *ngIf="doctorname.invalid && doctorname.touched && doctorname.dirty">
  Name is required
</div>
```

Touched but not dirty: the user focused the box and left without typing. The message stays hidden. Dirty but not touched is rare (the value changed from code). The table wants both.

`readonly` on the id box keeps the id visible and blocks typing. `disabled` also blocks typing, and a disabled control is left out of some form submissions. Use `readonly` for Doctor Id.

The Update button stays disabled while the form is invalid:

```html
<button type="submit" class="btn" [disabled]="!updateDoctorForm['form'].valid">Update</button>
<a routerLink="/viewDoctors">Cancel</a>
```

`type="submit"` is what fires `ngSubmit`. `[disabled]` is a property binding. `updateDoctorForm` is the local template name. `.form` is the underlying form model, and `['form']` is the same property read with brackets. These templates already use bracket access elsewhere, so keep `updateDoctorForm['form'].valid`.

`updateDoctorForm.valid` is the form directive’s own flag and means the same thing. `updateDoctorForm.invalid` is the opposite, useful as `[disabled]="updateDoctorForm.invalid"`.

The id input has no `required`, so only the name can disable Update.

## Code for this question

Service method: the PUT block above.

List component: `editDoctorDetails(doctor)` navigates with id and name.

List template, inside the existing `*ngFor` row, after the two cells from question 1:

```html
<td>
  <button class="btn" (click)="editDoctorDetails(doctor)">Edit Doctor Details</button>
</td>
```

Update component: `ngOnInit` and `editDoctorDetails(doctorname)` as above.

Update template: the form, both inputs, the message, Update, and Cancel as above.

Click Edit Doctor Details on Albert. The URL is `/editDoctorDetails/1001/Albert`, Doctor Id shows 1001, and Doctor Name shows Albert. Clear the name, click away, and type nothing: `Name is required` appears after the box is dirty and you leave it. Save a new name. The dialog says `Doctor's name updated successfully!`, the URL returns to View Doctors, and the table shows the new name.
