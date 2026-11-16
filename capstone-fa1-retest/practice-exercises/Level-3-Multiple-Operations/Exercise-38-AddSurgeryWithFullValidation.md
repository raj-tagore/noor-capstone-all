# Exercise 38: AddSurgeryWithFullValidation

## Business Scenario
In scenarios where stored procedures are not utilized, the entire business validation pipeline for adding surgeries must be enforced directly within the Entity Framework repository layer.

## Problem Statement
Write a repository method `AddSurgeryWithFullValidation` taking `doctorId`, `surgeryCategory`, `surgeryDate`, `startTime`, and `endTime`.
Execute the following sequential validations via EF Core and LINQ Query Syntax:
1. Check if `doctorId` exists in `Doctor`. If not, return `-1`.
2. Check if `surgeryCategory` exists in `Specialization`. If not, return `-2`.
3. Check if doctor has that category in `DoctorSpecialization`. If not, return `-3`.
4. Check if `startTime < endTime`. If `startTime >= endTime`, return `-4`.
5. Check if `surgeryDate < DateTime.Today`. If date is in the past, return `-6`.
6. If all pass, create `Surgery` entity, add to DbSet, call `SaveChanges()`, and return `1`.
7. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryWithFullValidation(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the integer code.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithFullValidation(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| surgeryCategory | string | Specialization code |
| surgeryDate | DateTime | Scheduled date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |

## Relevant Database Tables
- **Doctor**: DoctorID
- **Specialization**: SpecializationCode
- **DoctorSpecialization**: DoctorID, SpecializationCode
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- Full integrity check across all four tables.

## Required Operations
1. Verify doctor existence (-1).
2. Verify category existence (-2).
3. Verify doctor holds category (-3).
4. Verify start time < end time (-4).
5. Verify date is not in past (-6).
6. Create entity, add, save, return 1.

## Business Rules and Validations
1. Doctor exists (-1).
2. Category exists (-2).
3. Credential exists (-3).
4. Timing valid (-4).
5. Future date (-6).

## Required IF-ELSE Conditions
- Ordered sequence of 5 validation if-statements.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor missing | `-1` |
| Specialization missing | `-2` |
| Mismatched credentials | `-3` |
| Start time >= End time | `-4` |
| Past surgery date | `-6` |
| Successfully inserted | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
