# Exercise 76: GetSpecializationReport

## Business Scenario
Hospital board members review annual operational metrics across clinical departments, requiring a multi-table aggregation report per specialization.

## Problem Statement
Define a DTO class `SpecializationReport`:
- `string SpecializationCode { get; set; }`
- `string SpecializationName { get; set; }`
- `int DoctorCount { get; set; }`
- `int SurgeryCount { get; set; }`
- `decimal TotalSurgeryHours { get; set; }`

Write a repository method `GetSpecializationReport`.
Using LINQ Query Syntax, iterate or group-join `Specialization` with `DoctorSpecialization` and `Surgery`:
```csharp
from sp in context.Specializations
join ds in context.DoctorSpecializations on sp.SpecializationCode equals ds.SpecializationCode into docGroup
join s in context.Surgeries on sp.SpecializationCode equals s.SurgeryCategory into surgGroup
select new SpecializationReport
{
    SpecializationCode = sp.SpecializationCode,
    SpecializationName = sp.SpecializationName,
    DoctorCount = docGroup.Count(),
    SurgeryCount = surgGroup.Count(),
    TotalSurgeryHours = surgGroup.Sum(x => (decimal?)(x.EndTime - x.StartTime)) ?? 0m
}
```
Return `List<SpecializationReport>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SpecializationReport> GetSpecializationReport()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSpecializationReport()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Specialization**: SpecializationCode, SpecializationName
- **DoctorSpecialization**: SpecializationCode
- **Surgery**: SurgeryCategory, StartTime, EndTime

## Entity Relationships
- Multi-table group join across three tables.

## Required Operations
1. Create `SpecializationReport` DTO.
2. Group-join `Specialization` with `DoctorSpecialization` and `Surgery`.
3. Compute counts and sum of hours.
4. Return `.ToList()`.

## Business Rules and Validations
1. Handle zero values gracefully using null-coalescing.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Report generated | `List<SpecializationReport>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SpecializationReport`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
