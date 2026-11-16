# Exercise 41: GetSurgeriesByDoctorName

## Business Scenario
Theater nurses look up all scheduled surgeries by typing the surgeon's name rather than their system numeric identifier.

## Problem Statement
Write a repository method `GetSurgeriesByDoctorName` accepting `doctorName`.
Using LINQ Query Syntax, join `Surgery` and `Doctor`:
```csharp
from s in context.Surgeries
join d in context.Doctors on s.DoctorId equals d.DoctorId
where d.DoctorName == doctorName
select s
```
Return `List<Surgery>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<Surgery> GetSurgeriesByDoctorName(string doctorName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeriesByDoctorName(string doctorName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Name of surgeon |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- `Surgery` to `Doctor` on `DoctorID`.

## Required Operations
1. Join `Surgery` with `Doctor` using LINQ Query Syntax.
2. Filter by `d.DoctorName == doctorName`.
3. Select `s` and call `.ToList()`.

## Business Rules and Validations
1. Exact name match.

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
