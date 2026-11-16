# Exercise 30: ReassignSurgeryToNewDoctor

## Business Scenario
When a surgeon becomes unavailable due to an emergency, a scheduled surgery must be reassigned to a substitute doctor. This requires two separate database lookups: one to locate the target surgery, and one to verify the substitute doctor.

## Problem Statement
Write a repository method `ReassignSurgeryToNewDoctor` accepting `surgeryId` and `newDoctorId`.
Pattern — Two LINQ Queries:
1. Query 1: Find the surgery using `from s in context.Surgeries where s.SurgeryId == surgeryId select s` (with `FirstOrDefault()`). If not found, return `-1`.
2. Query 2: Find the new doctor using `from d in context.Doctors where d.DoctorId == newDoctorId select d` (with `FirstOrDefault()`). If not found, return `-2`.
3. If both exist, reassign `surgery.DoctorId = newDoctorId`, call `SaveChanges()`, and return `1`.
4. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the status code.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery to reassign |
| newDoctorId | int | Substitute doctor ID |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID
- **Doctor**: DoctorID

## Entity Relationships
- `Surgery.DoctorID` foreign key to `Doctor.DoctorID`.

## Required Operations
1. Execute Query 1 to find `Surgery`.
2. Check if surgery is null -> return `-1`.
3. Execute Query 2 to find `Doctor`.
4. Check if doctor is null -> return `-2`.
5. Update `surgery.DoctorId = newDoctorId`.
6. Call `context.SaveChanges()`.
7. Return `1`.

## Business Rules and Validations
1. Surgery must exist (-1).
2. Substitute doctor must exist (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If `surgery == null` return `-1`.
- **Condition 2:** Else if `doctor == null` return `-2`.
- **Condition 3:** Else update and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Doctor not found | `-2` |
| Reassigned successfully | `1` |
| Exception occurred | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
