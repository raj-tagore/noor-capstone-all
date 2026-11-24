# Question 1 — Show every doctor

The navbar link View Doctors is already wired to `ViewDoctorComponent`. The page shell already has the heading and an empty table. This question fills that table from the API.

From the method tables:

- `getDoctors` in `curewell.service.ts` calls the web API and returns the doctors.
- `errorHandler` in the same file runs when any call in this service fails.
- `ngOnInit` in `view-doctor.component.ts` calls `getDoctor()`.
- `getDoctor` calls the service. On success, store the array in `doctorList` and set `showMsgDiv`. On failure, set `doctorList` to `null` and store the failure in `errorMsg`. When the fetch succeeds, `console.log('Doctors Fetched Successfully')`.

The figure shows two columns, Doctor Id and Doctor Name, one row per doctor. The Action buttons are questions 2 and 4. In this question the row only needs the two data cells.

Files:

- `src/app/curewell-services/curewell.service.ts`
- `src/app/curewell-components/view-doctor/view-doctor.component.ts`
- `src/app/curewell-components/view-doctor/view-doctor.component.html`

`Doctor` is already declared:

```typescript
export interface Doctor {
  doctorId: number;
  doctorName: string;
}
```

The API returns JSON in that shape, for example `{ "doctorId": 1001, "doctorName": "Albert" }`. A successful `GetDoctors` returns an array. A failure returns `null`.

## HTTP GET

The service is the only file that talks to the API. The component asks the service for doctors. It does not build URLs.

`HttpClient` is already injected as `this.http`. A call looks like this:

```typescript
this.http.get<Doctor[]>(this.baseUrl + 'GetDoctors')
```

| Piece | Meaning |
|---|---|
| `this.http` | the injected client |
| `.get` | HTTP GET. Nothing is sent in a body |
| `<Doctor[]>` | the JSON you expect back. Angular parses it into that type |
| `this.baseUrl + 'GetDoctors'` | `http://localhost:5109/api/CureWell/GetDoctors` |

`get` does not return the array. It returns an `Observable<Doctor[]>`. An observable is a call that has been described and has not run yet. The request leaves the browser only when something subscribes. That is why the component, not the service, calls `subscribe`.

Other verbs use the same client. You will need them in later questions. The shape is the same: verb, type, URL, and sometimes a body.

```typescript
this.http.get<Doctor[]>(url)
this.http.post<boolean>(url, body)
this.http.put<boolean>(url, body)
this.http.delete<boolean>(url, { body: doctor })
```

| Verb | Typical use | Second argument |
|---|---|---|
| `get` | read a list or one record | none |
| `post` | create | the object to insert |
| `put` | replace / update | the object to save |
| `patch` | change a few fields | a partial object. This exam does not use it |
| `delete` | remove | usually just the URL. This API also wants the doctor in the body, which is question 4 |
| `request('delete', url, { body })` | same delete when you want the verb written out as a string | options object |

`get` can also take an options object as a second argument:

```typescript
this.http.get<Doctor[]>(url, {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  params: { specializationCode: 'ANE' }
})
```

`headers` adds HTTP headers. `params` adds a query string (`?specializationCode=ANE`). This exam’s GET actions take neither. The action name is already in the path.

`observe` changes what you receive:

```typescript
this.http.get(url, { observe: 'response' })  // full response, including status
this.http.get(url, { observe: 'body' })      // JSON only. This is the default
```

Stay on the default. The method tables want the list or the boolean, not the status code.

## Which GET to write

`get` is always a read. What changes is the URL, the type in `<...>`, and sometimes `params`. Match the API action. The action’s parameters tell you which form to write.

This exam’s `GetDoctors` takes nothing and returns the list, so the call above is the one that belongs in `getDoctors`. The other forms are for a later action whose signature is different.

### The whole collection

No id, no filter. The type is an array.

```typescript
getDoctors(): Observable<Doctor[]> {
  return this.http
    .get<Doctor[]>(this.baseUrl + 'GetDoctors')
    .pipe(catchError(this.errorHandler));
}
```

Use this when the action has no argument and returns many rows. `GetSpecializations` and `GetAllSurgeryTypeForToday` are the same shape with a different type and action name.

### One record by id

The id goes in the path. The type is one object, not an array.

```typescript
getDoctorById(doctorId: number): Observable<Doctor> {
  return this.http
    .get<Doctor>(this.baseUrl + 'GetDoctorById/' + doctorId)
    .pipe(catchError(this.errorHandler));
}
```

That requests `http://localhost:5109/api/CureWell/GetDoctorById/1001`. Question 8 is this call, against the real action. A template string builds the same URL:

```typescript
this.http.get<Doctor>(`${this.baseUrl}GetDoctorById/${doctorId}`)
```

The component passes the id it already has. `next` receives one `Doctor`, so you read `doctor.doctorName`. There is no `*ngFor`.

```typescript
this._curewellService.getDoctorById(this.doctorId).subscribe({
  next: (doctor) => {
    this.doctorName = doctor.doctorName;
  }
});
```

If the route has no `{doctorId}` slot and the action still takes the id, send it as a query string. `params` turns the object into `?doctorId=1001`:

```typescript
this.http.get<Doctor>(this.baseUrl + 'GetDoctorById', {
  params: { doctorId: doctorId }
})
```

`HttpParams` is the same object written one key at a time. `.set` replaces a key. `.append` adds a second value for the same key (`?id=1&id=2`).

```typescript
const params = new HttpParams().set('doctorId', doctorId.toString());
this.http.get<Doctor>(this.baseUrl + 'GetDoctorById', { params });
```

| The action looks like | You write |
|---|---|
| route contains `{doctorId}` | path: `url + '/' + doctorId` |
| a simple argument, and the route has no `{ }` slot | `params: { doctorId }` |
| no argument, returns a list | path is only the action name |

An id is safe in a path because it is an integer. This exam’s list actions have no id. Leave them as the collection form.

### One record by email

An email contains `@`, and sometimes `+`. A query parameter is the usual place for it. Angular encodes `@` as `%40` when you pass `params`.

```typescript
getDoctorByEmail(email: string): Observable<Doctor> {
  return this.http
    .get<Doctor>(this.baseUrl + 'GetDoctorByEmail', {
      params: { email: email }
    })
    .pipe(catchError(this.errorHandler));
}
```

That requests `.../GetDoctorByEmail?email=albert%40curewell.com`. Question 8 is this call. The practice addresses are `{name}@curewell.com`, such as `albert@curewell.com`.

Use the path only when the route itself says `{email}`. Encode the value yourself, or `@` is read as part of the URL:

```typescript
this.http.get<Doctor>(
  this.baseUrl + 'GetDoctorByEmail/' + encodeURIComponent(email)
)
```

`encodeURIComponent('a@b.com')` is `a%40b.com`.

The type is `Doctor` when the email matches one row, and `Doctor[]` when it can match several. The action’s return value decides that, the same way `GetDoctors` decides `Doctor[]`.

### A full object

GET does not normally carry a body. Servers and proxies often drop one. Holding a `Doctor` in the component does not mean you send that object. You pick the form the action can actually bind.

You have the object, and the route wants an id. Read the field and use the id form. The parameter type can still be `Doctor`.

```typescript
getDoctor(doctor: Doctor): Observable<Doctor> {
  return this.http
    .get<Doctor>(this.baseUrl + 'GetDoctorById/' + doctor.doctorId)
    .pipe(catchError(this.errorHandler));
}
```

The URL contains `1001`. `doctorName` stays on the client.

The action filters on several simple fields. Put those fields in `params`. The object is only where you read them.

```typescript
getDoctorsMatching(doctor: Doctor): Observable<Doctor[]> {
  return this.http
    .get<Doctor[]>(this.baseUrl + 'GetDoctors', {
      params: {
        doctorId: doctor.doctorId,
        doctorName: doctor.doctorName
      }
    })
    .pipe(catchError(this.errorHandler));
}
```

That requests `.../GetDoctors?doctorId=1001&doctorName=Albert`. Use `Doctor[]` when the filter can return many rows, and `Doctor` when the combination is unique.

The action takes one complex parameter as a body. That is a POST, including when the method only searches. Question 8’s action is this case: `POST GetDoctorByObject` with the doctor JSON. Swagger does not show a body editor on a GET.

```typescript
this.http.post<Doctor>(this.baseUrl + 'GetDoctorByObject', doctor)
```

A GET body is the leftover case, and only when the problem says the body is required. `request` is how you attach one. Many hosts ignore it.

```typescript
this.http.request<Doctor[]>('get', this.baseUrl + 'GetDoctors', { body: doctor })
```

| You have | The action | Write |
|---|---|---|
| a `Doctor`, route has `{id}` | one id | `doctor.doctorId` in the path |
| a `Doctor`, action takes `doctorId` and `doctorName` | separate simple arguments | `params` with those fields |
| a `Doctor`, action takes that class as a body | POST | `post(url, doctor)` |
| a `Doctor`, action is GET and the statement requires a body | GET with a body | `request('get', url, { body: doctor })` |

`GetDoctors` on this exam takes no arguments, so `getDoctors` uses none of the id, email, or object forms.

### The type argument

| The API returns | `<...>` | What `next` receives |
|---|---|---|
| a list | `Doctor[]` | the array. `[]` means no rows |
| one row | `Doctor` | the object. Read `.doctorName` |
| one row, or `null` on a miss | `Doctor` | check for `null` before reading a property |

## pipe and catchError

The problem says every service method must handle exceptions through `errorHandler`. You attach that with `pipe`:

```typescript
return this.http
  .get<Doctor[]>(this.baseUrl + 'GetDoctors')
  .pipe(catchError(this.errorHandler));
```

`pipe` chains operators onto the observable before it is returned. `catchError` runs when the HTTP call fails (network error, 404, 500). It must return another observable. It must not throw a plain string.

```typescript
errorHandler(error: HttpErrorResponse) {
  return throwError(() => error.message || 'ERROR');
}
```

| Piece | Meaning |
|---|---|
| `error: HttpErrorResponse` | status, status text, and `message` from the failed response |
| `error.message` | the string the component will see in `subscribe`’s error callback |
| `\|\| 'ERROR'` | used when `message` is empty |
| `throwError(() => ...)` | an observable that fails with that value, so the component’s error path still runs |

`throwError` has two forms:

```typescript
throwError(() => error.message || 'ERROR')  // current RxJS 7 form
throwError(error.message || 'ERROR')        // older form. Still runs. RxJS warns that it is deprecated
```

`catchError` has two ways to call your method:

```typescript
catchError(this.errorHandler)
catchError(err => this.errorHandler(err))
```

The first passes the function itself. That is fine here because `errorHandler` never reads `this`. The second keeps `this` if the handler ever uses a field on the service. Both satisfy the problem.

If you forget `pipe(catchError(...))`, a failure still reaches the component’s error callback, but it is the raw `HttpErrorResponse`, and `errorHandler` never runs. The rubric scores that method on its own.

## subscribe

In the component:

```typescript
this._curewellService.getDoctors().subscribe({
  next: (res) => { },
  error: (error) => { },
  complete: () => { }
});
```

Until `subscribe`, `getDoctors()` only returns a plan. `subscribe` starts the request.

| Handler | When it runs | What the argument is |
|---|---|---|
| `next` | the API responded and `catchError` did not run | the JSON: `Doctor[]` or `null` |
| `error` | the request failed | whatever `errorHandler` forwarded (`error.message`) |
| `complete` | after a successful `next` | nothing |

`complete` does not run after `error`. A message that must appear on failure goes in `error`. A message that must appear only after success can go in `next` or in `complete`. This question says “on successful completion”, so `complete` matches the sentence. Putting the same `console.log` in `next` also prints it on success. Later questions that say “on completion” use `complete`.

The older callback form is the same three steps without names:

```typescript
this._curewellService.getDoctors().subscribe(
  res => { },
  error => { },
  () => { }
);
```

Use the object form. You can omit a handler you do not need. This question uses all three.

Assign the fields the table names. On success the list is the array. On failure the table says `doctorList` is `null`, not `[]`. An empty array is a successful response that happens to contain no rows. `null` means the call failed.

```typescript
next: (res) => {
  this.doctorList = res;
  this.showMsgDiv = true;
},
error: (error) => {
  this.doctorList = null;
  this.errorMsg = error;
  this.showMsgDiv = true;
},
complete: () => {
  console.log('Doctors Fetched Successfully');
}
```

`showMsgDiv` is set in both handlers because the table says to set it, and it does not say to set it only on success. The doctor template you were given does not read `showMsgDiv`. It decides the empty message from the list length. Set the flag anyway. Question 5 is the page where that flag changes what is on screen.

## ngOnInit

```typescript
ngOnInit() {
  this.getDoctor();
}
```

Angular creates the component, runs the constructor, renders the first template, then calls `ngOnInit` once. The constructor is for storing injected services (`_curewellService`, `router`), which is already done. Fetching belongs in `ngOnInit`.

Other lifecycle hooks you will see and do not need here:

| Hook | When |
|---|---|
| constructor | the object is created. Services are available. The template is not |
| `ngOnInit` | first load. Fetch the list here |
| `ngOnChanges` | an `@Input` value changes. These pages have no inputs |
| `ngOnDestroy` | the user navigates away. Use it to unsubscribe if you stored the subscription |

The object form of `subscribe` above is not stored, so these short page loads do not need `ngOnDestroy`.

## The HTML table

The shell already contains the table. Read it before adding cells:

```html
<table align="center" class="table" style="border:2px solid rgba(220,230,242,1);" *ngIf="doctorList">
  <tr style="background-color:rgba(220,230,242,1); font-size:12pt">
    <th>Doctor Id</th>
    <th>Doctor Name</th>
    <th style="text-align:center" colspan="2">Action</th>
  </tr>

  <tr *ngFor="let doctor of doctorList" id="getdoctor" style="background-color:white">
    <!--Add your logic here-->
  </tr>
</table>
```

A table is rows inside `<table>`. Each `<tr>` is a row. `<th>` is a header cell. `<td>` is a data cell. The browser lines them up by position: the first `<td>` sits under the first `<th>`.

```html
<table>
  <tr>
    <th>Doctor Id</th>
    <th>Doctor Name</th>
  </tr>
  <tr>
    <td>1001</td>
    <td>Albert</td>
  </tr>
</table>
```

Variations:

| Form | What changes |
|---|---|
| `<thead>` and `<tbody>` | header rows in `thead`, data rows in `tbody`. Styling and screen readers can tell them apart. The shell uses one flat list of `<tr>` and no `thead` |
| `colspan="2"` | that cell stretches across two columns. The Action header already does this, because the row will eventually hold two buttons |
| `rowspan` | a cell stretches down across rows. Not used here |
| `<caption>` | a title that belongs to the table. This page uses an `<h2>` instead |
| `align` and `style` | old HTML alignment and inline CSS. The shell already has both. Match it |

`*ngIf="doctorList"` on the table removes the whole table when `doctorList` is `null` or otherwise missing. The star means a structural directive: it changes the shape of the page, it does not just change an attribute.

`*ngFor` repeats the element it sits on, once per item:

```html
<tr *ngFor="let doctor of doctorList">
```

| Piece | Meaning |
|---|---|
| `*ngFor` | repeat this `<tr>` |
| `let doctor` | the name of the current item inside this row |
| `of doctorList` | the array on the component (`this.doctorList`) |

`doctor` exists only in this row. `{{doctor.doctorId}}` prints the property. The same print in the class would be `this.doctorList[i].doctorId`.

Other `*ngFor` clauses, if a later question needs them:

```html
<tr *ngFor="let doctor of doctorList; let i = index">
<tr *ngFor="let doctor of doctorList; trackBy: trackById">
```

`index` is 0, 1, 2, … `trackBy` is a method that returns a stable id so Angular can reuse rows when the array is replaced. This exam’s tables do not need either. The variable name is your choice, but the shell already says `let doctor`, and the row id is `getdoctor`. Keep both.

`*ngIf` and `*ngFor` cannot sit on the same element. The shell puts `*ngIf` on the `<table>` and `*ngFor` on the `<tr>`, which is the usual split. The other way to put both on one row is a wrapper that does not render a tag:

```html
<ng-container *ngIf="doctorList">
  <tr *ngFor="let doctor of doctorList"></tr>
</ng-container>
```

`[hidden]="!doctorList"` hides an element with CSS and leaves it in the page. `*ngIf` removes it. The shell uses `*ngIf`.

The empty message under the table is already written:

```html
<div *ngIf="!(doctorList?.length > 0)">
```

`?.` reads `length` only when `doctorList` exists. When the error path sets the list to `null`, this condition is true and the heading “No Doctor Available” shows.

Inside the row, add the two cells. `{{ }}` is interpolation: it turns the value into text.

```html
<tr *ngFor="let doctor of doctorList" id="getdoctor" style="background-color:white">
  <td>{{doctor.doctorId}}</td>
  <td>{{doctor.doctorName}}</td>
</tr>
```

`{{doctor.doctorId}}` is one-way, class to screen. Later questions use two more forms:

```html
<button (click)="editDoctorDetails(doctor)">Edit Doctor Details</button>
<input [value]="doctorId">
```

`(click)` is an event, screen to class. `[value]` is a property binding, class to one DOM property. Interpolation is enough for these two cells.

## Code for this question

Service:

```typescript
getDoctors(): Observable<Doctor[]> {
  return this.http
    .get<Doctor[]>(this.baseUrl + 'GetDoctors')
    .pipe(catchError(this.errorHandler));
}

errorHandler(error: HttpErrorResponse) {
  return throwError(() => error.message || 'ERROR');
}
```

Component:

```typescript
ngOnInit() {
  this.getDoctor();
}

getDoctor() {
  this._curewellService.getDoctors().subscribe({
    next: (res) => {
      this.doctorList = res;
      this.showMsgDiv = true;
    },
    error: (error) => {
      this.doctorList = null;
      this.errorMsg = error;
      this.showMsgDiv = true;
    },
    complete: () => {
      console.log('Doctors Fetched Successfully');
    }
  });
}
```

Open View Doctors. The table lists 1001 Albert, 1002 Olivia, 1003 Susan. The console shows `Doctors Fetched Successfully`. If the API is down, the table disappears and “No Doctor Available” remains.
