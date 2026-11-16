# Exercise 10: DeleteSpecialization

## Business Scenario
CureHospital periodically reviews its list of medical specializations. If a particular specialization department is being dissolved or merged with another, the administrator needs to remove the specialization record from the system. However, a specialization can only be deleted if no doctors are currently assigned to it — otherwise the system must prevent the deletion to maintain data integrity.

## Problem Statement
Write a repository method named `DeleteSpecialization` that accepts a `specializationCode` (string) as a parameter and deletes the corresponding Specialization record from the database. The method should use `Find()` to locate the specialization by its primary key. If the specialization is found, use `context.Specializations.Remove()` to mark it for deletion and call `SaveChanges()`. If the specialization is **not found**, return `false`. Return `true` on successful deletion, or `false` if the specialization was not found or if any exception occurs. **Important**: If the specialization is referenced by records in the `DoctorSpecializations` table (i.e., doctors are assigned to this specialization) or in the `Surgeries` table (i.e., surgeries exist under this category), `SaveChanges()` will throw a foreign key constraint violation exception, which must be caught in the `catch` block. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public bool DeleteSpecialization(string specializationCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpDelete]` action method named `DeleteSpecialization` in `CureHospitalController`. It should accept `specializationCode` as a **query parameter**. The controller method should call the repository's `DeleteSpecialization` method and return the boolean result wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpDelete]
public JsonResult DeleteSpecialization(string specializationCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | The 3-character specialization code to delete (e.g., "GYN", "CAR") |

## Relevant Database Tables
- **Specialization**: SpecializationCode (CHAR(3), PK), SpecializationName (VARCHAR(50), NOT NULL, UNIQUE)
- **DoctorSpecialization**: DoctorID (INT, FK → Doctor), SpecializationCode (CHAR(3), FK → Specialization) — may block deletion
- **Surgery**: SurgeryCategory (CHAR(3), FK → Specialization) — may block deletion

## Entity Relationships
- `Specialization.SpecializationCode` is referenced as a foreign key in the `DoctorSpecialization` table (via `SpecializationCode` column).
- `Specialization.SpecializationCode` is also referenced as a foreign key in the `Surgery` table (via `SurgeryCategory` column).
- Attempting to delete a Specialization that has existing records in `DoctorSpecialization` or `Surgery` will cause a **foreign key constraint violation** exception.

## Required Operations
1. Use `Find()` on the `Specializations` DbSet to locate the Specialization record by `specializationCode`.
2. Check if the returned Specialization entity is `null`.
3. If found, use `context.Specializations.Remove()` to mark the entity for deletion.
4. Call `context.SaveChanges()` to persist the deletion.
5. Return the appropriate boolean result.

## Business Rules and Validations
1. The specialization must exist in the database before deletion can be attempted.
2. If `Find()` returns `null`, the method must return `false` without calling `Remove()` or `SaveChanges()`.
3. If the specialization has related records in `DoctorSpecialization` or `Surgery` tables, the database will reject the deletion with a foreign key constraint violation. This exception must be handled gracefully.
4. Only specializations with **no references** in any related tables can be successfully deleted.

## Required IF-ELSE Conditions
1. **IF** `Find()` returns a valid Specialization entity → call `Remove()`, then `SaveChanges()`, and return `true`.
2. **ELSE** (Specialization not found / `null`) → return `false`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specialization found and successfully deleted | `true` |
| Specialization not found (Find returns null) | `false` |
| An exception occurs (e.g., FK constraint violation because doctors or surgeries reference this specialization) | `false` |

## Exception Handling Requirements
- Wrap the entire find-check-remove-save logic inside a `try-catch` block.
- In the `catch` block, return `false`.
- The `catch` should handle a general `Exception` to cover foreign key constraint violations (`SqlException`) and other potential errors.
- **Key point**: The FK constraint failure from `DoctorSpecialization` or `Surgery` references is the most common exception scenario for this exercise.

## What Needs to Be Implemented
- [ ] Repository method `DeleteSpecialization` in `CureHospitalRepositoryEF`
- [ ] Controller method `DeleteSpecialization` in `CureHospitalController`
