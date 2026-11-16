# Exercise 46: GetDoctorSurgeryInfo

## Business Scenario
A dashboard view requires combining surgery schedules with the surgeon's full name, which requires joining `Surgery` and `Doctor` into a custom Data Transfer Object (DTO).

## Problem Statement
Define a DTO class `DoctorSurgeryInfo` with properties:
- `int SurgeryId { get; set; }`
- `string DoctorName { get; set; }`
- `DateTime SurgeryDate { get; set; }`
- `decimal StartTime { get; set; }`
- `decimal EndTime { get; set; }`
- `string SurgeryCategory { get; set; }`

Write a repository method `GetDoctorSurgeryInfo` taking `doctorId`.
Using LINQ Query Syntax with `select new DoctorSurgeryInfo`:
```csharp
from s in context.Surgeries
join d in context.Doctors on s.DoctorId equals d.DoctorId
where s.DoctorId == doctorId
select new DoctorSurgeryInfo
{
    SurgeryId = s.SurgeryId,
    DoctorName = d.DoctorName,
    SurgeryDate = s.SurgeryDate,
    StartTime = s.StartTime,
    EndTime = s.EndTime,
    SurgeryCategory = s.SurgeryCategory
}
```
Return `List<DoctorSurgeryInfo>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<DoctorSurgeryInfo> GetDoctorSurgeryInfo(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the DTO list.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorSurgeryInfo(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target doctor ID |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- 2-table join: `Surgery` and `Doctor` on `DoctorID`.

## Required Operations
1. Declare DTO class `DoctorSurgeryInfo`.
2. Join `Surgery` and `Doctor` via Query Syntax.
3. Project into `DoctorSurgeryInfo` with `select new`.
4. Return `.ToList()`.

## Business Rules and Validations
1. LINQ Query Syntax only.
2. DTO projection required.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<DoctorSurgeryInfo>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorSurgeryInfo`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
