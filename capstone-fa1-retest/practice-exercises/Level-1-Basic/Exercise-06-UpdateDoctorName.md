# Exercise 06: UpdateDoctorName

## Business Scenario
Occasionally, a doctor's name needs to be corrected in the CureHospital system — for instance, due to a data entry error during initial registration or a legal name change. The administration team requires a feature to update a doctor's name by specifying their Doctor ID and the new name.

## Problem Statement
Write a repository method named `UpdateDoctorName` that accepts a `doctorId` (int) and a `newDoctorName` (string) as parameters, and updates the corresponding Doctor record in the database. The method should use `Find()` to locate the doctor by their primary key. If the doctor is found, update the `DoctorName` property with the new value and call `SaveChanges()`. If the doctor is **not found** (i.e., `Find()` returns `null`), return `false` without making any changes. Return `true` on successful update, or `false` if the doctor was not found or if any exception occurs. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public bool UpdateDoctorName(int doctorId, string newDoctorName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action method named `UpdateDoctorName` in `CureHospitalController`. It should accept `doctorId` and `newDoctorName` as **query parameters**. The controller method should call the repository's `UpdateDoctorName` method and return the boolean result wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateDoctorName(int doctorId, string newDoctorName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | The unique identifier of the doctor to update (e.g., 1001) |
| newDoctorName | string | The new name to assign to the doctor (e.g., "Robert") |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK, IDENTITY(1001,1)), DoctorName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- Only the `Doctor` table is involved. No related tables are modified in this exercise.

## Required Operations
1. Use `Find()` on the `Doctors` DbSet to locate the Doctor record by `doctorId`.
2. Check if the returned Doctor entity is `null`.
3. If found, assign `newDoctorName` to the `DoctorName` property.
4. Call `context.SaveChanges()` to persist the update.
5. Return the appropriate boolean result.

## Business Rules and Validations
1. The doctor must exist in the database before an update can be performed.
2. `DoctorName` has a UNIQUE constraint — if the new name already belongs to another doctor, `SaveChanges()` will throw an exception.
3. If `Find()` returns `null`, the method must return `false` without calling `SaveChanges()`.

## Required IF-ELSE Conditions
1. **IF** `Find()` returns a valid Doctor entity → update the `DoctorName` property, call `SaveChanges()`, and return `true`.
2. **ELSE** (Doctor not found / `null`) → return `false`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor found and name updated successfully | `true` |
| Doctor not found (Find returns null) | `false` |
| An exception occurs (e.g., duplicate name constraint violation) | `false` |

## Exception Handling Requirements
- Wrap the entire find-check-update-save logic inside a `try-catch` block.
- In the `catch` block, return `false`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `UpdateDoctorName` in `CureHospitalRepositoryEF`
- [ ] Controller method `UpdateDoctorName` in `CureHospitalController`
