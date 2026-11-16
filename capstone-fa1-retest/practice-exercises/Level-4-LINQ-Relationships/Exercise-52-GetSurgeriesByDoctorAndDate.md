# Exercise 52: GetSurgeriesByDoctorAndDate

## Business Scenario
A surgeon checks their personal roster for a specific workday by entering their ID and date.

## Problem Statement
Write a repository method `GetSurgeriesByDoctorAndDate` taking `doctorId` and `surgeryDate`.
Using LINQ Query Syntax with multiple conditions in `where`:
```csharp
from s in context.Surgeries
where s.DoctorId == doctorId && s.SurgeryDate == surgeryDate
orderby s.StartTime
select s
```
Return `List<Surgery>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<Surgery> GetSurgeriesByDoctorAndDate(int doctorId, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeriesByDoctorAndDate(int doctorId, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| surgeryDate | DateTime | Workday date |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime

## Entity Relationships
- Filtered on `DoctorID` and `SurgeryDate`.

## Required Operations
1. LINQ Query Syntax with compound `&&` in `where`.
2. Order by `StartTime`.
3. Return `.ToList()`.

## Business Rules and Validations
1. Must match both doctor and date.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<Surgery>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
