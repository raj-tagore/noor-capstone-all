# Exercise 50: GetSurgeryHoursByDoctor

## Business Scenario
The clinical director evaluates surgeon operating room engagement by calculating total surgical hours logged by each doctor.

## Problem Statement
Define a DTO class `DoctorSurgeryHours`:
- `int DoctorId { get; set; }`
- `decimal TotalHours { get; set; }`

Write a repository method `GetSurgeryHoursByDoctor`.
Using LINQ Query Syntax with `group by` and aggregate `.Sum()`:
```csharp
from s in context.Surgeries
group s by s.DoctorId into g
select new DoctorSurgeryHours
{
    DoctorId = g.Key,
    TotalHours = g.Sum(x => x.EndTime - x.StartTime)
}
```
Return `List<DoctorSurgeryHours>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<DoctorSurgeryHours> GetSurgeryHoursByDoctor()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryHoursByDoctor()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Surgery**: DoctorID, StartTime, EndTime

## Entity Relationships
- Grouping on `DoctorID`.

## Required Operations
1. Create DTO `DoctorSurgeryHours`.
2. Formulate LINQ group by query.
3. Aggregate duration using `.Sum()`.
4. Project to DTO and return `.ToList()`.

## Business Rules and Validations
1. Duration = EndTime - StartTime.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Aggregated results | `List<DoctorSurgeryHours>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorSurgeryHours`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
