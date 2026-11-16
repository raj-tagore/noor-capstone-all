# Exercise 43: GetTotalSurgeryHoursByDoctor

## Business Scenario
Physician compensation requires calculating the total operating room hours logged by a doctor across all their performed surgeries.

## Problem Statement
Write a repository method `GetTotalSurgeryHoursByDoctor` taking `doctorId`.
Using LINQ Query Syntax, compute the sum of `(s.EndTime - s.StartTime)`:
```csharp
(from s in context.Surgeries
 where s.DoctorId == doctorId
 select (s.EndTime - s.StartTime)).Sum()
```
Return `decimal`. On exception, return `0m`.

## DAL / Repository Method Signature
```csharp
public decimal GetTotalSurgeryHoursByDoctor(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` with the total hours.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetTotalSurgeryHoursByDoctor(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, StartTime, EndTime

## Entity Relationships
- Filtered on `DoctorID`.

## Required Operations
1. Query `(s.EndTime - s.StartTime)` for given `doctorId`.
2. Apply `.Sum()`.
3. Return total decimal.

## Business Rules and Validations
1. Calculates cumulative surgical duration.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Hours totaled | `decimal` value (e.g., 14.50) |
| Exception caught | `0m` |

## Exception Handling Requirements
Try-catch block returning `0m`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
