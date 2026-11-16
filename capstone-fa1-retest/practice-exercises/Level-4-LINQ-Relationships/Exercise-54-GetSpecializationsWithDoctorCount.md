# Exercise 54: GetSpecializationsWithDoctorCount

## Business Scenario
Hospital leadership assesses department staffing strength by reviewing how many doctors are credentialed in each specialization.

## Problem Statement
Define a DTO class `SpecDoctorCount`:
- `string SpecializationCode { get; set; }`
- `string SpecializationName { get; set; }`
- `int DoctorCount { get; set; }`

Write a repository method `GetSpecializationsWithDoctorCount`.
Using LINQ Query Syntax, join `Specialization` and `DoctorSpecialization`, group by specialization, and count:
```csharp
from s in context.Specializations
join ds in context.DoctorSpecializations on s.SpecializationCode equals ds.SpecializationCode into g
select new SpecDoctorCount
{
    SpecializationCode = s.SpecializationCode,
    SpecializationName = s.SpecializationName,
    DoctorCount = g.Count()
}
```
Return `List<SpecDoctorCount>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SpecDoctorCount> GetSpecializationsWithDoctorCount()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSpecializationsWithDoctorCount()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Specialization**: SpecializationCode, SpecializationName
- **DoctorSpecialization**: DoctorID, SpecializationCode

## Entity Relationships
- Group join between `Specialization` and `DoctorSpecialization`.

## Required Operations
1. Create `SpecDoctorCount` DTO.
2. Formulate LINQ group join.
3. Project count to DTO.
4. Return `.ToList()`.

## Business Rules and Validations
1. Include specializations even if doctor count is 0.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Staffing summary | `List<SpecDoctorCount>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SpecDoctorCount`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
