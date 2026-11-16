# Exercise 71: AddDoctorViaStoredProcWithPreCheck

## Business Scenario
Before executing the multi-table stored procedure `usp_AddDoctorDetails`, the system conducts an initial EF Core query check to ensure doctor names remain distinct.

## Problem Statement
Write a repository method `AddDoctorViaStoredProcWithPreCheck` taking `doctorName`, `specializationCode`, and `specializationName`.
1. Query `Doctor` table using LINQ Query Syntax:
   `from d in context.Doctors where d.DoctorName == doctorName select d`
   If `FirstOrDefault() != null`, return `-1` (doctor name already in use).
2. If not duplicate, call stored procedure `usp_AddDoctorDetails`.
3. Return procedure return code (`1`).
4. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddDoctorViaStoredProcWithPreCheck(string doctorName, string specializationCode, string specializationName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorViaStoredProcWithPreCheck(string doctorName, string specializationCode, string specializationName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Doctor name |
| specializationCode | string | Code |
| specializationName | string | Name |

## Relevant Database Tables
- **Doctor**: DoctorName
- Stored Procedure: `usp_AddDoctorDetails`

## Entity Relationships
- Pre-validation in EF Core combined with stored procedure execution.

## Required Operations
1. Check doctor existence in EF Core.
2. Return `-1` if duplicate.
3. Execute stored procedure if unique.
4. Return status code.

## Business Rules and Validations
1. Avoid SP invocation if duplicate exists (-1).

## Required IF-ELSE Conditions
- **Condition:** If duplicate exists return `-1`; else invoke SP and return result.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor name exists | `-1` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
