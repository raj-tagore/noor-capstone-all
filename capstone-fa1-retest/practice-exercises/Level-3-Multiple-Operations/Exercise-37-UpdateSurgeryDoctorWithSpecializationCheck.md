# Exercise 37: UpdateSurgeryDoctorWithSpecializationCheck

## Business Scenario
A surgery can only be transferred to a new doctor if the new doctor holds credentials in the specialization category required by the surgery.

## Problem Statement
Write a repository method `UpdateSurgeryDoctorWithSpecializationCheck` taking `surgeryId` and `newDoctorId`.
Execute three LINQ validations:
1. Find surgery by `surgeryId`. If null, return `-1`.
2. Find new doctor in `Doctor` table by `newDoctorId`. If null, return `-2`.
3. Verify if `newDoctorId` holds the surgery's `SurgeryCategory` in `DoctorSpecialization` using LINQ Query Syntax:
   `from ds in context.DoctorSpecializations where ds.DoctorId == newDoctorId && ds.SpecializationCode == surgery.SurgeryCategory select ds`
   If `FirstOrDefault() == null`, return `-3`.
4. If all validations pass, update `surgery.DoctorId = newDoctorId`, save changes, and return `1`.
5. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateSurgeryDoctorWithSpecializationCheck(int surgeryId, int newDoctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateSurgeryDoctorWithSpecializationCheck(int surgeryId, int newDoctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery ID |
| newDoctorId | int | Proposed new doctor ID |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryCategory
- **Doctor**: DoctorID
- **DoctorSpecialization**: DoctorID, SpecializationCode

## Entity Relationships
- Multi-table verification involving `Surgery`, `Doctor`, and `DoctorSpecialization`.

## Required Operations
1. Locate surgery; if null return `-1`.
2. Locate new doctor; if null return `-2`.
3. Check doctor qualification in `DoctorSpecialization`; if null return `-3`.
4. Update `DoctorId`, save changes, return `1`.

## Business Rules and Validations
1. Surgery must exist (-1).
2. Doctor must exist (-2).
3. Doctor must have category qualification (-3).

## Required IF-ELSE Conditions
- **Condition 1:** If surgery is null -> -1.
- **Condition 2:** Else if doctor is null -> -2.
- **Condition 3:** Else if qualification is null -> -3.
- **Condition 4:** Else update and return 1.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Doctor not found | `-2` |
| Doctor lacks required specialization | `-3` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
