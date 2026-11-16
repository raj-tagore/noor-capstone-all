# Exercise 56: GetSurgeryDetailsByDate

## Business Scenario
Operating theatre personnel need to retrieve daily surgery schedules by calling the database table-valued function `ufn_FetchSurgeryDetails`.

## Problem Statement
Write a repository method `GetSurgeryDetailsByDate` accepting `surgeryDate` (DateTime).
Call the TVF `ufn_FetchSurgeryDetails` using EF Core `FromSqlInterpolated`:
```csharp
context.SurgeryDetails
    .FromSqlInterpolated($"SELECT * FROM ufn_FetchSurgeryDetails({surgeryDate})")
    .ToList();
```
Return `List<SurgeryDetail>`. If an exception occurs, return `null`.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetail> GetSurgeryDetailsByDate(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` taking `surgeryDate` as a query parameter and returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsByDate(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Target surgery date |

## Relevant Database Tables
- Function: `ufn_FetchSurgeryDetails(@SurgeryDate DATE)`
- Output mapped to model: `SurgeryDetail` (SurgeryId, DoctorName, SpecializationName, SurgeryDate, StartTime, EndTime)

## Entity Relationships
- Function joins `Surgery`, `Doctor`, and `Specialization`.

## Required Operations
1. Invoke TVF using `FromSqlInterpolated`.
2. Convert results `.ToList()`.
3. Return list or null on exception.

## Business Rules and Validations
1. Maps to keyless entity `SurgeryDetail`.

## Required IF-ELSE Conditions
- None in DAL query.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Records found | `List<SurgeryDetail>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
