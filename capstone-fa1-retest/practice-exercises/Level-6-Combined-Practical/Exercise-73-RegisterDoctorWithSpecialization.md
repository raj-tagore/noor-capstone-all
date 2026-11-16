# Exercise 73: RegisterDoctorWithSpecialization

## Business Scenario
Onboarding a new medical practitioner involves registering the doctor's profile and immediately issuing their initial credential mapping in `DoctorSpecialization` within a single cohesive transaction.

## Problem Statement
Write a repository method `RegisterDoctorWithSpecialization` taking `doctorName`, `specializationCode`, and `specializationDate`.
Validation and execution pipeline:
1. Check if a doctor with `doctorName` already exists via LINQ Query Syntax. If found, return `-1`.
2. Check if `specializationCode` exists in `Specialization` table via `Find()`. If not found, return `-2`.
3. Create a new `Doctor` entity, set `DoctorName = doctorName`, add to `context.Doctors`, and call `context.SaveChanges()` so that SQL Server generates the identity `DoctorID`.
4. Create a new `DoctorSpecialization` entity using the generated `DoctorId`, `specializationCode`, and `specializationDate`.
5. Add to `context.DoctorSpecializations`, call `context.SaveChanges()`, and return the newly generated `DoctorId`.
6. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int RegisterDoctorWithSpecialization(string doctorName, string specializationCode, DateTime specializationDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the resulting doctor ID or error code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult RegisterDoctorWithSpecialization(string doctorName, string specializationCode, DateTime specializationDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Name of physician |
| specializationCode | string | Specialty code |
| specializationDate | DateTime | Awarded date |

## Relevant Database Tables
- **Doctor**: DoctorID (IDENTITY), DoctorName
- **Specialization**: SpecializationCode
- **DoctorSpecialization**: DoctorID, SpecializationCode, SpecializationDate

## Entity Relationships
- Multi-table add: `Doctor` -> `DoctorSpecialization`.

## Required Operations
1. Duplicate check on `DoctorName` (-1).
2. Existence check on `SpecializationCode` (-2).
3. Insert `Doctor`, save to acquire identity PK.
4. Insert `DoctorSpecialization` with acquired PK.
5. Save changes and return new `DoctorId`.

## Business Rules and Validations
1. Unique doctor name (-1).
2. Existing specialization (-2).
3. Identity PK propagation.

## Required IF-ELSE Conditions
- **Condition 1:** If duplicate doctor name exists -> return -1.
- **Condition 2:** Else if specialization does not exist -> return -2.
- **Condition 3:** Else insert both and return new DoctorId.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Duplicate doctor name | `-1` |
| Specialization not found | `-2` |
| Success | Newly generated `DoctorId` (>= 1001) |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
