# Exercise 74: GetDoctorSchedule

## Business Scenario
A surgeon's daily clinic roster requires assembling an executive schedule DTO containing doctor profile details and an embedded collection of scheduled surgeries.

## Problem Statement
Define DTO classes:
```csharp
public class ScheduleItem
{
    public int SurgeryId { get; set; }
    public string SpecializationName { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
}

public class DoctorSchedule
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime SurgeryDate { get; set; }
    public List<ScheduleItem> Surgeries { get; set; }
}
```

Write a repository method `GetDoctorSchedule` taking `doctorId` and `surgeryDate`.
1. Query `Doctor` table by `doctorId`. If null, return `null`.
2. Using LINQ Query Syntax, join `Surgery` and `Specialization` for that doctor on that date:
```csharp
from s in context.Surgeries
join sp in context.Specializations on s.SurgeryCategory equals sp.SpecializationCode
where s.DoctorId == doctorId && s.SurgeryDate == surgeryDate
orderby s.StartTime
select new ScheduleItem
{
    SurgeryId = s.SurgeryId,
    SpecializationName = sp.SpecializationName,
    StartTime = s.StartTime,
    EndTime = s.EndTime
}
```
3. Assemble and return `DoctorSchedule` object. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public DoctorSchedule GetDoctorSchedule(int doctorId, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorSchedule(int doctorId, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| surgeryDate | DateTime | Workday date |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory
- **Specialization**: SpecializationCode, SpecializationName

## Entity Relationships
- Parent entity `Doctor` linked to joined child collection `Surgery` + `Specialization`.

## Required Operations
1. Declare DTOs `ScheduleItem` and `DoctorSchedule`.
2. Find doctor; return null if missing.
3. Query surgeries joined with specialization in Query Syntax.
4. Assemble parent DTO with child list.
5. Return result.

## Business Rules and Validations
1. Return null if doctor does not exist.

## Required IF-ELSE Conditions
- **Condition:** If `doctor == null` return `null`; else build DTO.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor found | `DoctorSchedule` DTO |
| Doctor not found | `null` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `ScheduleItem`
- [ ] DTO class `DoctorSchedule`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
