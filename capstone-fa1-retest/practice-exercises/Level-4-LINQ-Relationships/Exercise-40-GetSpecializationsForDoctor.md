# Exercise 40: GetSpecializationsForDoctor

## Business Scenario
On a physician's public profile page, the hospital website renders all clinical specializations the doctor holds.

## Problem Statement
Write a repository method `GetSpecializationsForDoctor` that takes `doctorId`.
Using LINQ Query Syntax, join `DoctorSpecialization` and `Specialization`:
```csharp
from ds in context.DoctorSpecializations
join s in context.Specializations on ds.SpecializationCode equals s.SpecializationCode
where ds.DoctorId == doctorId
select s
```
Return the resulting `List<Specialization>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<Specialization> GetSpecializationsForDoctor(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSpecializationsForDoctor(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target doctor ID |

## Relevant Database Tables
- **DoctorSpecialization**: DoctorID, SpecializationCode
- **Specialization**: SpecializationCode, SpecializationName

## Entity Relationships
- `DoctorSpecialization` joined with `Specialization` on `SpecializationCode`.

## Required Operations
1. Formulate LINQ Query Syntax joining tables.
2. Filter on `ds.DoctorId == doctorId`.
3. Select the `Specialization` entity `s`.
4. Return `.ToList()`.

## Business Rules and Validations
1. LINQ Query Syntax only.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specializations found | `List<Specialization>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
