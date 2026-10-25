# View Doctor and Edit Doctor

What this screen pair does: list every doctor, then open one doctor’s name in a form, edit it, and save it. The list is `ViewDoctorComponent`. The form is `UpdateDoctorComponent`. They do not share fields. The URL carries `doctorId` and `doctorName` from one page to the other.

## 1. View Doctor, from the click to the table

The navbar link is only a route change:

```html
<a routerLink="viewDoctors">View Doctors</a>
<router-outlet></router-outlet>
```

`app.routing.ts` maps that path to the component:

```typescript
{ path: 'viewDoctors', component: ViewDoctorComponent }
```

Angular creates `ViewDoctorComponent` and puts its template inside `<router-outlet>`. `ngOnInit` runs once, and it only calls `getDoctor()`.

```typescript
ngOnInit() {
  this.getDoctor();
}

getDoctor() {
  this._curewellService.getDoctors().subscribe({
    next: (res) => {
      this.doctorList = res;
      this.showMsgDiv = true;
      console.log('Doctors Fetched Successfully');
    },
    error: (error) => {
      this.doctorList = null;
      this.errorMsg = error;
      this.showMsgDiv = true;
    }
  });
}
```

`getDoctors()` returns an `Observable`. Nothing is on the network until `subscribe`.

```typescript
getDoctors(): Observable<Doctor[]> {
  return this.http
    .get<Doctor[]>(this.baseUrl + 'GetDoctors')
    .pipe(catchError(this.errorHandler));
}
```

`baseUrl` is `http://localhost:5109/api/CureWell/`, so the request is `GET /api/CureWell/GetDoctors`.

On the API, `GetDoctors` calls `rep.GetAllDoctors()`, copies each row into `CureWellServices.Models.Doctor`, and `return Json(list)`. The repository query is:

```csharp
doctors = (from doctor in Context.Doctors select doctor).ToList();
```

ASP.NET writes JSON in camelCase (`doctorId`, `doctorName`). That matches the TypeScript interface:

```typescript
export interface Doctor {
  doctorId: number;
  doctorName: string;
}
```

`next` stores that array on `this.doctorList`. The template reads the same field:

```html
<tr *ngFor="let doctor of doctorList">
  <td>{{doctor.doctorId}}</td>
  <td>{{doctor.doctorName}}</td>
  <td><button (click)="editDoctorDetails(doctor)">Edit Doctor Details</button></td>
</tr>
```

`*ngFor` repeats the row once per doctor. `{{ }}` prints the property. The button does not contain the id; it passes the whole `doctor` object into the method.

```typescript
editDoctorDetails(doctor: Doctor) {
  this.router.navigate(['/editDoctorDetails', doctor.doctorId, doctor.doctorName]);
}
```

For doctor 1001 / Albert the URL becomes `/editDoctorDetails/1001/Albert`.

## 2. Edit Doctor, from the URL to the saved name

The route declares the two segments by name:

```typescript
{ path: 'editDoctorDetails/:doctorId/:doctorName', component: UpdateDoctorComponent }
```

`UpdateDoctorComponent` is a new object. It does not see `doctorList`. `ngOnInit` copies the URL into its own fields:

```typescript
ngOnInit() {
  this.doctorId = Number(this.route.snapshot.params['doctorId']);
  this.doctorName = this.route.snapshot.params['doctorName'];
}
```

`params['doctorId']` is the string `"1001"`. `Number(...)` makes it `1001`. `doctorName` stays a string.

The template binds those fields:

```html
<form #updateDoctorForm="ngForm" (ngSubmit)="editDoctorDetails(doctorName)">
  <input name="doctorId" [(ngModel)]="doctorId" readonly>
  <input name="doctorname" required [(ngModel)]="doctorName" #doctorname="ngModel">
  <button type="submit" [disabled]="!updateDoctorForm['form'].valid">Update</button>
</form>
```

`[(ngModel)]="doctorName"` shows `"Albert"` because `ngOnInit` already set that field. The id box is `readonly`, so the user can see it and cannot change it.

Submit calls `editDoctorDetails` with the current name:

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

The service builds the body and sends PUT:

```typescript
editDoctorDetails(doctorId: number, doctorName: string): Observable<boolean> {
  const doctor: Doctor = { doctorId: doctorId, doctorName: doctorName };
  return this.http
    .put<boolean>(this.baseUrl + 'UpdateDoctorDetails', doctor)
    .pipe(catchError(this.errorHandler));
}
```

Body: `{ "doctorId": 1001, "doctorName": "Albert" }`. URL: `PUT /api/CureWell/UpdateDoctorDetails`.

`UpdateDoctorDetails` copies that into a DAL `Doctor` and calls `rep.UpdateDoctorDetails`. The repository loads the row, changes one property, and saves:

```csharp
updatedocObj = Context.Doctors.Find(dObj.DoctorId);
if (updatedocObj != null)
{
    updatedocObj.DoctorName = dObj.DoctorName;
    Context.SaveChanges();
    return true;
}
```

`true` comes back as JSON `true`. `next` alerts and `navigate(['/viewDoctors'])` destroys the edit component and creates `ViewDoctorComponent` again, so `ngOnInit` fetches the list with the new name.

`complete` runs only after a successful HTTP response. It does not run in `error`.

## 3. How the HTML and the TypeScript meet

| HTML | TypeScript |
|---|---|
| `routerLink="viewDoctors"` | route table picks `ViewDoctorComponent` |
| `*ngFor="let doctor of doctorList"` | `this.doctorList` set in `subscribe` |
| `{{doctor.doctorName}}` | read-only print of that property |
| `(click)="editDoctorDetails(doctor)"` | method on the component class |
| `[(ngModel)]="doctorName"` | `this.doctorName` |
| `(ngSubmit)="editDoctorDetails(doctorName)"` | method, argument is the field’s current value |
| `#doctorname="ngModel"` | not a class field; it is the input’s validity state |

Angular creates the component, then renders the template against that instance. `doctorList` in the HTML is `this.doctorList`. A click handler is the class method of the same name.

One-way display is `{{ }}` or `[property]="value"` (class → screen). An event is `(click)` or `(ngSubmit)` (screen → class). Two-way is both directions on the same input.

## 4. Two-way binding

```html
<input [(ngModel)]="doctorName">
```

is the same idea as:

```html
<input [ngModel]="doctorName" (ngModelChange)="doctorName = $event">
```

- Class → input: after `ngOnInit`, the box shows `this.doctorName`.
- Input → class: each keystroke writes the box text back into `this.doctorName`.

That is why the edit page can open with “Albert” already filled in, and why submit can pass `doctorName` without reading the DOM. `name="doctorname"` is required so Angular registers this input on the `ngForm`.

`[(ngModel)]="doctorId"` on the readonly box still shows the id. `readonly` blocks typing. It does not remove the binding.

## 5. Where validation runs

Validation here is template-driven. There is no `FormBuilder`.

```html
<input name="doctorname" required [(ngModel)]="doctorName" #doctorname="ngModel">
<div *ngIf="doctorname.invalid && doctorname.touched && doctorname.dirty">
  Name is required
</div>
<button type="submit" [disabled]="!updateDoctorForm['form'].valid">Update</button>
```

| Flag | Becomes true when |
|---|---|
| `invalid` | `required` fails (empty name) |
| `touched` | the user focused the box, then left it |
| `dirty` | the user changed the text |
| `updateDoctorForm['form'].valid` | every registered control is valid |

The message is hidden until all three are true, which is the rule in the problem statement. The button stays disabled while the form is invalid, so submit does not call `editDoctorDetails` with an empty name.

`#updateDoctorForm="ngForm"` is the whole form. `#doctorname="ngModel"` is only the name box. The id box has no `required`, so it does not block submit.

Add Doctor uses the same three flags and the text `Name is required`. It also sets `toggleButton` from `doctorNameField.invalid` and disables the button with that flag.

## 6. The whole update path

```text
View Doctors link
  -> ViewDoctorComponent.ngOnInit
  -> CurewellService.getDoctors
  -> GET /api/CureWell/GetDoctors
  -> CureWellController.GetDoctors
  -> CureWellRepository.GetAllDoctors
  -> doctorList
  -> *ngFor

Edit Doctor Details
  -> navigate /editDoctorDetails/:doctorId/:doctorName
  -> UpdateDoctorComponent.ngOnInit copies params
  -> [(ngModel)] shows the name

Update
  -> editDoctorDetails(doctorName)
  -> PUT /api/CureWell/UpdateDoctorDetails  { doctorId, doctorName }
  -> repository Find + DoctorName + SaveChanges
  -> true / false
  -> alert
  -> navigate /viewDoctors
  -> list loads again
```
