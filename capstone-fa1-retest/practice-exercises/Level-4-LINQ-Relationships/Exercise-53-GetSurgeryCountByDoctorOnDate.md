# Exercise 53: GetSurgeryCountByDoctorOnDate

## Business Scenario
Theater charge nurses monitor caseload distribution among surgeons on a given day to ensure no doctor is overloaded.

## Problem Statement
Define a DTO class `DoctorDateSurgeryCount`:
- `int DoctorId { get; set; }`
- `DateTime SurgeryDate { get; set; }`
- `int SurgeryCount { get; set; }`

Write a repository method `GetSurgeryCountByDoctorOnDate` taking `surgeryDate`.
Using LINQ Query Syntax, filter by date, group by `DoctorId`, and count:
```csharp
from s in context.Surgeries
where s.SurgeryDate == surgeryDate
group s by s.DoctorId into g
select new DoctorDateSurgeryCount
{
    DoctorId = g.Key,
    SurgeryDate = surgeryDate,
    SurgeryCount = g.Count()
}
```
Return `List<DoctorDateSurgeryCount>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<DoctorDateSurgeryCount> GetSurgeryCountByDoctorOnDate(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCountByDoctorOnDate(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Filter date |

## Relevant Database Tables
- **Surgery**: DoctorID, SurgeryDate

## Entity Relationships
- Grouped on `DoctorID`.

## Required Operations
1. Create DTO `DoctorDateSurgeryCount`.
2. LINQ Query Syntax: filter then group by.
3. Project key and count to DTO.
4. Return `.ToList()`.

## Business Rules and Validations
1. Grouping after date filter.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Group counts | `List<DoctorDateSurgeryCount>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorDateSurgeryCount`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
