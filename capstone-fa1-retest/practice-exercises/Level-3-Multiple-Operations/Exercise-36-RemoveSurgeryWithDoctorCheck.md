# Exercise 36: RemoveSurgeryWithDoctorCheck

## Business Scenario
Before cancelling an individual surgery, the system verifies that both the surgery record and its parent doctor account are currently valid in the system.

## Problem Statement
Write a repository method `RemoveSurgeryWithDoctorCheck` accepting `surgeryId`.
1. Locate surgery by `surgeryId` using `Find()`. If not found, return `-1`.
2. Locate the associated doctor in `Doctor` table using `surgery.DoctorId`. If doctor is null, return `-2`.
3. If both exist, remove the surgery using `context.Surgeries.Remove(surgery)`, save changes, and return `1`.
4. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int RemoveSurgeryWithDoctorCheck(int surgeryId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult RemoveSurgeryWithDoctorCheck(int surgeryId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Surgery ID to cancel |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID
- **Doctor**: DoctorID

## Entity Relationships
- `Surgery` to `Doctor`.

## Required Operations
1. Find surgery via `context.Surgeries.Find(surgeryId)`. If null, return `-1`.
2. Find doctor via `context.Doctors.Find(surgery.DoctorId)`. If null, return `-2`.
3. Remove surgery, call `SaveChanges()`, and return `1`.

## Business Rules and Validations
1. Surgery must exist (-1).
2. Parent doctor must exist (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If surgery is null -> return -1.
- **Condition 2:** Else if doctor is null -> return -2.
- **Condition 3:** Else remove and return 1.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Doctor not found | `-2` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
