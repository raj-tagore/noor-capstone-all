# Exercise 47: GetSurgeryFullDetails

## Business Scenario
The clinical intake team requires a comprehensive master surgery view displaying the surgery ID, surgeon name, specialization name, and operating time window without relying on database stored functions.

## Problem Statement
Define a DTO class `SurgeryFullDetail` with:
- `int SurgeryId { get; set; }`
- `string DoctorName { get; set; }`
- `string SpecializationName { get; set; }`
- `DateTime SurgeryDate { get; set; }`
- `decimal StartTime { get; set; }`
- `decimal EndTime { get; set; }`

Write a repository method `GetSurgeryFullDetails` taking `surgeryDate`.
Using LINQ Query Syntax with a **three-table join**:
```csharp
from s in context.Surgeries
join d in context.Doctors on s.DoctorId equals d.DoctorId
join sp in context.Specializations on s.SurgeryCategory equals sp.SpecializationCode
where s.SurgeryDate == surgeryDate
select new SurgeryFullDetail
{
    SurgeryId = s.SurgeryId,
    DoctorName = d.DoctorName,
    SpecializationName = sp.SpecializationName,
    SurgeryDate = s.SurgeryDate,
    StartTime = s.StartTime,
    EndTime = s.EndTime
}
```
Return `List<SurgeryFullDetail>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SurgeryFullDetail> GetSurgeryFullDetails(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryFullDetails(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Surgery date |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory
- **Doctor**: DoctorID, DoctorName
- **Specialization**: SpecializationCode, SpecializationName

## Entity Relationships
- 3-table join: `Surgery` joins `Doctor` on `DoctorID`, joins `Specialization` on `SurgeryCategory`.

## Required Operations
1. Create `SurgeryFullDetail` DTO.
2. Join all 3 tables using LINQ Query Syntax.
3. Project to DTO via `select new`.
4. Filter on `s.SurgeryDate == surgeryDate`.
5. Return `.ToList()`.

## Business Rules and Validations
1. 3-table join in pure Query Syntax.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Results found | `List<SurgeryFullDetail>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SurgeryFullDetail`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
