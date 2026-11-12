# CureHospital FA1 retest

Practice copy of the CureHospital exam: SQL Server, Entity Framework data access, and the ASP.NET Core Web API. Solution lines are wrapped in `SOLUTION START` / `SOLUTION END` comments. Signatures, regions, and the `//To Do` lines are the given starter.

There is no Angular app in this problem statement. The graded work is the database script, `CureHospitalRepositoryEF`, and `CureHospitalController`.

## What was recreated

- `CureHospitalDALCrossPlatform` with the models, `CureHospitalDB.sql`, and the repository
- `CureHospitalWebService` on `http://localhost:5111`
- `CureHospitalTest`, the console project the exam says to set as the startup project

The exam database name is `CureHospitalDB`. This copy creates `CureHospitalFA1DB` on `(localdb)\MSSQLLocalDB` so the database used by `capstone-3` is not dropped. The connection string name is `DBConnectionString`, in both `appsettings.json` and `EduAppsettings.json`.

## Run it

1. Install the .NET 8 runtime and SQL Server LocalDB.
2. From `capstone-fa1-retest\CureHospital`, create the database:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "CureHospitalDALCrossPlatform\CureHospitalDB.sql"
```

3. Run the sample checks:

```powershell
dotnet run --project CureHospitalTest
```

4. Start the API:

```powershell
dotnet run --project CureHospitalWebService --launch-profile http
```

Swagger is at `http://localhost:5111/swagger`. Action URLs look like `http://localhost:5111/api/CureHospital/FetchDoctorIDs?specializationCode=CAR`.

## Return codes worth remembering

`usp_AddSurgeryDetails` returns `1` and the new surgery id on success. Otherwise the OUT id is `0`:

| Code | Meaning |
| --- | --- |
| -1 | Doctor does not exist |
| -2 | Surgery category is not a specialization code |
| -3 | That doctor does not have that specialization |
| -4 | Start time is not less than end time |
| -5 | Doctor is already booked for an overlapping time that day |
| -6 | Surgery date is before today |
| -99 | Exception inside the procedure |

The DAL catch for `AddSurgeryDetails` returns `-98` and sets the OUT id to `0`. The Web API catch for that action returns `-95`. `UpdateSurgeryTime` returns `1`, `-1` when the end time is not greater than the start time, and `-98` when the surgery id does not exist or an exception is thrown.

The printed success row uses `2025-05-09`. That date is before today, so the procedure correctly returns `-6`. A booking on a future date returns `1` and surgery id `5002` on a fresh database.
