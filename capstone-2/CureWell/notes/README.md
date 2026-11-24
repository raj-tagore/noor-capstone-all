# CureWell notes

Work through these in order. Each file is one exam question. A concept is explained in the question that first needs it. Later questions use that concept and only explain what is new.

The screenshots and method tables are in `CureWellApp/ps`. The method names, fields, routes, and page shells are already in `CureWellApp`. Fill in the method bodies and the `<!--Add your logic here-->` spots.

| Question | File | What is new |
|---|---|---|
| 1. Show every doctor | [01-view-doctors.md](01-view-doctors.md) | `HttpClient` GET, `Observable`, `catchError`, `subscribe`, `ngOnInit`, HTML table, `*ngFor`, `{{ }}` |
| 2. Edit a doctor’s name | [02-update-doctor.md](02-update-doctor.md) | `(click)`, `router.navigate`, route params, PUT, `ngForm`, `ngModel`, `required`, `alert` |
| 3. Add a doctor | [03-add-doctor.md](03-add-doctor.md) | POST, a message on the page instead of an alert |
| 4. Delete a doctor | [04-delete-doctor.md](04-delete-doctor.md) | DELETE with a body, then reload the list |
| 5. Show specializations | [05-view-specialization.md](05-view-specialization.md) | Same list pattern. `showMsgDiv` actually controls this template |
| 6. Show today’s surgeries | [06-view-todays-surgery.md](06-view-todays-surgery.md) | `date` pipe, a route with several parameters |
| 7. Change a surgery’s times | [07-update-surgery.md](07-update-surgery.md) | Several route params, start time must be earlier than end time |
| 8. Find one doctor | [08-find-doctor.md](08-find-doctor.md) | GET by id in the path, GET by email in the query string. Not part of the exam |

Routes already declared in `app.routing.ts`:

```text
viewDoctors
viewSpecialization
viewTodaySurgery
addDoctor
editDoctorDetails/:doctorId/:doctorName
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
findDoctor
```

The API base already in the service is `http://localhost:5109/api/CureWell/`. Action names are appended to that string.
