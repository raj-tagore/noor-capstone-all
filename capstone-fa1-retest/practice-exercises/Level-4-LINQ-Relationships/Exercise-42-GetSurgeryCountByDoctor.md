# Exercise 42: GetSurgeryCountByDoctor

## Business Scenario
Hospital management tracks surgical workload by counting the total number of surgeries assigned to a doctor.

## Problem Statement
Write a repository method `GetSurgeryCountByDoctor` taking `doctorId`.
Using LINQ Query Syntax:
```csharp
(from s in context.Surgeries
 where s.DoctorId == doctorId
 select s).Count()
```
Return the integer count. On exception, return `-1`.

## DAL / Repository Method Signature
```csharp
public int GetSurgeryCountByDoctor(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the count.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCountByDoctor(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID

## Entity Relationships
- Filtered on `DoctorID`.

## Required Operations
1. Formulate LINQ Query Syntax on `Surgeries`.
2. Apply `.Count()` aggregate.
3. Return integer.

## Business Rules and Validations
1. Return 0 if no surgeries exist for the doctor.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Count calculated | Non-negative integer (e.g. 0, 4) |
| Exception caught | `-1` |

## Exception Handling Requirements
Try-catch block returning `-1`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
