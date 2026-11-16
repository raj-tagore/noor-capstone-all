# Exercise 55: GetDoctorSurgerySummary

## Business Scenario
Annual physician reviews summarize each surgeon's performance, including total surgeries conducted and total operating theater hours logged.

## Problem Statement
Define a DTO class `DoctorSurgerySummary`:
- `int DoctorId { get; set; }`
- `string DoctorName { get; set; }`
- `int TotalSurgeries { get; set; }`
- `decimal TotalHours { get; set; }`

Write a repository method `GetDoctorSurgerySummary`.
Using LINQ Query Syntax, join `Doctor` with `Surgery`, group by doctor, and calculate aggregates:
```csharp
from d in context.Doctors
join s in context.Surgeries on d.DoctorId equals s.DoctorId into surgeryGroup
select new DoctorSurgerySummary
{
    DoctorId = d.DoctorId,
    DoctorName = d.DoctorName,
    TotalSurgeries = surgeryGroup.Count(),
    TotalHours = surgeryGroup.Sum(x => (decimal?)(x.EndTime - x.StartTime)) ?? 0m
}
```
Return `List<DoctorSurgerySummary>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<DoctorSurgerySummary> GetDoctorSurgerySummary()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorSurgerySummary()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, StartTime, EndTime

## Entity Relationships
- Group join: `Doctor` with `Surgery`.

## Required Operations
1. Create DTO `DoctorSurgerySummary`.
2. Group join doctors and surgeries in LINQ Query Syntax.
3. Project `Count()` and `Sum()` into DTO.
4. Return `.ToList()`.

## Business Rules and Validations
1. Handle doctors with zero surgeries gracefully (0 hours).

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Summary produced | `List<DoctorSurgerySummary>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorSurgerySummary`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
