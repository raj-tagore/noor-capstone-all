# Exercise 25: UpdateSurgeryAllProperties

## Business Scenario
A complete reassignment of a surgery may occur when a patient is transferred to another specialty department under a new lead surgeon.

## Problem Statement
Write a repository method `UpdateSurgeryAllProperties` accepting `surgeryId`, `newDoctorId`, `newDate`, `newStartTime`, `newEndTime`, and `newCategory`.
Validate:
1. Surgery exists (return `-1` if not found).
2. Doctor exists (return `-2` if not found).
3. Category exists in `Specialization` table (return `-3` if not found).
If all exist, update all five properties of the surgery, save changes, and return `1`. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateSurgeryAllProperties(int surgeryId, int newDoctorId, DateTime newDate, decimal newStartTime, decimal newEndTime, string newCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateSurgeryAllProperties(int surgeryId, int newDoctorId, DateTime newDate, decimal newStartTime, decimal newEndTime, string newCategory)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery ID |
| newDoctorId | int | New doctor ID |
| newDate | DateTime | New date |
| newStartTime | decimal | New start time |
| newEndTime | decimal | New end time |
| newCategory | string | New specialization code |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory
- **Doctor**: DoctorID
- **Specialization**: SpecializationCode

## Entity Relationships
- References `Doctor` and `Specialization`.

## Required Operations
1. Find surgery using `Find(surgeryId)`. If null, return `-1`.
2. Find doctor using `context.Doctors.Find(newDoctorId)`. If null, return `-2`.
3. Find specialization using `context.Specializations.Find(newCategory)`. If null, return `-3`.
4. Update surgery fields: DoctorId, SurgeryDate, StartTime, EndTime, SurgeryCategory.
5. Save changes and return `1`.

## Business Rules and Validations
1. Surgery must exist (-1).
2. Doctor must exist (-2).
3. Category must exist (-3).

## Required IF-ELSE Conditions
- **Condition 1:** If surgery is null -> `-1`.
- **Condition 2:** Else if doctor is null -> `-2`.
- **Condition 3:** Else if category is null -> `-3`.
- **Condition 4:** Else update all and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Doctor not found | `-2` |
| Category not found | `-3` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
