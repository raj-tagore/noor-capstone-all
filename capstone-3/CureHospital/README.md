# CureHospital

Surgery scheduling app from the capstone-3 problem statement.

| Piece | Folder | Address |
|---|---|---|
| Database | `CureHospitalDB.sql` | `(localdb)\MSSQLLocalDB`, database `CureHospitalDB` |
| Data access | `CureHospitalDALCrossPlatform` | connection name `DBConnectionString` |
| API | `CureHospitalWebService` | http://localhost:5110/swagger |
| Tester | `CureHospitalTest` | calls `AddSurgeryDetails` with the sample inputs |
| Angular | `CureWellApp` | http://localhost:4200 |

The SQL script is already applied. Running it again drops and recreates `CureHospitalDB`.

`node_modules`, `.angular`, `dist`, `bin`, and `obj` are not in git. Recreate them from this folder with the .NET 8 SDK and Node.js 18:

```powershell
dotnet restore CureHospital.sln
dotnet build CureHospital.sln
cd CureWellApp
npm install
```

`dotnet build` writes `bin` and `obj` for the data access, API, and tester projects and downloads their NuGet packages. `npm install` writes `CureWellApp/node_modules` from `package-lock.json`. `npm start` writes `.angular`, and `npm run build` writes `dist`.

```powershell
dotnet run --project CureHospitalWebService --launch-profile http
cd CureWellApp
npm start
```

Sample surgeries are today+2 (Albert, ANE, 9.00–14.00) and today+5 (Olivia, CAR, 10.00–16.00). `CureHospitalTest` expects the next surgery id to be 5002.
