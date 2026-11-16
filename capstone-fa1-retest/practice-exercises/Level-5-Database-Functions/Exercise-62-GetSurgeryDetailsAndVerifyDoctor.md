# Exercise 62: GetSurgeryDetailsAndVerifyDoctor

## Business Scenario
A patient care coordinator verifies whether an attending physician is registered in the hospital before pulling their daily TVF schedule.

## Problem Statement
Write a repository method `GetSurgeryDetailsAndVerifyDoctor` taking `surgeryDate` and `doctorId`.
1. Check if `doctorId` exists in `Doctor` table via `context.Doctors.Find(doctorId)`. If null, return `null`.
2. If doctor exists, call TVF `ufn_FetchSurgeryDetails` for `surgeryDate`.
3. Using LINQ Query Syntax, filter TVF results matching `d.DoctorName == doctor.DoctorName`.
4. Return `List<SurgeryDetail>`. On exception, return `null`.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetail> GetSurgeryDetailsAndVerifyDoctor(DateTime surgeryDate, int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsAndVerifyDoctor(DateTime surgeryDate, int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Target date |
| doctorId | int | Doctor ID |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- TVF: `ufn_FetchSurgeryDetails`

## Entity Relationships
- Combines EF entity lookup with TVF result filtering.

## Required Operations
1. Lookup doctor using `Find(doctorId)`.
2. If null, return `null`.
3. Call TVF.
4. Filter on `DoctorName`.
5. Return list.

## Business Rules and Validations
1. Doctor must exist.

## Required IF-ELSE Conditions
- **Condition:** If `doctor == null` return `null`; else query TVF and return filtered results.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `null` |
| Surgeries found | `List<SurgeryDetail>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
