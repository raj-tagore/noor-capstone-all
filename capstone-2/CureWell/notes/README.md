# CureWell notes

What you write: `CurewellService` HTTP calls, component methods that subscribe and navigate, and the template-driven validation. Database, DAL, and how to start Visual Studio are already set up.

Source of the answers: the feature commits in `CureWellApp` (`View Doctors`, `specializations`, `getTodaysSurgery`, `Add doctor`, `UpdateDoctorDetails`, `UpdateSurgery`, `DeleteDoctor`). Method tables are in `CureWellApp/ps`.

## Read order

| File | Commit / PS |
|---|---|
| [01-reading-the-problem.md](01-reading-the-problem.md) | Which method, which variable, which message |
| [02-http-and-catcherror.md](02-http-and-catcherror.md) | `HttpClient` + `pipe(catchError)` |
| [03-subscribe.md](03-subscribe.md) | `next` / `error` / `complete` |
| [04-router.md](04-router.md) | `navigate` and `snapshot.params` |
| [05-template-forms.md](05-template-forms.md) | `required`, dirty, touched, readonly |
| [06-service.md](06-service.md) | Every method in `curewell.service.ts` |
| [07-view-doctors.md](07-view-doctors.md) | `getDoctor`, navigate to edit |
| [08-view-specialization.md](08-view-specialization.md) | `getSpecialization` |
| [09-view-todays-surgery.md](09-view-todays-surgery.md) | `getTodaySurgery`, navigate to edit |
| [10-add-doctor.md](10-add-doctor.md) | `addDoctor` |
| [11-update-doctor.md](11-update-doctor.md) | route params + `editDoctorDetails` |
| [12-update-surgery.md](12-update-surgery.md) | route param **order** + `editSurgery` |
| [13-delete-doctor.md](13-delete-doctor.md) | `removeDoctor` |
| [14-api-actions.md](14-api-actions.md) | Controller actions the service calls (`ps/51`, `ps/52`) |
| [15-doctor-view-edit-flow.md](15-doctor-view-edit-flow.md) | View Doctor and Edit Doctor, from the click through `SaveChanges` |

## If the PS says X

| PS | Open |
|---|---|
| consume the web API | [02](02-http-and-catcherror.md), [06](06-service.md) |
| store the response in a variable / alert / navigate | [03](03-subscribe.md) |
| values from the URL | [04](04-router.md), [11](11-update-doctor.md), [12](12-update-surgery.md) |
| “Name is required” / start time < end time | [05](05-template-forms.md) |
| returns true/false or a list or null | [14](14-api-actions.md) |
