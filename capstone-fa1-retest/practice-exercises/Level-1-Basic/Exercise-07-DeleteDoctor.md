# Exercise 07: DeleteDoctor

## Business Scenario
When a doctor permanently leaves CureHospital — due to retirement, resignation, or transfer to another facility — the administration needs to remove their record from the system. Before deleting, the system must verify that the doctor exists. Note that deletion may fail if the doctor has existing references in related tables such as surgeries or specialization assignments.

## Problem Statement
Write a repository method named `DeleteDoctor` that accepts a `doctorId` (int) as a parameter and deletes the corresponding Doctor record from the database. The method should use `Find()` to locate the doctor by their primary key. If the doctor is found, use `context.Doctors.Remove()` to mark the entity for deletion and call `SaveChanges()` to persist the removal. If the doctor is **not found**, return `false`. Return `true` on successful deletion, or `false` if the doctor was not found or if any exception occurs (e.g., foreign key constraint violations from related `DoctorSpecialization` or `Surgery` records). Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public bool DeleteDoctor(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action method named `DeleteDoctor` in `CureHospitalController`. It should accept `doctorId` as a **query parameter**. The controller method should call the repository's `DeleteDoctor` method and return the boolean result wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult DeleteDoctor(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | The unique identifier of the doctor to delete (e.g., 1001) |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK, IDENTITY(1001,1)), DoctorName (VARCHAR(50), NOT NULL, UNIQUE)
- **DoctorSpecialization**: DoctorID (INT, FK → Doctor), SpecializationCode (CHAR(3), FK → Specialization) — may block deletion
- **Surgery**: DoctorID (INT, FK → Doctor) — may block deletion

## Entity Relationships
- `Doctor.DoctorID` is referenced as a foreign key in both `DoctorSpecialization` and `Surgery` tables.
- Attempting to delete a Doctor who has existing records in `DoctorSpecialization` or `Surgery` will cause a foreign key constraint violation, which should be caught by the exception handler.

## Required Operations
1. Use `Find()` on the `Doctors` DbSet to locate the Doctor record by `doctorId`.
2. Check if the returned Doctor entity is `null`.
3. If found, use `context.Doctors.Remove()` to mark the entity for deletion.
4. Call `context.SaveChanges()` to persist the deletion.
5. Return the appropriate boolean result.

## Business Rules and Validations
1. The doctor must exist in the database before deletion can be attempted.
2. If `Find()` returns `null`, the method must return `false` without calling `Remove()` or `SaveChanges()`.
3. If the doctor has related records in `DoctorSpecialization` or `Surgery` tables, `SaveChanges()` will throw a foreign key constraint exception, which must be caught.

## Required IF-ELSE Conditions
1. **IF** `Find()` returns a valid Doctor entity → call `Remove()`, then `SaveChanges()`, and return `true`.
2. **ELSE** (Doctor not found / `null`) → return `false`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor found and successfully deleted | `true` |
| Doctor not found (Find returns null) | `false` |
| An exception occurs (e.g., FK constraint violation because doctor has surgeries or specializations) | `false` |

## Exception Handling Requirements
- Wrap the entire find-check-remove-save logic inside a `try-catch` block.
- In the `catch` block, return `false`.
- The `catch` should handle a general `Exception` to cover both FK constraint violations and other potential errors.

## What Needs to Be Implemented
- [ ] Repository method `DeleteDoctor` in `CureHospitalRepositoryEF`
- [ ] Controller method `DeleteDoctor` in `CureHospitalController`
