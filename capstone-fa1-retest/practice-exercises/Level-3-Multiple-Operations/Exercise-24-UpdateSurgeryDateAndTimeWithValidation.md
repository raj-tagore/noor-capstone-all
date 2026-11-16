# Exercise 24: UpdateSurgeryDateAndTimeWithValidation

## Business Scenario
When rescheduling surgery timings, clinical guidelines mandate that start time must strictly precede end time.

## Problem Statement
Write a repository method `UpdateSurgeryDateAndTimeWithValidation` accepting `surgeryId`, `newDate`, `newStartTime`, and `newEndTime`.
Validations:
- If `newStartTime >= newEndTime`, return status code `-1`.
- Locate the surgery by `surgeryId`. If null, return `-2`.
- If valid, update `SurgeryDate`, `StartTime`, and `EndTime`, call `SaveChanges()`, and return status `1`.
- On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateSurgeryDateAndTimeWithValidation(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` with the integer status code.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateSurgeryDateAndTimeWithValidation(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery identifier |
| newDate | DateTime | Rescheduled date |
| newStartTime | decimal | Rescheduled start time |
| newEndTime | decimal | Rescheduled end time |

## Relevant Database Tables
- **Surgery**: SurgeryID (PK), SurgeryDate, StartTime, EndTime

## Entity Relationships
- Direct update on `Surgery`.

## Required Operations
1. Check if `newStartTime >= newEndTime`; if true return `-1`.
2. Locate surgery via `context.Surgeries.Find(surgeryId)`. If null, return `-2`.
3. Update date, start time, and end time.
4. Call `context.SaveChanges()` and return `1`.

## Business Rules and Validations
1. Time validation: Start must be strictly less than End (-1).
2. Existence validation: Surgery must exist (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If `newStartTime >= newEndTime` return `-1`.
- **Condition 2:** Else if `surgery == null` return `-2`.
- **Condition 3:** Else update and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Start time >= End time | `-1` |
| Surgery not found | `-2` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
