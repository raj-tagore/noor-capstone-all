# 01 — Reading the method tables

When you need this: the `ps` images are tables. Each row is one method you fill in. Do not invent extra screens.

## What each column means

| Column | You do |
|---|---|
| Method name | Keep the given name and parameters |
| “invoke the appropriate method of curewell.service.ts” | Call the matching service method, do not call `HttpClient` from the component |
| “store the response in `status` / `doctorList` / …” | Assignment inside `subscribe` |
| “display … using an alert box” | `alert('exact text from the table')` |
| “navigate to …” | `this.router.navigate(['/path'])` after the alert |
| “on completion, display … in the console” | `console.log` in the `complete` handler |

## Service vs component

| Layer | Job |
|---|---|
| `curewell.service.ts` (`ps/17`) | HTTP verb + URL + body. Always `.pipe(catchError(this.errorHandler))` |
| component `.ts` | `subscribe`, set the named fields, `alert`, `navigate` |
| component `.html` | `*ngFor`, `(click)`, `ngForm` validation text |

## Return shape from the API (`ps/51`)

| Action | Success | Failure |
|---|---|---|
| Get doctors / specializations / today’s surgeries | array | `null` |
| Add / update doctor, update surgery, delete doctor | `true` | `false` |

The Angular `Doctor` interface uses `doctorId` / `doctorName`. ASP.NET writes JSON in camelCase, so those names match the C# `DoctorId` / `DoctorName`.

## Route paths already declared

```text
viewDoctors
viewSpecialization
viewTodaySurgery
addDoctor
editDoctorDetails/:doctorId/:doctorName
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
```

Surgery route lists **end time before start time**. The service method `editSurgery` uses that same order.
