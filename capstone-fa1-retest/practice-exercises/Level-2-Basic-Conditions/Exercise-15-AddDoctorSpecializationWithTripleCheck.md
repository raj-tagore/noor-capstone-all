# Exercise 15: AddDoctorSpecializationWithTripleCheck

## Business Scenario
A doctor cannot be assigned the same medical specialization twice, as the composite primary key `(DoctorID, SpecializationCode)` enforces uniqueness. The DAL must validate foreign keys and avoid primary key collision before insertion.

## Problem Statement
Write a repository method `AddDoctorSpecializationWithTripleCheck` taking `doctorId`, `specializationCode`, and `specializationDate`. Validate:
1. Doctor exists (return `-1` if not found).
2. Specialization exists (return `-2` if not found).
3. The combination `(doctorId, specializationCode)` does not already exist in `DoctorSpecialization` using a LINQ query syntax query. If already present, return `-3`.
If all three validations succeed, insert the record, save changes, and return `1`. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddDoctorSpecializationWithTripleCheck(int doctorId, string specializationCode, DateTime specializationDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` wrapping this method and returning a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorSpecializationWithTripleCheck(int doctorId, string specializationCode, DateTime specializationDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target doctor ID |
| specializationCode | string | Target specialization code |
| specializationDate | DateTime | Awarded date |

## Relevant Database Tables
- **Doctor**: DoctorID (PK)
- **Specialization**: SpecializationCode (PK)
- **DoctorSpecialization**: (DoctorID, SpecializationCode) PK

## Entity Relationships
- Composite primary key on `DoctorSpecialization(DoctorID, SpecializationCode)`.

## Required Operations
1. Validate doctor via `Find()`; if null return `-1`.
2. Validate specialization via `Find()`; if null return `-2`.
3. Check existing mapping using LINQ Query Syntax:
   `from ds in context.DoctorSpecializations where ds.DoctorId == doctorId && ds.SpecializationCode == specializationCode select ds`
   If `FirstOrDefault() != null`, return `-3`.
4. Create and add new `DoctorSpecialization`, call `SaveChanges()`.
5. Return `1`.

## Business Rules and Validations
1. Doctor existence check (-1).
2. Specialization existence check (-2).
3. Duplicate mapping check (-3).

## Required IF-ELSE Conditions
- **Condition 1:** If doctor not found -> `-1`.
- **Condition 2:** Else if spec not found -> `-2`.
- **Condition 3:** Else if mapping found -> `-3`.
- **Condition 4:** Else insert -> `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `-1` |
| Specialization not found | `-2` |
| Duplicate specialization assignment | `-3` |
| Successfully created | `1` |
| Exception occurred | `-99` |

## Exception Handling Requirements
Catch block returns `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
