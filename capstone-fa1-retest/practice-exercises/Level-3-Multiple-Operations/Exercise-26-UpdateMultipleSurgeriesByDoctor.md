# Exercise 26: UpdateMultipleSurgeriesByDoctor

## Business Scenario
When a surgeon transitions to a sub-specialty division within the hospital, all existing surgeries associated with that surgeon need their category updated to the new department code.

## Problem Statement
Write a repository method `UpdateMultipleSurgeriesByDoctor` that accepts `doctorId` and `newSurgeryCategory`.
Using LINQ Query Syntax, fetch all surgeries for the doctor:
`from s in context.Surgeries where s.DoctorId == doctorId select s`
If no surgeries are found, return `0`.
If surgeries are found, iterate through the list using a `foreach` loop, update `s.SurgeryCategory = newSurgeryCategory` for each record, call `SaveChanges()`, and return the count of updated records. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateMultipleSurgeriesByDoctor(int doctorId, string newSurgeryCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` with the count of updated surgeries.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateMultipleSurgeriesByDoctor(int doctorId, string newSurgeryCategory)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target surgeon ID |
| newSurgeryCategory | string | Revised specialization category |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID, SurgeryCategory

## Entity Relationships
- Surgeries filtered by `DoctorID`.

## Required Operations
1. Execute LINQ Query Syntax to retrieve `List<Surgery>`.
2. Check if the list is empty (`surgeries.Count == 0`). If so, return `0`.
3. Iterate with `foreach (var surgery in surgeries)` and assign `surgery.SurgeryCategory = newSurgeryCategory`.
4. Call `context.SaveChanges()`.
5. Return `surgeries.Count`.

## Business Rules and Validations
1. Only matching surgeries updated.
2. Returns exact count of updated records.

## Required IF-ELSE Conditions
- **Condition:** If `surgeries == null || surgeries.Count == 0` return `0`; else loop, save, and return count.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| No surgeries found | `0` |
| Surgeries updated | Count of updated surgeries (e.g., 2, 5) |
| Exception occurred | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
