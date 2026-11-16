# Exercise 82: UpdateDoctorSpecializationAndVerifySurgeries

## Business Scenario
A doctor's credential can only be modified if they do not have active surgeries scheduled under their current specialization, preventing scheduled surgeries from being orphaned without a credentialed lead doctor.

## Problem Statement
Write a repository method `UpdateDoctorSpecializationAndVerifySurgeries` accepting `doctorId`, `oldSpecCode`, and `newSpecCode`.
Validations:
1. Find mapping `(doctorId, oldSpecCode)` in `DoctorSpecialization`. If not found, return `-1`.
2. Check if `newSpecCode` exists in `Specialization`. If not found, return `-2`.
3. Check if doctor has any surgeries where `DoctorId == doctorId && SurgeryCategory == oldSpecCode`. If active surgeries exist, return `-3` (cannot change credential while active surgeries are booked under this category).
4. If no conflict, remove the old mapping, add the new mapping `(doctorId, newSpecCode, DateTime.Today)`, save changes, and return `1`.
5. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateDoctorSpecializationAndVerifySurgeries(int doctorId, string oldSpecCode, string newSpecCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateDoctorSpecializationAndVerifySurgeries(int doctorId, string oldSpecCode, string newSpecCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| oldSpecCode | string | Existing specialty |
| newSpecCode | string | Proposed specialty |

## Relevant Database Tables
- **DoctorSpecialization**: DoctorID, SpecializationCode
- **Specialization**: SpecializationCode
- **Surgery**: DoctorID, SurgeryCategory

## Entity Relationships
- Cross-table validation guarding integrity across `DoctorSpecialization` and `Surgery`.

## Required Operations
1. Locate old mapping (-1).
2. Check new specialization existence (-2).
3. Check for active surgeries under old category (-3).
4. Remove old, add new, save changes (1).

## Business Rules and Validations
1. Integrity rule: cannot change credential if surgeries reference old category.

## Required IF-ELSE Conditions
- **Condition 1:** If old mapping missing -> -1.
- **Condition 2:** Else if new spec missing -> -2.
- **Condition 3:** Else if active surgeries found -> -3.
- **Condition 4:** Else update and return 1.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Old credential missing | `-1` |
| New specialty invalid | `-2` |
| Active surgeries exist under old category | `-3` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
