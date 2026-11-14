# CureHospital — FA1 Retest

Recreated from the exam screenshots in `ps-pics`. This is the problem the candidate is asked to implement. It is not the solution.

The work is a SQL Server database, an Entity Framework Core data access layer, and an ASP.NET Core Web API. The screenshots do not include an Angular application.

Do not modify the given method signatures. Changing a signature reduces the score even when the code runs.

---

# Given

Material already supplied. Execute the scripts and use the projects and models as they are. Do not change the method signatures.

## Solution layout

The provided solution contains:

- **CureHospitalDALCrossPlatform**, implemented with Entity Framework Core. The Models folder is already available, with the entities and the context class for the given database.
- **CureHospitalTest**, used to test the data access methods. Set this project as the startup project to execute those tests. `Program.cs` contains one sample value. The code must work for every input, not only that sample.
- **CureHospitalWebService**, implemented with ASP.NET Core Web API. Set this project as the startup project when testing the service.

The SQL script is `CureHospitalDB.sql` inside **CureHospitalDALCrossPlatform**. Execute it in SQL Server. The project also contains `EduAppsettings.json`.

The connection string is stored under the name `DBConnectionString`.

## Database

Execute the provided database scripts in SQL Server. The database objects are described below.

### Diagram

- **Doctor** (`DoctorID`, `DoctorName`) is referenced by **DoctorSpecialization** and by **Surgery**.
- **Specialization** (`SpecializationCode`, `SpecializationName`) is referenced by **DoctorSpecialization** and by **Surgery**.
- **DoctorSpecialization** contains `DoctorID`, `SpecializationCode`, and `SpecializationDate`.
- **Surgery** contains `SurgeryID`, `DoctorID`, `SurgeryDate`, `StartTime`, `EndTime`, and `SurgeryCategory`.

### 1. Table: Specialization

Details of all specializations.

| Field | Data type | Description |
| --- | --- | --- |
| SpecializationCode | CHAR(3) | Primary key |
| SpecializationName | VARCHAR(50) | Name of the specialization. Mandatory. Unique. |

Sample data:

| SpecializationCode | SpecializationName |
| --- | --- |
| GYN | Gynecologist |
| CAR | Cardiologist |
| ANE | Anesthesiologist |

### 2. Table: Doctor

Details of doctors.

| Field | Data type | Description |
| --- | --- | --- |
| DoctorID | INT | Primary key. IDENTITY, starts at 1001 and increments by 1. |
| DoctorName | VARCHAR(50) | Name of the doctor. Mandatory. Unique. |

Sample data:

| DoctorID | DoctorName |
| --- | --- |
| 1001 | Albert |
| 1002 | Olivia |
| 1003 | Susan |

### 3. Table: DoctorSpecialization

Specialization details of doctors.

| Field | Data type | Description |
| --- | --- | --- |
| DoctorID | INT | An existing `DoctorID` in **Doctor**. Mandatory. |
| SpecializationCode | CHAR(3) | An existing `SpecializationCode` in **Specialization**. Mandatory. |
| SpecializationDate | DATE | Date on which the specialization is added. Mandatory. |

The primary key is the combination of `DoctorID` and `SpecializationCode`.

Sample data:

| DoctorID | SpecializationCode | SpecializationDate |
| --- | --- | --- |
| 1001 | ANE | 2010-01-01 |
| 1002 | CAR | 2016-10-30 |
| 1003 | CAR | 2013-05-19 |

### 4. Table: Surgery

Details of all surgeries.

| Field | Data type | Description |
| --- | --- | --- |
| SurgeryID | INT | Primary key. IDENTITY, starts at 5000 and increments by 1. |
| DoctorID | INT | An existing `DoctorID` in **Doctor**. Mandatory. |
| SurgeryCategory | CHAR(3) | An existing `SpecializationCode` in **Specialization**. Mandatory. |
| SurgeryDate | DATE | Date of the surgery. Mandatory. |
| StartTime | DECIMAL(4, 2) | Start time of the surgery. Mandatory. |
| EndTime | DECIMAL(4, 2) | End time of the surgery. Mandatory. |

`StartTime` and `EndTime` are in HH.MM 24-hour format.

Sample data:

| SurgeryID | DoctorID | SurgeryCategory | SurgeryDate | StartTime | EndTime |
| --- | --- | --- | --- | --- | --- |
| 5000 | 1001 | ANE | Current date + 2 days | 09.00 | 14.00 |
| 5001 | 1002 | CAR | Current date + 5 days | 10.00 | 16.00 |

## Web service models

The web service contains the `DoctorSpecialization` and `Surgery` models. **3 marks** are for these models as already shown.

```csharp
public class DoctorSpecialization
{
    public int DoctorId { get; set; }
    public string SpecializationCode { get; set; }
    public DateTime SpecializationDate { get; set; }
}
```

## Database objects

These are already in `CureHospitalDB.sql`. The slides say to execute that script. The C# methods call them. They are not something to write.

### Stored procedure: usp_AddSurgeryDetails

Inserts the surgery details into **Surgery** and returns the newly generated `SurgeryID` as an OUTPUT parameter.

**Input parameters**

| Parameter | Data type |
| --- | --- |
| DoctorID | INT |
| SurgeryCategory | CHAR(3) |
| SurgeryDate | DATE |
| StartTime | DECIMAL(4, 2) |
| EndTime | DECIMAL(4, 2) |

**Output parameter**

| Parameter | Data type |
| --- | --- |
| SurgeryID | INT |

**Functionality**

- Check that `DoctorID` exists in **Doctor**.
- Check that `SurgeryCategory` exists in **Specialization** as `SpecializationCode`.
- Check that the combination of `DoctorID` and `SurgeryCategory` exists in **DoctorSpecialization**.
- Check that the start time is less than the end time.
- Check that the doctor is available between the start time and the end time on the given surgery date.
- Check whether the surgery date is less than the present date.
- When every validation succeeds, insert the row into **Surgery** and set the OUTPUT parameter to the newly generated `SurgeryID`.
- In every other case, set the OUTPUT parameter to 0.

**Return values**

| Code | Meaning |
| --- | --- |
| 1 | Successful insertion |
| -1 | `DoctorID` does not exist in **Doctor** |
| -2 | `SurgeryCategory` does not exist in **Specialization** as `SpecializationCode` |
| -3 | The combination of `DoctorID` and `SurgeryCategory` does not exist in **DoctorSpecialization** |
| -4 | Start time is less than end time |
| -5 | The doctor is not available for the given date and time period |
| -6 | The surgery date is less than the present date |
| -99 | Any exception |

The published test table treats start time `12` and end time `10` as return code `-4`.

### Stored procedure: usp_AddDoctorDetails

Inserts a doctor and that doctor’s specialization.

**Input parameters**

| Parameter | Data type |
| --- | --- |
| DoctorName | VARCHAR(50) |
| SpecializationCode | CHAR(3) |
| SpecializationName | VARCHAR(50) |

**Functionality**

- Check whether `SpecializationCode` exists in **Specialization**.
  - When it does not exist, insert `SpecializationCode` and `SpecializationName` into **Specialization**.
- Insert the doctor into **Doctor**.
- Insert the newly generated `DoctorID` and the given `SpecializationCode` into **DoctorSpecialization**, with `SpecializationDate` set to the current date.

**Return values**

| Code | Meaning |
| --- | --- |
| 1 | Successful insertion |
| -99 | Any exception |

### Function: ufn_FetchSurgeryDetails

A table-valued function. It fetches the surgeries for a given surgery date and returns a table.

| SurgeryID | DoctorName | SpecializationName | SurgeryDate | StartTime |
| --- | --- | --- | --- | --- |

The slide is cut off after `StartTime`.

### Function: ufn_FetchSurgeryCount

A scalar function. It returns the number of surgeries for a given specialization and date.

**Input parameters**

| Parameter | Data type |
| --- | --- |
| SpecializationName | VARCHAR(50) |
| SurgeryDate | DATE |

Fetch the number of surgeries in **Surgery** for the given specialization on the given date.

**Return value**

| Name | Data type |
| --- | --- |
| Count | INT |

---

# Deliverables

Write the data access methods, validations, and Web API actions below. The method tables on the slides are these two sections.

## Data access layer

**30 marks.**

Implement **CureHospitalDALCrossPlatform** with Entity Framework Core. Implement structured exception handling in every method. **2 marks.**

Class: `CureHospitalRepositoryEF`. Context: the supplied `CureHospitalDbContext`. Each method is inside a region marked **Do not modify the signature**.

| Sl. No | Method name | Description | Return values |
| --- | --- | --- | --- |
| 1 | `FetchDoctorIDs` | Fetch all the DoctorID(s) from the DoctorSpecialization table whose specialization code matches the given specializationCode. `public List<int> FetchDoctorIDs(string specializationCode)` | `List<int>` whose SpecializationCode matches the given code. On an exception, return `null`. |
| 2 | `AddDoctorSpecialization` | Add the given details to the DoctorSpecialization table. `public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)` | `true` on successful execution. `false` on failure or an exception. |
| 3 | `UpdateSurgeryTime` | Update the surgery start time and end time for the given surgeryID in the Surgery table. `public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)` | `1` on successful execution. `-1` when EndTime is not greater than StartTime. `-98` on an exception. |
| 4 | `RemoveSurgeryDetails` | Delete all the surgery details for the given surgery date from the Surgery table. `public bool? RemoveSurgeryDetails(DateTime surgeryDate)` | `true` on successful execution. `false` on an exception, or when no surgery details are found. |
| 5 | `AddSurgeryDetails` | Add the new surgery details to the Surgery table by invoking `usp_AddSurgeryDetails`. `public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)` | Return the value returned by the stored procedure. The OUTPUT parameter is the `surgeryID` out argument. On an exception, the return value is `-98` and the out parameter is `0`. |
| 6 | `GetSurgeryDetails` | Fetch all surgery details by invoking `ufn_FetchSurgeryDetails` for the given SurgeryDate. Use the `SurgeryDetail` class in the Models folder. `public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)` | `List<SurgeryDetail>` whose SurgeryDate matches the given date. On an exception, return `null`. |

### Test inputs

**AddSurgeryDetails**

| doctorID | surgeryCategory | surgeryDate | startTime | endTime | Return value | OUT parameter |
| --- | --- | --- | --- | --- | --- | --- |
| 1003 | CAR | 2025-05-09 | 10 | 12 | 1 | 5002 |
| 1005 | CAR | 2025-05-09 | 10 | 12 | -1 | 0 |
| 1003 | ENT | 2025-05-09 | 10 | 12 | -2 | 0 |
| 1001 | CAR | 2025-05-09 | 10 | 12 | -3 | 0 |
| 1003 | CAR | 2025-05-09 | 12 | 10 | -4 | 0 |
| 1003 | null | 2025-05-09 | 10 | 12 | -98 | 0 |

**UpdateSurgeryTime**

| surgeryID | newStartTime | newEndTime | Return value |
| --- | --- | --- | --- |
| 5000 | 11 | 15 | 1 |
| 5000 | 15 | 11 | -1 |
| 5007 | 11 | 15 | -98 |

## Service layer

Implement the actions with ASP.NET Core Web API. **20 marks.**

| Item | Marks |
| --- | --- |
| Exception handling in every action | included in the 20 |
| Map every action to the HTTP verb that matches the operation | 3 |
| DoctorSpecialization and Surgery models, as shown above | 3 |
| Business validations below | 4 |

Set **CureHospitalWebService** as the startup project.

Controller: `CureHospitalController`. Each action is inside a region marked **Do not modify the signature**.

**DoctorSpecialization validations**

- Every field is mandatory.
- `DoctorId` accepts values from 1001 through the maximum value of `int`.
- `SpecializationCode` is exactly three characters.

**Surgery validations**

- Every field except `SurgeryId` is mandatory.
- `StartTime` and `EndTime` accept decimal values from 0.00 through 23.00.

| Sl. No | Method name | Description | Return values | Marks |
| --- | --- | --- | --- | --- |
| 1 | `FetchDoctorIDs` | Fetch DoctorIDs based on specialization code by invoking `CureHospitalRepositoryEF.FetchDoctorIDs`. `[HttpGet] public JsonResult FetchDoctorIDs(string specializationCode)` | Value returned by the data access method, as JSON. On an exception, return `null`. | 1.5 |
| 2 | `AddDoctorSpecialization` | Add doctor specialization details to the DoctorSpecialization table by invoking `CureHospitalRepositoryEF.AddDoctorSpecialization`. `[HttpPost] public bool? AddDoctorSpecialization(DoctorSpecialization doctorSpecialization)` | Value returned by the data access method. On an exception, return `null`. | 1.5 |
| 3 | `UpdateSurgeryTime` | Update StartTime and EndTime in the Surgery table for the provided surgeryId by invoking `CureHospitalRepositoryEF.UpdateSurgeryTime`. `[HttpPut] public JsonResult UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)` | Value returned by the data access method, as JSON. On an exception, return `-98`. | 1.5 |
| 4 | `RemoveSurgeryDetails` | Delete all the surgery details for the given surgeryDate from the Surgery table by invoking `CureHospitalRepositoryEF.RemoveSurgeryDetails`. `[HttpDelete] public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)` | Value returned by the data access method, as JSON. On an exception, return `null` as a `JsonResult`. | 1.5 |
| 5 | `AddSurgeryDetails` | Add surgery details to the Surgery table by invoking `CureHospitalRepositoryEF.AddSurgeryDetails`. `[HttpPost] public JsonResult AddSurgeryDetails(Models.Surgery surgery)` | When the data access method returns `1`, return the `surgeryId` as JSON. Otherwise, return the value returned by the data access method as JSON. On an exception, return `-95`. | 3 |
| 6 | `GetSurgeryDetails` | Get surgery details based on surgeryDate by invoking `CureHospitalRepositoryEF.GetSurgeryDetails`. `[HttpGet] public JsonResult GetSurgeryDetails(DateTime surgeryDate)` | Value returned by the data access method, as JSON. On an exception, return `null`. | 1 |

### Web API test inputs

**AddSurgeryDetails**

| Model input | Return value in JSON |
| --- | --- |
| DoctorId = 1003, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | 5002 |
| DoctorId = 1005, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -1 |
| DoctorId = 1003, SurgeryCategory = ENT, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -2 |
| DoctorId = 1001, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -3 |
