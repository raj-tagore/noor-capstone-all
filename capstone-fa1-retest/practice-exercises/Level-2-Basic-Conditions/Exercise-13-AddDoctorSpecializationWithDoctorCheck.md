# Exercise 13: AddDoctorSpecializationWithDoctorCheck

## Business Scenario
When credentialing doctors with hospital specialties, CureHospital records links in the `DoctorSpecialization` junction table. To prevent orphan references, the system must first check that the specified Doctor ID actually belongs to an existing doctor before inserting.

## Problem Statement
Implement a repository method `AddDoctorSpecializationWithDoctorCheck` that accepts `doctorId`, `specializationCode`, and `specializationDate`. Before adding the mapping, verify whether the `DoctorID` exists in the `Doctor` table using `Find()`. If the doctor does not exist, return status code `-1`. If the doctor exists, instantiate `DoctorSpecialization`, assign properties, persist to database, and return status code `1`. If an exception occurs, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddDoctorSpecializationWithDoctorCheck(int doctorId, string specializationCode, DateTime specializationDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` that calls this method and returns `JsonResult` containing the status integer.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorSpecializationWithDoctorCheck(int doctorId, string specializationCode, DateTime specializationDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor identifier to validate |
| specializationCode | string | Specialization code (3 chars) |
| specializationDate | DateTime | Date credential was awarded |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK)
- **DoctorSpecialization**: DoctorID (INT, FK), SpecializationCode (CHAR(3), FK), SpecializationDate (DATE)

## Entity Relationships
- `DoctorSpecialization` has a foreign key `DoctorID` referencing `Doctor(DoctorID)`.

## Required Operations
1. Query `context.Doctors.Find(doctorId)`.
2. Check if doctor entity is null.
3. If null, return `-1`.
4. If exists, construct `DoctorSpecialization` with given parameters.
5. Add entity to `context.DoctorSpecializations`, call `SaveChanges()`.
6. Return `1`.

## Business Rules and Validations
1. Doctor must exist in `Doctor` table.
2. Failure to find doctor yields `-1`.

## Required IF-ELSE Conditions
- **Condition:** If `doctor == null`, return `-1`. Else proceed with add and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor does not exist | `-1` |
| Successfully assigned | `1` |
| Exception occurred | `-99` |

## Exception Handling Requirements
Catch all exceptions in repository method and return `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
