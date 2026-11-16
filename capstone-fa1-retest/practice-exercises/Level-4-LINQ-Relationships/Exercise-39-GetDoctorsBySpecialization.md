# Exercise 39: GetDoctorsBySpecialization

## Business Scenario
Patients needing specialized consultation search for doctors qualified under a designated specialization code (e.g., 'CAR' for Cardiologist).

## Problem Statement
Write a repository method `GetDoctorsBySpecialization` taking `specializationCode`.
Using LINQ Query Syntax, join `DoctorSpecialization` and `Doctor`:
```csharp
from ds in context.DoctorSpecializations
join d in context.Doctors on ds.DoctorId equals d.DoctorId
where ds.SpecializationCode == specializationCode
select d
```
Return `List<Doctor>`. If no doctors match or if an exception occurs, return `null` (or empty list if caught).

## DAL / Repository Method Signature
```csharp
public List<Doctor> GetDoctorsBySpecialization(string specializationCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` accepting `specializationCode` as a query parameter and returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorsBySpecialization(string specializationCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | 3-char specialization code |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **DoctorSpecialization**: DoctorID, SpecializationCode

## Entity Relationships
- `Doctor` joined with `DoctorSpecialization` on `DoctorID`.

## Required Operations
1. Join `DoctorSpecialization` and `Doctor` using LINQ Query Syntax.
2. Filter on `ds.SpecializationCode == specializationCode`.
3. Select `d`.
4. Apply `.ToList()`.
5. Return the resulting list.

## Business Rules and Validations
1. Query Syntax only.

## Required IF-ELSE Conditions
- None required in DAL query.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Matching doctors found | `List<Doctor>` |
| No matches | Empty list or null |
| Exception caught | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
