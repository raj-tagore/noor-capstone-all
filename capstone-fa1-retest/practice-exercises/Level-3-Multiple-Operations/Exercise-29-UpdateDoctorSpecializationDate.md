# Exercise 29: UpdateDoctorSpecializationDate

## Business Scenario
Credentialing audits require updating the certified `SpecializationDate` for a doctor's specialization qualification. Because `DoctorSpecialization` has a composite primary key, both keys must be used to locate the row.

## Problem Statement
Write a repository method `UpdateDoctorSpecializationDate` accepting `doctorId`, `specializationCode`, and `newDate`.
Use LINQ Query Syntax with `FirstOrDefault()` to locate the record:
`from ds in context.DoctorSpecializations where ds.DoctorId == doctorId && ds.SpecializationCode == specializationCode select ds`
If record is null, return `false`.
If found, set `SpecializationDate = newDate`, call `SaveChanges()`, and return `true`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool UpdateDoctorSpecializationDate(int doctorId, string specializationCode, DateTime newDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateDoctorSpecializationDate(int doctorId, string specializationCode, DateTime newDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| specializationCode | string | Specialization Code |
| newDate | DateTime | Revised credential date |

## Relevant Database Tables
- **DoctorSpecialization**: DoctorID, SpecializationCode, SpecializationDate

## Entity Relationships
- Composite primary key on `DoctorSpecialization`.

## Required Operations
1. Use LINQ Query Syntax to locate matching `DoctorSpecialization`.
2. Check if null; return `false` if not found.
3. Update `SpecializationDate = newDate`.
4. Call `SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Composite key match required.

## Required IF-ELSE Conditions
- **Condition:** If `mapping == null` return `false`; else update and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Mapping not found | `false` |
| Updated successfully | `true` |
| Exception | `false` |

## Exception Handling Requirements
Try-catch returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
