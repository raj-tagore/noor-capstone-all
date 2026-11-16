# Exercise 34: RemoveDoctorSpecialization

## Business Scenario
A doctor relinquishes a specific credential while maintaining their doctor profile and other specialties.

## Problem Statement
Write a repository method `RemoveDoctorSpecialization` accepting `doctorId` and `specializationCode`.
Locate the mapping row using LINQ Query Syntax with `FirstOrDefault()` filtering on both keys.
If not found, return `false`.
If found, call `context.DoctorSpecializations.Remove(entity)`, execute `SaveChanges()`, and return `true`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool RemoveDoctorSpecialization(int doctorId, string specializationCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action in `CureHospitalController` returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult RemoveDoctorSpecialization(int doctorId, string specializationCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| specializationCode | string | Specialization Code |

## Relevant Database Tables
- **DoctorSpecialization**: DoctorID, SpecializationCode

## Entity Relationships
- Deletion on composite key entity.

## Required Operations
1. Use LINQ Query Syntax to find target record.
2. If null, return `false`.
3. Call `Remove()` and `SaveChanges()`.
4. Return `true`.

## Business Rules and Validations
1. Record must exist.

## Required IF-ELSE Conditions
- **Condition:** If `record == null` return `false`; else delete and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Record not found | `false` |
| Successfully removed | `true` |
| Exception caught | `false` |

## Exception Handling Requirements
Try-catch block returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
