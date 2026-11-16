# Exercise 48: GetDoctorSpecializationDetails

## Business Scenario
The hospital human resources department maintains a credentialing directory showing each doctor's name, specialization title, and certification date.

## Problem Statement
Define a DTO class `DoctorSpecDetail`:
- `int DoctorId { get; set; }`
- `string DoctorName { get; set; }`
- `string SpecializationCode { get; set; }`
- `string SpecializationName { get; set; }`
- `DateTime SpecializationDate { get; set; }`

Write a repository method `GetDoctorSpecializationDetails`.
Using LINQ Query Syntax, perform a **three-table join**:
```csharp
from ds in context.DoctorSpecializations
join d in context.Doctors on ds.DoctorId equals d.DoctorId
join s in context.Specializations on ds.SpecializationCode equals s.SpecializationCode
select new DoctorSpecDetail
{
    DoctorId = ds.DoctorId,
    DoctorName = d.DoctorName,
    SpecializationCode = ds.SpecializationCode,
    SpecializationName = s.SpecializationName,
    SpecializationDate = ds.SpecializationDate
}
```
Return `List<DoctorSpecDetail>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<DoctorSpecDetail> GetDoctorSpecializationDetails()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorSpecializationDetails()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Specialization**: SpecializationCode, SpecializationName
- **DoctorSpecialization**: DoctorID, SpecializationCode, SpecializationDate

## Entity Relationships
- 3-table join through junction table `DoctorSpecialization`.

## Required Operations
1. Declare DTO `DoctorSpecDetail`.
2. Execute 3-table join in Query Syntax.
3. Project with `select new`.
4. Return `.ToList()`.

## Business Rules and Validations
1. Query Syntax only.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Records found | `List<DoctorSpecDetail>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorSpecDetail`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
