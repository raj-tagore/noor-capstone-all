# CureHospital — Infosys .NET Practical Examination (Practice Paper)

---

## General Instructions

- **Total Marks:** 50 Marks (Data Access Layer: 30 Marks, Service Layer: 20 Marks)
- **Time Allocated:** 3 Hours
- **Provided Solution Structure:**
  - **`CureHospitalDALCrossPlatform`**: Data Access Layer implemented using **Entity Framework Core (Database-First)**.
  - **`CureHospitalWebService`**: Service Layer implemented using **ASP.NET Core Web API**.
  - **`CureHospitalTest`**: Console application for validating DAL methods against test vectors.
- **Connection String:** Saved under the name `'DBConnectionString'` in `appsettings.json` / `EduAppsettings.json`.
- **LINQ Requirement:** All queries within repository methods must use **LINQ Query Syntax** (`from ... where ... select`). Method syntax is strictly prohibited.
- **Exception Handling:** Implement structured exception handling (`try-catch`) across all DAL repository methods and Web API controller actions.
- Do not modify existing model classes, DbContext configurations, or database tables unless explicitly requested.

---

## DATABASE SPECIFICATIONS

### Database Design

```
Doctor (1) ─────────────< (M) DoctorSpecialization (M) >───────────── (1) Specialization
   │                                                                           │
   │ (1)                                                                       │ (1)
   ▼                                                                           ▼
Surgery (M) ───────────────────────────────────────────────────────────────────┘
```

### Table 1: Specialization
Contains department classifications and clinical disciplines.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `SpecializationCode` | CHAR (3) | Primary Key | 3-character unique specialty code |
| `SpecializationName` | VARCHAR (50) | NOT NULL, UNIQUE | Full name of the clinical specialty |

**Sample Data:**
| SpecializationCode | SpecializationName |
| :--- | :--- |
| GYN | Gynecologist |
| CAR | Cardiologist |
| ANE | Anesthesiologist |

---

### Table 2: Doctor
Contains registry details of registered hospital surgeons.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `DoctorID` | INT | Primary Key, IDENTITY(1001, 1) | Unique doctor identifier auto-incremented by 1 |
| `DoctorName` | VARCHAR (50) | NOT NULL, UNIQUE | Full name of the medical practitioner |

**Sample Data:**
| DoctorID | DoctorName |
| :--- | :--- |
| 1001 | Albert |
| 1002 | Olivia |
| 1003 | Susan |

---

### Table 3: DoctorSpecialization
Junction table mapping doctor credentials to clinical specializations.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `DoctorID` | INT | FK $\rightarrow$ Doctor(DoctorID), NOT NULL | Existing Doctor ID |
| `SpecializationCode` | CHAR (3) | FK $\rightarrow$ Specialization(SpecializationCode), NOT NULL | Existing Specialization Code |
| `SpecializationDate` | DATE | NOT NULL | Date qualification was conferred |

**Note:** The combination of `(DoctorID, SpecializationCode)` forms the composite Primary Key.

**Sample Data:**
| DoctorID | SpecializationCode | SpecializationDate |
| :--- | :--- | :--- |
| 1001 | ANE | 2010-01-01 |
| 1002 | CAR | 2016-10-30 |
| 1003 | CAR | 2013-05-19 |

---

### Table 4: Surgery
Contains operating theater scheduling details for surgical operations.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| `SurgeryID` | INT | Primary Key, IDENTITY(5000, 1) | Unique surgery identifier auto-incremented by 1 |
| `DoctorID` | INT | FK $\rightarrow$ Doctor(DoctorID), NOT NULL | Assigned lead surgeon |
| `SurgeryCategory` | CHAR (3) | FK $\rightarrow$ Specialization(SpecializationCode), NOT NULL | Specialization category |
| `SurgeryDate` | DATE | NOT NULL | Date of surgical procedure |
| `StartTime` | DECIMAL (4, 2) | NOT NULL | Start time in 24-hr `HH.MM` format (0.00 – 23.00) |
| `EndTime` | DECIMAL (4, 2) | NOT NULL | End time in 24-hr `HH.MM` format (0.00 – 23.00) |

**Sample Data:**
| SurgeryID | DoctorID | SurgeryCategory | SurgeryDate | StartTime | EndTime |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 5000 | 1001 | ANE | Current Date + 2 Days | 09.00 | 14.00 |
| 5001 | 1002 | CAR | Current Date + 5 Days | 10.00 | 16.00 |

---

### Database Programmability Objects

#### Stored Procedure: `usp_AddSurgeryDetails`
Inserts a new surgery record after conducting database-level validations.
- **Input Parameters:**
  - `@did INT` — Doctor ID
  - `@sc CHAR(3)` — Surgery Category / Specialization Code
  - `@sd DATE` — Surgery Date
  - `@st DECIMAL(4, 2)` — Start Time
  - `@et DECIMAL(4, 2)` — End Time
- **Output Parameters:**
  - `@si INT OUTPUT` — Populated with the newly generated `SurgeryID` on success; set to `0` on any validation failure.
- **Validation Rules & Stored Procedure Return Codes:**
  - `1`: Successful insertion (sets `@si` to generated `SurgeryID`)
  - `-1`: `DoctorID` does not exist in `Doctor` table
  - `-2`: `SurgeryCategory` does not exist in `Specialization` table
  - `-3`: Combination of `(DoctorID, SurgeryCategory)` does not exist in `DoctorSpecialization`
  - `-4`: `StartTime >= EndTime` (start time must be strictly before end time)
  - `-5`: Doctor is unavailable (overlapping surgery exists on that date)
  - `-6`: `SurgeryDate` is strictly less than current date (`GETDATE()`)
  - `-99`: Any internal SQL exception

#### Stored Procedure: `usp_AddDoctorDetails`
Atomically inserts a new specialization (if not present), adds the doctor profile, and adds their credential link.
- **Input Parameters:** `@DoctorName VARCHAR(50)`, `@SpecializationCode CHAR(3)`, `@SpecializationName VARCHAR(50)`
- **Return Values:** `1` on success, `-99` on exception.

#### Table-Valued Function: `ufn_FetchSurgeryDetails`
Accepts `@SurgeryDate DATE` and returns a table:
`SurgeryID`, `DoctorName`, `SpecializationName`, `SurgeryDate`, `StartTime`, `EndTime`.
Mapped to the keyless EF Core model `SurgeryDetail`.

#### Scalar Function: `ufn_FetchSurgeryCount`
Accepts `@SpecializationName VARCHAR(50)` and `@SurgeryDate DATE`.
Returns `INT` representing total surgeries conducted in that category on that date.

---

## SECTION A: DATA ACCESS LAYER (DAL) — [30 Marks]

Implement the following repository methods in `CureHospitalRepositoryEF.cs` located in project `CureHospitalDALCrossPlatform`.

### DAL Method Specifications

| Sl. No | Method Name | Method Signature | Description | Return Values |
| :---: | :--- | :--- | :--- | :--- |
| **1** | **FetchDoctorIDs** | `public List<int> FetchDoctorIDs(string specializationCode)` | Fetch all `DoctorID` values from `DoctorSpecialization` matching `specializationCode` using LINQ Query Syntax. | • Matching `List<int>`<br>• `null` in case of exception |
| **2** | **AddDoctorSpecialization** | `public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)` | Add a new record to `DoctorSpecialization` table and save changes. | • `true` on successful execution<br>• `false` in case of failure or exception |
| **3** | **UpdateSurgeryTime** | `public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)` | Locate surgery by `surgeryID` using `Find()`. If `newEndTime <= newStartTime`, do not update and return `-1`. Otherwise, update start and end time and save. | • `1` on successful execution<br>• `-1` if `newEndTime <= newStartTime`<br>• `-98` if surgery not found or on exception |
| **4** | **RemoveSurgeryDetails** | `public bool? RemoveSurgeryDetails(DateTime surgeryDate)` | Delete all surgeries from `Surgery` whose `SurgeryDate` matches the provided date using `RemoveRange()`. | • `true` on successful deletion<br>• `false` if no surgeries found or on exception |
| **5** | **AddSurgeryDetails** | `public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)` | Invoke stored procedure `usp_AddSurgeryDetails` using `ExecuteSqlRaw()`. Capture OUT parameter `@si` into `surgeryID` and stored procedure return value `@r`. | • Stored procedure return code (`1`, `-1`, `-2`, `-3`, `-4`, `-5`, `-6`)<br>• On exception: return `-98` and set `surgeryID = 0` |
| **6** | **GetSurgeryDetails** | `public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)` | Fetch surgery records by invoking table-valued function `ufn_FetchSurgeryDetails` using `FromSqlInterpolated()`. | • Matching `List<SurgeryDetail>`<br>• `null` in case of exception |
| **7** | **AddDoctorWithDuplicateCheck** | `public bool AddDoctorWithDuplicateCheck(string doctorName)` | Check if doctor with same name exists via LINQ Query Syntax. If unique, create `Doctor`, save, and return `true`. | • `false` if duplicate exists<br>• `true` on successful insertion<br>• `false` on exception |
| **8** | **AddDoctorSpecializationWithChecks** | `public int AddDoctorSpecializationWithChecks(int doctorId, string specializationCode, DateTime specializationDate)` | Validate: 1) `doctorId` in `Doctor`, 2) `specializationCode` in `Specialization`, 3) combination not in `DoctorSpecialization`. | • `1` on success<br>• `-1` if doctor not found<br>• `-2` if specialization not found<br>• `-3` if already assigned<br>• `-99` on exception |
| **9** | **ReassignSurgeryToNewDoctor** | `public int ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)` | Query 1: Find surgery by `surgeryId`. Query 2: Find new doctor by `newDoctorId`. If both exist, update `surgery.DoctorId` and save. | • `1` on success<br>• `-1` if surgery not found<br>• `-2` if new doctor not found<br>• `-99` on exception |
| **10** | **GetDoctorSurgeryInfo** | `public List<DoctorSurgeryInfo> GetDoctorSurgeryInfo(int doctorId)` | Perform two-table join between `Surgery` and `Doctor` in pure LINQ Query Syntax. Project into DTO `DoctorSurgeryInfo`. | • `List<DoctorSurgeryInfo>`<br>• `null` in case of exception |

---

### Official DAL Test Cases Table

Use the following inputs for testing DAL methods against return values and OUT parameters:

#### Test Cases for `AddSurgeryDetails`:
| doctorID | surgeryCategory | surgeryDate | startTime | endTime | Return Code | OUT `surgeryID` | Expected Behavior |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| `1003` | `"CAR"` | `2025-05-09` | `10.00` | `12.00` | **`1`** | **`5002`** | Successful insertion |
| `1005` | `"CAR"` | `2025-05-09` | `10.00` | `12.00` | **`-1`** | **`0`** | DoctorID does not exist |
| `1003` | `"ENT"` | `2025-05-09` | `10.00` | `12.00` | **`-2`** | **`0`** | Category does not exist |
| `1001` | `"CAR"` | `2025-05-09` | `10.00` | `12.00` | **`-3`** | **`0`** | Doctor lacks specialization |
| `1003` | `"CAR"` | `2025-05-09` | `12.00` | `10.00` | **`-4`** | **`0`** | StartTime $\ge$ EndTime |
| `1003` | `null` | `2025-05-09` | `10.00` | `12.00` | **`-98`** | **`0`** | Exception caught in DAL |

#### Test Cases for `UpdateSurgeryTime`:
| surgeryID | newStartTime | newEndTime | Expected Return Value | Expected Behavior |
| :---: | :---: | :---: | :---: | :--- |
| `5000` | `11.00` | `15.00` | **`1`** | Successful update |
| `5000` | `15.00` | `11.00` | **`-1`** | EndTime not greater than StartTime |
| `5007` | `11.00` | `15.00` | **`-98`** | Surgery ID does not exist |

#### Test Cases for `RemoveSurgeryDetails`:
| surgeryDate | Expected Return Value | Expected Behavior |
| :---: | :---: | :--- |
| Existing Date with surgeries | **`true`** | Surgeries deleted successfully |
| Date with no surgeries | **`false`** | No records found |

---

## SECTION B: SERVICE LAYER (ASP.NET WEB API) — [20 Marks]

Implement the controller action methods in `CureHospitalController.cs` in project `CureHospitalWebService`.
Base Route: `api/[controller]/[action]`

### Model Validation Rules

#### 1. Model: `DoctorSpecialization`
- `DoctorId`: `[Required]`, `[Range(1001, int.MaxValue)]`
- `SpecializationCode`: `[Required]`, `[StringLength(3, MinimumLength = 3)]`
- `SpecializationDate`: `[Required]`

#### 2. Model: `Surgery`
- `DoctorId`: `[Required]`
- `SurgeryCategory`: `[Required]`, `[StringLength(3, MinimumLength = 3)]`
- `SurgeryDate`: `[Required]`
- `StartTime`: `[Required]`, `[Range(typeof(decimal), "0.00", "23.00")]`
- `EndTime`: `[Required]`, `[Range(typeof(decimal), "0.00", "23.00")]`

---

### Web API Action Specifications

| Sl. No | HTTP Verb | Action Signature | Description | Return Values | Marks |
| :---: | :---: | :--- | :--- | :--- | :---: |
| **1** | `[HttpGet]` | `FetchDoctorIDs(string specializationCode)` | Invoke repository method `FetchDoctorIDs`. | • JSON array of doctor IDs<br>• `null` on exception | **1.5** |
| **2** | `[HttpPost]` | `AddDoctorSpecialization(DoctorSpecialization doctorSpecialization)` | Invoke repository method `AddDoctorSpecialization`. | • `true` / `false`<br>• `null` on exception | **1.5** |
| **3** | `[HttpPut]` | `UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)` | Invoke repository method `UpdateSurgeryTime`. | • Value returned by DAL (`1` or `-1`)<br>• **`-98`** on exception | **1.5** |
| **4** | `[HttpDelete]` | `RemoveSurgeryDetails(DateTime surgeryDate)` | Invoke repository method `RemoveSurgeryDetails`. | • `true` / `false`<br>• `null` on exception | **1.5** |
| **5** | `[HttpPost]` | `AddSurgeryDetails(Surgery surgery)` | Invoke repository method `AddSurgeryDetails` with `out int surgeryId`:<br>• If DAL returns **`1`**, return **`surgeryId`**<br>• Otherwise, return the **DAL return code** | • `surgeryId` (on success)<br>• Error code (`-1` to `-6`)<br>• **`-95`** on exception | **3.0** |
| **6** | `[HttpGet]` | `GetSurgeryDetails(DateTime surgeryDate)` | Invoke repository method `GetSurgeryDetails`. | • JSON array of `SurgeryDetail`<br>• `null` on exception | **1.0** |
| **7** | `[HttpPost]` | `AddDoctorWithDuplicateCheck(string doctorName)` | Invoke repository method `AddDoctorWithDuplicateCheck`. | • JSON boolean (`true` / `false`) | **1.5** |
| **8** | `[HttpPost]` | `AddDoctorSpecializationWithChecks(int doctorId, string specializationCode, DateTime specializationDate)` | Invoke repository method `AddDoctorSpecializationWithChecks`. | • JSON status code integer (`1`, `-1`, `-2`, `-3`, `-99`) | **1.5** |
| **9** | `[HttpPut]` | `ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)` | Invoke repository method `ReassignSurgeryToNewDoctor`. | • JSON status code integer (`1`, `-1`, `-2`, `-99`) | **1.5** |
| **10** | `[HttpGet]` | `GetDoctorSurgeryInfo(int doctorId)` | Invoke repository method `GetDoctorSurgeryInfo`. | • JSON array of `DoctorSurgeryInfo`<br>• `null` on exception | **1.5** |

---

### Official Web API Test Cases Table

Use the following test inputs and verify the JSON response from your API endpoints:

#### Action: `AddSurgeryDetails` (`POST /api/CureHospital/AddSurgeryDetails`)
| Request Body (JSON) | Expected Response in JSON | Reason |
| :--- | :---: | :--- |
| `{"DoctorId": 1003, "SurgeryCategory": "CAR", "SurgeryDate": "2025-05-09", "StartTime": 10.00, "EndTime": 12.00}` | `5002` | Insertion successful, returns generated SurgeryID |
| `{"DoctorId": 1005, "SurgeryCategory": "CAR", "SurgeryDate": "2025-05-09", "StartTime": 10.00, "EndTime": 12.00}` | `-1` | Doctor 1005 does not exist |
| `{"DoctorId": 1003, "SurgeryCategory": "ENT", "SurgeryDate": "2025-05-09", "StartTime": 10.00, "EndTime": 12.00}` | `-2` | Specialization ENT does not exist |
| `{"DoctorId": 1001, "SurgeryCategory": "CAR", "SurgeryDate": "2025-05-09", "StartTime": 10.00, "EndTime": 12.00}` | `-3` | Specialization mismatch |
| `{"DoctorId": 1003, "SurgeryCategory": "CAR", "SurgeryDate": "2025-05-09", "StartTime": 12.00, "EndTime": 10.00}` | `-4` | StartTime $\ge$ EndTime |

#### Action: `UpdateSurgeryTime` (`PUT /api/CureHospital/UpdateSurgeryTime?surgeryId=5000&startTime=11&endTime=15`)
| Query Parameters | Expected Response in JSON | Reason |
| :--- | :---: | :--- |
| `surgeryId=5000, startTime=11.00, endTime=15.00` | `1` | Successful update |
| `surgeryId=5000, startTime=15.00, endTime=11.00` | `-1` | EndTime not greater than StartTime |
| `surgeryId=5007, startTime=11.00, endTime=15.00` | `-98` | Surgery 5007 does not exist |
