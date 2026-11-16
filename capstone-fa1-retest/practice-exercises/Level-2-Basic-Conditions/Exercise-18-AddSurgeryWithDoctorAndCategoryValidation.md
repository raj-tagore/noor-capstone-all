# Exercise 18: AddSurgeryWithDoctorAndCategoryValidation

## Business Scenario
Scheduling a surgery requires verifying both the attending physician and the clinical category code under which the procedure falls.

## Problem Statement
Implement repository method `AddSurgeryWithDoctorAndCategoryValidation` accepting surgery scheduling details.
Validations:
- If DoctorID does not exist in `Doctor`, return `-1`.
- If SurgeryCategory does not exist in `Specialization`, return `-2`.
- If both exist, create and insert `Surgery`, call `SaveChanges()`, and return status `1`.
- On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryWithDoctorAndCategoryValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` endpoint returning `JsonResult` with the status code integer.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithDoctorAndCategoryValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
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
- **Specialization**: SpecializationCode (PK)
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- `Surgery` references `Doctor` via `DoctorID` and `Specialization` via `SurgeryCategory`.

## Required Operations
1. Verify doctor via `Find(doctorId)`; return `-1` if null.
2. Verify specialization via `Find(surgeryCategory)`; return `-2` if null.
3. Construct `Surgery`, set properties, call `Add()` and `SaveChanges()`.
4. Return `1`.

## Business Rules and Validations
1. Doctor existence (-1).
2. Category existence (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If doc == null -> return -1.
- **Condition 2:** Else if spec == null -> return -2.
- **Condition 3:** Else insert -> return 1.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `-1` |
| Category not found | `-2` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99` on catch.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
