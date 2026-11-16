# Exercise 23: UpdateSurgeryDateAndTime

## Business Scenario
Operating rooms at CureHospital frequently reschedule surgical appointments due to patient preparation delays. Hospital administrators need to update the surgery date, start time, and end time in a single operation.

## Problem Statement
Write a repository method `UpdateSurgeryDateAndTime` that accepts `surgeryId`, `newDate`, `newStartTime`, and `newEndTime`. Locate the surgery using `context.Surgeries.Find(surgeryId)`. If not found, return `false`. If found, update `SurgeryDate`, `StartTime`, and `EndTime`. Save changes and return `true`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool UpdateSurgeryDateAndTime(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` accepting these four parameters and returning `JsonResult` wrapping the boolean status.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateSurgeryDateAndTime(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery identifier |
| newDate | DateTime | New surgery date |
| newStartTime | decimal | New start time |
| newEndTime | decimal | New end time |

## Relevant Database Tables
- **Surgery**: SurgeryID (PK), DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- Direct update on `Surgery` table.

## Required Operations
1. Find surgery using `context.Surgeries.Find(surgeryId)`.
2. Check if null; return `false` if not found.
3. Update `surg.SurgeryDate = newDate; surg.StartTime = newStartTime; surg.EndTime = newEndTime;`.
4. Call `context.SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Target surgery must exist.
2. All three properties must be updated.

## Required IF-ELSE Conditions
- **Condition:** If `surgery == null` return `false`; else update and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `false` |
| Successfully updated | `true` |
| Exception occurred | `false` |

## Exception Handling Requirements
Try-catch block returning `false` on exception.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
