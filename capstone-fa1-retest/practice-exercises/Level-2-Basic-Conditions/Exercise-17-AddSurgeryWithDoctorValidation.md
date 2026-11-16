# Exercise 17: AddSurgeryWithDoctorValidation

## Business Scenario
Operating theatre nurses book surgeries into the system. Before creating a surgery record directly through Entity Framework, the system must check that the assigned surgeon exists in the hospital registry.

## Problem Statement
Write a repository method `AddSurgeryWithDoctorValidation` accepting `doctorId`, `surgeryDate`, `startTime`, `endTime`, and `surgeryCategory`. Validate if `doctorId` exists in `Doctor`. If not found, return `false`. If found, instantiate a `Surgery` entity, set all five properties, add to `Surgeries` DbSet, execute `SaveChanges()`, and return `true`. Return `false` on any exception.

## DAL / Repository Method Signature
```csharp
public bool AddSurgeryWithDoctorValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` accepting parameters, calling repository, and returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithDoctorValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryDate | DateTime | Surgery date |
| startTime | decimal | Start time (e.g., 9.00) |
| endTime | decimal | End time (e.g., 12.00) |
| surgeryCategory | string | 3-character specialization code |

## Relevant Database Tables
- **Doctor**: DoctorID (PK)
- **Surgery**: SurgeryID (PK, IDENTITY), DoctorID (FK), SurgeryDate, StartTime, EndTime, SurgeryCategory (FK)

## Entity Relationships
- `Surgery` has foreign key `DoctorID` referencing `Doctor(DoctorID)`.

## Required Operations
1. Check doctor existence via `context.Doctors.Find(doctorId)`.
2. If null, return `false`.
3. Instantiate `Surgery` object and assign individual properties.
4. Add to `context.Surgeries` and call `SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Doctor must exist in database.

## Required IF-ELSE Conditions
- **Condition:** If `doc == null`, return `false`. Else insert and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor does not exist | `false` |
| Surgery successfully inserted | `true` |
| Exception occurred | `false` |

## Exception Handling Requirements
Try-catch block returning `false` on error.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
