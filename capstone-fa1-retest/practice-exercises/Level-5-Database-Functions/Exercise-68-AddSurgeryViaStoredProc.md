# Exercise 68: AddSurgeryViaStoredProc

## Business Scenario
Booking a surgery requires enforcing multi-tier integrity rules via the database stored procedure `usp_AddSurgeryDetails`. The procedure populates an `OUTPUT` parameter with the newly assigned `SurgeryID` and returns an execution status code.

## Problem Statement
Write a repository method `AddSurgeryViaStoredProc` with signature:
```csharp
public int AddSurgeryViaStoredProc(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
```
Implementation requirements:
1. Initialize `surgeryId = 0`.
2. Configure `SqlParameter` objects for `@did`, `@sc`, `@sd`, `@st`, `@et`.
3. Configure `SqlParameter` for `@si` (SqlDbType.Int) with `Direction = ParameterDirection.Output`.
4. Configure `SqlParameter` for return code `@r` (SqlDbType.Int) with `Direction = ParameterDirection.ReturnValue`.
5. Execute stored procedure using `context.Database.ExecuteSqlRaw("EXEC @r = usp_AddSurgeryDetails @did, @sc, @sd, @st, @et, @si OUT", ...)` with all parameters.
6. Retrieve return code from `@r.Value` and surgery ID from `@si.Value`.
7. Set `surgeryId = Convert.ToInt32(siParam.Value)` and return the procedure return code.
8. On exception, set `surgeryId = 0` and return `-98`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryViaStoredProc(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` accepting `Models.Surgery` body object. Call the repository method passing individual properties and receiving `out int surgeryId`. If return code == 1, return `JsonResult` with `surgeryId`. Otherwise, return `JsonResult` with the error code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryViaStoredProc(Models.Surgery surgery)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryCategory | string | Specialization code |
| surgeryDate | DateTime | Surgery date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |
| surgeryId | out int | Populated with generated ID |

## Relevant Database Tables
- Stored Procedure: `usp_AddSurgeryDetails`

## Entity Relationships
- Enforces FK constraints, overlapping check, past date check.

## Required Operations
1. Initialize `out` parameter to 0.
2. Build input, output, and return-value `SqlParameter`s.
3. Call `ExecuteSqlRaw`.
4. Extract values from parameters.
5. Return procedure status code.

## Business Rules and Validations
- Return codes:
  - `-1`: Doctor not found
  - `-2`: Specialization not found
  - `-3`: Doctor specialization mismatch
  - `-4`: Start time >= End time
  - `-5`: Overlapping surgery booking
  - `-6`: Past surgery date
  - `1`: Success (surgeryId populated)
  - `-98`: DAL exception

## Required IF-ELSE Conditions
- Handled internally by stored procedure logic and error code checking.

## Expected Return Values
| Scenario | Return Code | Output surgeryId |
|----------|-------------|------------------|
| Success | `1` | Generated SurgeryID (>= 5000) |
| Validation failure | `-1` to `-6` | `0` |
| Exception | `-98` | `0` |

## Exception Handling Requirements
Try-catch block resetting `surgeryId = 0` and returning `-98`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
