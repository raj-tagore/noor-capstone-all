# Exercise 78: ValidateAndAddSurgery

## Business Scenario
A high-throughput surgical unit enforces all database procedure rules in C# and adds an additional hospital policy restriction: no surgeon may perform more than 3 surgeries on any single calendar date.

## Problem Statement
Write a repository method `ValidateAndAddSurgery` taking `doctorId`, `surgeryCategory`, `surgeryDate`, `startTime`, and `endTime`.
Execute in C# using EF Core:
1. Verify doctor exists in `Doctor`. If not, return `-1`.
2. Verify category exists in `Specialization`. If not, return `-2`.
3. Verify doctor holds category in `DoctorSpecialization`. If not, return `-3`.
4. Verify `startTime < endTime`. If not, return `-4`.
5. Check for time overlap with doctor's existing surgeries on that date (`s.StartTime < endTime && s.EndTime > startTime`). If overlap, return `-5`.
6. Verify `surgeryDate >= DateTime.Today`. If in the past, return `-6`.
7. **Daily cap rule:** Count existing surgeries for that doctor on `surgeryDate`. If count >= 3, return `-7` (daily maximum surgeries reached).
8. If all pass, create `Surgery`, add to DbSet, call `SaveChanges()`, and return `1`.
9. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int ValidateAndAddSurgery(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the status code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult ValidateAndAddSurgery(Models.Surgery surgery)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryCategory | string | Specialty code |
| surgeryDate | DateTime | Date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |

## Relevant Database Tables
- **Doctor**: DoctorID
- **Specialization**: SpecializationCode
- **DoctorSpecialization**: DoctorID, SpecializationCode
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- Cross-table validation and constraint evaluation.

## Required Operations
1. Sequential checks (-1 through -7).
2. Insert surgery.
3. Save changes and return 1.

## Business Rules and Validations
1. Complete validation chain including max 3 surgeries per day.

## Required IF-ELSE Conditions
- 7 distinct validation conditions.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `-1` |
| Category not found | `-2` |
| Doctor not certified | `-3` |
| Start time >= End time | `-4` |
| Overlap detected | `-5` |
| Past date | `-6` |
| Max surgeries exceeded | `-7` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
