# Exercise 35: RemoveSurgeriesByDoctorId

## Business Scenario
When a visiting consultant completes their rotation, all pending surgeries scheduled under their ID are cancelled in batch.

## Problem Statement
Write a repository method `RemoveSurgeriesByDoctorId` accepting `doctorId`.
Retrieve all surgeries assigned to `doctorId` using LINQ Query Syntax.
If no records are found, return `0`.
If records exist, use `context.Surgeries.RemoveRange(surgeries)` to delete them in batch, call `SaveChanges()`, and return the count of deleted surgeries. Return `-99` on exception.

## DAL / Repository Method Signature
```csharp
public int RemoveSurgeriesByDoctorId(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action in `CureHospitalController` returning `JsonResult` with the count of deleted records.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult RemoveSurgeriesByDoctorId(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target surgeon ID |

## Relevant Database Tables
- **Surgery**: SurgeryID, DoctorID

## Entity Relationships
- Surgeries related to `Doctor`.

## Required Operations
1. Query surgeries for doctor via LINQ Query Syntax.
2. Check if count == 0; return `0`.
3. Call `context.Surgeries.RemoveRange(surgeries)`.
4. Save changes and return count.

## Business Rules and Validations
1. Uses `RemoveRange()` for batch deletion.

## Required IF-ELSE Conditions
- **Condition:** If list is empty return 0; else delete and return count.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| No surgeries found | `0` |
| Records removed | Count of deleted records |
| Exception caught | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
