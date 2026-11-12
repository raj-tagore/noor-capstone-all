# CureHospital — FA1 Retest

Recreated from the exam screenshots in `ps-pics`. This is the problem the candidate is asked to implement. It is not the solution.

The work is a SQL Server database, an Entity Framework Core data access layer, and an ASP.NET Core Web API. The screenshots do not include an Angular application.

Do not modify the given method signatures. Changing a signature reduces the score even when the code runs.

---

## Solution layout

The provided solution contains:

- **CureHospitalDALCrossPlatform**, implemented with Entity Framework Core. The Models folder is already available, with the entities and the context class for the given database.
- **CureHospitalTest**, used to test the data access methods. Set this project as the startup project to execute those tests. `Program.cs` contains one sample value. The code must work for every input, not only that sample.
- **CureHospitalWebService**, implemented with ASP.NET Core Web API. Set this project as the startup project when testing the service.

The SQL script is `CureHospitalDB.sql` inside **CureHospitalDALCrossPlatform**. Execute it in SQL Server. The project also contains `EduAppsettings.json`.

The connection string is stored under the name `DBConnectionString`.

---

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

---

## Stored procedure: usp_AddSurgeryDetails

Inserts the surgery details into **Surgery** and returns the newly generated `SurgeryID` as an OUTPUT parameter.

### Input parameters

- `DoctorID` INT
- `SurgeryCategory` CHAR(3)
- `SurgeryDate` DATE
- `StartTime` DECIMAL(4, 2)
- `EndTime` DECIMAL(4, 2)

### Output parameter

- `SurgeryID` INT

### Functionality

- Check that `DoctorID` exists in **Doctor**.
- Check that `SurgeryCategory` exists in **Specialization** as `SpecializationCode`.
- Check that the combination of `DoctorID` and `SurgeryCategory` exists in **DoctorSpecialization**.
- Check that the start time is less than the end time.
- Check that the doctor is available between the start time and the end time on the given surgery date.
- Check whether the surgery date is less than the present date.
- When every validation succeeds, insert the row into **Surgery** and set the OUTPUT parameter to the newly generated `SurgeryID`.
- In every other case, set the OUTPUT parameter to 0.

### Return values

- `1`, successful insertion
- `-1`, `DoctorID` does not exist in **Doctor**
- `-2`, `SurgeryCategory` does not exist in **Specialization** as `SpecializationCode`
- `-3`, the combination of `DoctorID` and `SurgeryCategory` does not exist in **DoctorSpecialization**
- `-4`, if the start time is less than the end time
- `-5`, the doctor is not available for the given date and time period
- `-6`, the surgery date is less than the present date
- `-99`, any exception

The published test table treats start time `12` and end time `10` as return code `-4`.

---

## Stored procedure: usp_AddDoctorDetails

Inserts a doctor and that doctor’s specialization.

### Input parameters

- `DoctorName` VARCHAR(50)
- `SpecializationCode` CHAR(3)
- `SpecializationName` VARCHAR(50)

### Functionality

- Check whether `SpecializationCode` exists in **Specialization**.
  - When it does not exist, insert `SpecializationCode` and `SpecializationName` into **Specialization**.
- Insert the doctor into **Doctor**.
- Insert the newly generated `DoctorID` and the given `SpecializationCode` into **DoctorSpecialization**, with `SpecializationDate` set to the current date.

### Return values

- `1`, successful insertion
- `-99`, any exception

---

## Function: ufn_FetchSurgeryDetails

A table-valued function. It fetches the surgeries for a given surgery date and returns a table.

The columns visible on the slide are:

| SurgeryID | DoctorName | SpecializationName | SurgeryDate | StartTime |
| --- | --- | --- | --- | --- |

The slide is cut off after `StartTime`.

---

## Function: ufn_FetchSurgeryCount

A scalar function. It returns the number of surgeries for a given specialization and date.

### Input parameters

- `SpecializationName` VARCHAR(50)
- `SurgeryDate` DATE

### Functionality

Fetch the number of surgeries in **Surgery** for the given specialization on the given date.

### Return value

- `Count` INT

---

## Data access layer

**30 marks.**

Implement **CureHospitalDALCrossPlatform** with Entity Framework Core. Implement structured exception handling in every method. **2 marks.**

Class: `CureHospitalRepositoryEF`. Context: the supplied `CureHospitalDbContext`. Each method is inside a region marked **Do not modify the signature**.

### 1. FetchDoctorIDs

```csharp
public List<int> FetchDoctorIDs(string specializationCode)
```

Fetch every `DoctorID` from **DoctorSpecialization** whose `SpecializationCode` matches the given code.

- Return `List<int>`.
- On an exception, return `null`.

### 2. AddDoctorSpecialization

```csharp
public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)
```

Add the given details to **DoctorSpecialization**.

- `true` when the insert succeeds.
- `false` when it fails or an exception occurs.

### 3. UpdateSurgeryTime

```csharp
public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)
```

Update `StartTime` and `EndTime` in **Surgery** for the given `surgeryID`.

- `1` when the update succeeds.
- `-1` when `EndTime` is not greater than `StartTime`.
- `-98` when an exception occurs.

Test inputs:

| surgeryID | newStartTime | newEndTime | Return value |
| --- | --- | --- | --- |
| 5000 | 11 | 15 | 1 |
| 5000 | 15 | 11 | -1 |
| 5007 | 11 | 15 | -98 |

### 4. RemoveSurgeryDetails

```csharp
public bool? RemoveSurgeryDetails(DateTime surgeryDate)
```

Delete every surgery for the given `surgeryDate` from **Surgery**.

- `true` when the delete succeeds.
- `false` when an exception occurs, or when no surgery exists for that date.

### 5. AddSurgeryDetails

```csharp
public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)
```

Add a surgery by calling `usp_AddSurgeryDetails`.

- Return the value returned by the stored procedure.
- The OUTPUT parameter from the procedure is the `surgeryID` out argument.
- On an exception, the return value is `-98` and the out parameter is `0`.

Test inputs:

| doctorID | surgeryCategory | surgeryDate | startTime | endTime | Return value | OUT parameter |
| --- | --- | --- | --- | --- | --- | --- |
| 1003 | CAR | 2025-05-09 | 10 | 12 | 1 | 5002 |
| 1005 | CAR | 2025-05-09 | 10 | 12 | -1 | 0 |
| 1003 | ENT | 2025-05-09 | 10 | 12 | -2 | 0 |
| 1001 | CAR | 2025-05-09 | 10 | 12 | -3 | 0 |
| 1003 | CAR | 2025-05-09 | 12 | 10 | -4 | 0 |
| 1003 | null | 2025-05-09 | 10 | 12 | -98 | 0 |

### 6. GetSurgeryDetails

```csharp
public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)
```

Fetch the surgeries for the given `surgeryDate` by calling `ufn_FetchSurgeryDetails`. Use the `SurgeryDetail` class in the Models folder.

- Return `List<SurgeryDetail>` for the rows whose surgery date matches.
- On an exception, return `null`.

---

## Service layer

Implement the actions with ASP.NET Core Web API. Implement exception handling in every action. **20 marks.**

Map every action to the HTTP verb that matches the operation. **3 marks.**

The web service contains the `DoctorSpecialization` and `Surgery` models shown below. **3 marks.**

Set **CureHospitalWebService** as the startup project.

Controller: `CureHospitalController`. Each action is inside a region marked **Do not modify the signature**.

### DoctorSpecialization

```csharp
public class DoctorSpecialization
{
    public int DoctorId { get; set; }
    public string SpecializationCode { get; set; }
    public DateTime SpecializationDate { get; set; }
}
```

Validations:

- Every field is mandatory.
- `DoctorId` accepts values from 1001 through the maximum value of `int`.
- `SpecializationCode` is exactly three characters.

### Surgery

Validations:

- Every field except `SurgeryId` is mandatory.
- `StartTime` and `EndTime` accept decimal values from 0.00 through 23.00.

These validations are worth **4 marks.**

### 1. FetchDoctorIDs

```csharp
[HttpGet]
public JsonResult FetchDoctorIDs(string specializationCode)
```

Fetch the doctor ids for the specialization code by calling `CureHospitalRepositoryEF.FetchDoctorIDs`.

- Return the data access result as JSON.
- On an exception, return `null`.

**1.5 marks.**

### 2. AddDoctorSpecialization

```csharp
[HttpPost]
public bool? AddDoctorSpecialization(DoctorSpecialization doctorSpecialization)
```

Add the doctor specialization by calling `CureHospitalRepositoryEF.AddDoctorSpecialization`.

- Return the value returned by the data access method.
- On an exception, return `null`.

**1.5 marks.**

### 3. UpdateSurgeryTime

```csharp
[HttpPut]
public JsonResult UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)
```

Update the start time and end time by calling `CureHospitalRepositoryEF.UpdateSurgeryTime`.

- Return the data access result as JSON.
- On an exception, return `-98`.

**1.5 marks.**

### 4. RemoveSurgeryDetails

```csharp
[HttpDelete]
public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)
```

Delete the surgeries for the given date by calling `CureHospitalRepositoryEF.RemoveSurgeryDetails`.

- Return the data access result as JSON.
- On an exception, return `null` as a `JsonResult`.

**1.5 marks.**

### 5. AddSurgeryDetails

```csharp
[HttpPost]
public JsonResult AddSurgeryDetails(Models.Surgery surgery)
```

Add the surgery by calling `CureHospitalRepositoryEF.AddSurgeryDetails`.

- When the data access method returns `1`, return the `surgeryId` as JSON.
- Otherwise, return the value returned by the data access method as JSON.
- On an exception, return `-95`.

**3 marks.**

Web API test inputs:

| Model input | JSON result |
| --- | --- |
| DoctorId = 1003, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | 5002 |
| DoctorId = 1005, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -1 |
| DoctorId = 1003, SurgeryCategory = ENT, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -2 |
| DoctorId = 1001, SurgeryCategory = CAR, SurgeryDate = 2025-05-09, StartTime = 10, EndTime = 12 | -3 |

### 6. GetSurgeryDetails

```csharp
[HttpGet]
public JsonResult GetSurgeryDetails(DateTime surgeryDate)
```

Get the surgery details for the date by calling `CureHospitalRepositoryEF.GetSurgeryDetails`.

- Return the data access result as JSON.
- On an exception, return `null`.

**1 mark.**
