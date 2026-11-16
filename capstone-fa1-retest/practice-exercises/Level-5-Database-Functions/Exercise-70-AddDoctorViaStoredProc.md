# Exercise 70: AddDoctorViaStoredProc

## Business Scenario
Doctor registration requires atomically creating a specialization (if not present), creating the doctor record, and creating their `DoctorSpecialization` entry via stored procedure `usp_AddDoctorDetails`.

## Problem Statement
Write a repository method `AddDoctorViaStoredProc` accepting `doctorName`, `specializationCode`, and `specializationName`.
Call `usp_AddDoctorDetails` using ADO.NET or `ExecuteSqlRaw`:
```sql
DECLARE @r INT;
EXEC @r = usp_AddDoctorDetails @DoctorName, @SpecializationCode, @SpecializationName;
SELECT @r;
```
The procedure returns `1` on success and `-99` on exception.
Return `int`. Return `-99` on catch.

## DAL / Repository Method Signature
```csharp
public int AddDoctorViaStoredProc(string doctorName, string specializationCode, string specializationName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the integer code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorViaStoredProc(string doctorName, string specializationCode, string specializationName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Doctor name |
| specializationCode | string | 3-char code |
| specializationName | string | Specialization title |

## Relevant Database Tables
- Stored Procedure: `usp_AddDoctorDetails`
- Modifies: `Doctor`, `Specialization`, `DoctorSpecialization`

## Entity Relationships
- Multi-table insertion managed in SQL.

## Required Operations
1. Setup SQL command for `usp_AddDoctorDetails`.
2. Pass input parameters.
3. Execute and retrieve return code.
4. Return integer.

## Business Rules and Validations
1. Return 1 on success.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
