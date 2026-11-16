# Exercise 14: AddDoctorSpecializationWithBothChecks

## Business Scenario
During medical staff registration, assigning a specialization requires validating both foreign key dependencies: the doctor must exist in the database and the requested medical specialization must also exist.

## Problem Statement
Implement a repository method `AddDoctorSpecializationWithBothChecks` accepting `doctorId`, `specializationCode`, and `specializationDate`. Validate sequentially:
1. Does the doctor exist in `Doctor`? If not, return `-1`.
2. Does the specialization exist in `Specialization`? If not, return `-2`.
If both validations pass, create the `DoctorSpecialization` object, add it to `DoctorSpecializations`, save changes, and return `1`. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddDoctorSpecializationWithBothChecks(int doctorId, string specializationCode, DateTime specializationDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` invoking this DAL method and returning the integer code as `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorSpecializationWithBothChecks(int doctorId, string specializationCode, DateTime specializationDate)
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
- **DoctorSpecialization**: DoctorID (FK), SpecializationCode (FK), SpecializationDate

## Entity Relationships
- Many-to-many relationship mapped via `DoctorSpecialization` between `Doctor` and `Specialization`.

## Required Operations
1. Search `Doctor` by `doctorId` using `Find()`. If null, return `-1`.
2. Search `Specialization` by `specializationCode` using `Find()`. If null, return `-2`.
3. Instantiate `DoctorSpecialization`, assign fields.
4. Add to context and execute `SaveChanges()`.
5. Return `1`.

## Business Rules and Validations
1. Doctor must exist (code `-1`).
2. Specialization code must exist (code `-2`).

## Required IF-ELSE Conditions
- **Condition 1:** If `doctor == null`, return `-1`.
- **Condition 2:** Else if `spec == null`, return `-2`.
- **Condition 3:** Else insert and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor does not exist | `-1` |
| Specialization does not exist | `-2` |
| Successfully registered | `1` |
| System exception | `-99` |

## Exception Handling Requirements
Try-catch block around DAL logic returning `-99` on catch.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
