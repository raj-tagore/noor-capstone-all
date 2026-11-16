# Exercise 75: RescheduleSurgery

## Business Scenario
Rescheduling an existing surgery demands checking for conflict against all other surgeries booked for the same surgeon on the new date, excluding the surgery being modified.

## Problem Statement
Write a repository method `RescheduleSurgery` taking `surgeryId`, `newDate`, `newStartTime`, and `newEndTime`.
Validations:
1. Find surgery by `surgeryId`. If null, return `-1`.
2. Check if `newDate < DateTime.Today`. If in the past, return `-2`.
3. Check if `newStartTime >= newEndTime`. If invalid, return `-3`.
4. Overlap check: Using LINQ Query Syntax, check if the surgeon already has another surgery on `newDate` that overlaps with `[newStartTime, newEndTime]`:
```csharp
from s in context.Surgeries
where s.DoctorId == surgery.DoctorId
   && s.SurgeryDate == newDate
   && s.SurgeryId != surgeryId
   && s.StartTime < newEndTime
   && s.EndTime > newStartTime
select s
```
If any overlapping record exists (`FirstOrDefault() != null`), return `-4`.
5. If all validations pass, update `surgery.SurgeryDate = newDate; surgery.StartTime = newStartTime; surgery.EndTime = newEndTime;`.
6. Save changes and return `1`.
7. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int RescheduleSurgery(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the status code.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult RescheduleSurgery(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery |
| newDate | DateTime | New date |
| newStartTime | decimal | New start hour |
| newEndTime | decimal | New end hour |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime

## Entity Relationships
- Cross-record integrity check on `Surgery`.

## Required Operations
1. Locate surgery (-1).
2. Validate future date (-2).
3. Validate time ordering (-3).
4. Check overlap excluding current surgery (-4).
5. Update and save changes (1).

## Business Rules and Validations
1. Overlap logic: `existing.StartTime < newEndTime && existing.EndTime > newStartTime`.

## Required IF-ELSE Conditions
- Ordered 4-step validation logic.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Past date | `-2` |
| Start time >= End time | `-3` |
| Overlapping booking | `-4` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
