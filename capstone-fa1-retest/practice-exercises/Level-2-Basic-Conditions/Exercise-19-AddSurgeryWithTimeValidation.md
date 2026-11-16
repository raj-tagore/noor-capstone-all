# Exercise 19: AddSurgeryWithTimeValidation

## Business Scenario
Operating theatre time management requires that surgery scheduled start time strictly precedes end time.

## Problem Statement
Write a repository method `AddSurgeryWithTimeValidation` that accepts surgery parameters.
Rules:
1. Verify doctor exists in `Doctor`. If not, return `-1`.
2. Check if `startTime < endTime`. If `startTime >= endTime`, return `-2`.
3. If both pass, add the surgery entity, save changes, and return `1`.
4. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryWithTimeValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the integer code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithTimeValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryDate | DateTime | Date of surgery |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |
| surgeryCategory | string | Specialization code |

## Relevant Database Tables
- **Doctor**: DoctorID (PK)
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- `Surgery` references `Doctor(DoctorID)`.

## Required Operations
1. Locate doctor using `context.Doctors.Find(doctorId)`. If null, return `-1`.
2. Compare times: if `startTime >= endTime`, return `-2`.
3. Create `Surgery` entity, assign values, add, save changes.
4. Return `1`.

## Business Rules and Validations
1. Doctor must exist (-1).
2. Start time must be less than end time (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If `doc == null` return `-1`.
- **Condition 2:** Else if `startTime >= endTime` return `-2`.
- **Condition 3:** Else insert and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor does not exist | `-1` |
| Start time >= end time | `-2` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
