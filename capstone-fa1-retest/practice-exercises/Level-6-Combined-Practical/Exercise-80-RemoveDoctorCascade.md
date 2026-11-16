# Exercise 80: RemoveDoctorCascade

## Business Scenario
When a physician leaves the hospital system, administrative guidelines require cascading the deletion: removing their credential mappings, removing all their surgeries, and finally removing the doctor entity in a single transaction.

## Problem Statement
Write a repository method `RemoveDoctorCascade` accepting `doctorId`.
1. Find doctor by `doctorId` via `context.Doctors.Find(doctorId)`. If null, return `-1`.
2. Retrieve all `DoctorSpecialization` records for that doctor and pass to `context.DoctorSpecializations.RemoveRange()`.
3. Retrieve all `Surgery` records for that doctor and pass to `context.Surgeries.RemoveRange()`.
4. Remove doctor via `context.Doctors.Remove(doctor)`.
5. Call `context.SaveChanges()`.
6. Return the total count of deleted entities (count of doctor specs + count of surgeries + 1).
7. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int RemoveDoctorCascade(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action in `CureHospitalController` returning `JsonResult` with the count of deleted entities.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult RemoveDoctorCascade(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID to remove |

## Relevant Database Tables
- **Doctor**: DoctorID
- **DoctorSpecialization**: DoctorID
- **Surgery**: DoctorID

## Entity Relationships
- Cascading removal across three related tables.

## Required Operations
1. Locate doctor (-1 if missing).
2. Remove child DoctorSpecializations via `RemoveRange`.
3. Remove child Surgeries via `RemoveRange`.
4. Remove parent Doctor.
5. Save changes and return cumulative count.

## Business Rules and Validations
1. Children must be deleted before parent to satisfy FK constraints.

## Required IF-ELSE Conditions
- **Condition:** If `doctor == null` return `-1`; else delete children then parent and return total count.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `-1` |
| Cascaded deletion | Total deleted record count >= 1 |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
