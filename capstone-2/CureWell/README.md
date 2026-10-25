# CureWell

Hospital app for listing doctors and specializations, adding and updating doctors, and updating surgery times. Three processes have to be running: SQL Server LocalDB, the ASP.NET Core API, and the Angular app.

| Piece | Folder | Address |
|---|---|---|
| Database | `CureWellDataAccessLayer/CureWell.sql` | `(localdb)\MSSQLLocalDB`, database `CureWellDB` |
| API | `CureWellServices` | http://localhost:5109 |
| Angular app | `CureWellApp` | http://localhost:4200 |

Leave the API terminal and the Angular terminal open while you use the app.

## Prerequisites

- .NET 8 runtime. The projects target `net8.0`. A newer SDK can build them, but the .NET 8 runtime still has to be installed or `dotnet run` fails with "You must install or update .NET".
- Node.js and npm. The app is Angular 15 with TypeScript 4.8.
- SQL Server LocalDB (`MSSQLLocalDB`) and `sqlcmd`.

## 1. Create the database

From any folder:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "CureWellDataAccessLayer\CureWell.sql"
```

Use the full path to the script if you are not inside `CureWell`. The script drops `CureWellDB` if it already exists, then creates it and inserts the sample doctors, specializations, and surgeries. Running it again wipes anything you added through the app.

Check the rows:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -d CureWellDB -Q "SELECT DoctorID, DoctorName FROM Doctor"
```

You should see Albert, Olivia, Susan, Harry, and Alice.

The sample surgeries are dated 2011 and 2015. `GetAllSurgeryTypeForToday` returns only rows whose date is today, so View Today's Surgery stays empty until you insert one:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -d CureWellDB -Q "INSERT INTO Surgery (DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory) VALUES (1001, CAST(GETDATE() AS date), 9.00, 14.00, 'ANE')"
```

## 2. Start the API

```powershell
cd CureWellServices
dotnet run --launch-profile http
```

Open http://localhost:5109/swagger. `GET /api/CureWell/GetDoctors` should return the doctors as JSON.

Use the `http` profile. The Angular service calls `http://localhost:5109`. The `https` profile listens on https://localhost:7029 as well, and the browser will not trust that certificate until you run `dotnet dev-certs https --trust`.

## 3. Start the Angular app

In a second terminal:

```powershell
cd CureWellApp
npm install
npm start
```

Open http://localhost:4200/. The first `npm install` and the first compile each take a few minutes. Later starts are faster. You are ready when the terminal prints `Compiled successfully` and `http://localhost:4200/`.

This project is Angular 15. On Node 17 or newer, `npm start` can fail with an OpenSSL error. Stop it and run:

```powershell
$env:NODE_OPTIONS="--openssl-legacy-provider"
npm start
```

`tsconfig.json` already has `skipLibCheck`, and `package.json` pins `@types/node` to 18. Without that pin, a current Node install pulls `@types/node` 26, which TypeScript 4.8 cannot compile.

## Important config

**Database connection.** Both `CureWellServices/appsettings.json` and `CureWellDataAccessLayer/appsettings.json` use:

```text
data source=(localdb)\MSSQLLocalDB;initial catalog=CureWellDB;Integrated Security=true
```

`CureWellDbContext` reads `CureWellDBConnectionString` from `appsettings.json` in the process working directory. When you start the API from `CureWellServices`, that is the API project's file.

**API URL and port.** `CureWellServices/Properties/launchSettings.json`, profile `http`, sets `http://localhost:5109`. Swagger opens at `/swagger`.

The Angular base URL is hardcoded in `CureWellApp/src/app/curewell-services/curewell.service.ts`:

```text
http://localhost:5109/api/CureWell/
```

Controller routes are `api/[controller]/[action]`, so the calls are:

| Action | Method |
|---|---|
| `/api/CureWell/GetDoctors` | GET |
| `/api/CureWell/GetSpecializations` | GET |
| `/api/CureWell/GetAllSurgeryTypeForToday` | GET |
| `/api/CureWell/AddDoctor` | POST |
| `/api/CureWell/UpdateDoctorDetails` | PUT |
| `/api/CureWell/UpdateSurgery` | PUT |
| `/api/CureWell/DeleteDoctor` | DELETE, with the doctor in the body |

**CORS.** The Angular app on port 4200 calls the API on port 5109. `CureWellServices/Program.cs` allows `http://localhost:4200`. Restart the API after changing that. Swagger works without CORS because it is served by the API itself.

**Angular routes.** Declared in `CureWellApp/src/app/app.routing.ts`:

- `viewDoctors`, `viewSpecialization`, `viewTodaySurgery`, `addDoctor`
- `editDoctorDetails/:doctorId/:doctorName`
- `editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId`

The surgery route lists end time before start time. The service method `editSurgery` uses that same order.

**Do not change method signatures.** Repository methods in `CureWellDataAccessLayer/CureWellRepository.cs` and controller actions in `CureWellServices/Controllers/CureWellController.cs` are marked "Do not modify the signature".
