# Exercise 05: AddNewSpecialization

## Business Scenario
CureHospital is expanding its medical departments and needs to register new specializations in the system. When the hospital adds a new department — for example, Neurology or Orthopedics — an administrator must enter both the specialization code and its full name into the database so that doctors can later be linked to these specializations.

## Problem Statement
Write a repository method named `AddNewSpecialization` that accepts a `specializationCode` (string) and a `specializationName` (string) as parameters, and inserts a new Specialization record into the database. The method should create a new `Specialization` entity object, assign the `specializationCode` to the `SpecializationCode` property and the `specializationName` to the `SpecializationName` property individually (do not use an object initializer that takes the entire object). Add the entity to the `Specializations` DbSet using `context.Specializations.Add()` and call `SaveChanges()`. Return `true` on success or `false` if any exception occurs. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public bool AddNewSpecialization(string specializationCode, string specializationName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action method named `AddNewSpecialization` in `CureHospitalController`. It should accept the specialization details from the **request body** as a `Specialization` object (using `[FromBody]`). The controller should extract `SpecializationCode` and `SpecializationName` from the received object and pass them as individual parameters to the repository method. Return the boolean result wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddNewSpecialization([FromBody] Specialization specialization)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | A 3-character code for the specialization (e.g., "NEU", "ORT") |
| specializationName | string | The full name of the specialization (e.g., "Neurologist", "Orthopedic") |

## Relevant Database Tables
- **Specialization**: SpecializationCode (CHAR(3), PK), SpecializationName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Specialization` is inserted as a standalone entity. No related tables are affected in this exercise.

## Required Operations
1. Create a new `Specialization` entity object.
2. Assign the `specializationCode` parameter to the `SpecializationCode` property.
3. Assign the `specializationName` parameter to the `SpecializationName` property.
4. Add the new Specialization object to the `Specializations` DbSet using `context.Specializations.Add()`.
5. Call `context.SaveChanges()` to persist the new record.
6. Return `true` indicating success.

## Business Rules and Validations
1. `SpecializationCode` is the primary key (CHAR(3)) — it must be exactly 3 characters.
2. `SpecializationName` has a UNIQUE constraint. Duplicate names will cause `SaveChanges()` to throw an exception.
3. `SpecializationCode` also has a UNIQUE/PK constraint. Duplicate codes will cause an exception.
4. Properties should be assigned individually (not by passing the entire object directly to `Add()`).

## Required IF-ELSE Conditions
None. The method follows a straight-through flow: create → assign properties → add → save → return `true`. The `catch` block handles failures.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specialization is successfully added to the database | `true` |
| An exception occurs (e.g., duplicate code, duplicate name, constraint violation) | `false` |

## Exception Handling Requirements
- Wrap the create, assign, add, and save operations inside a `try-catch` block.
- In the `catch` block, return `false`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `AddNewSpecialization` in `CureHospitalRepositoryEF`
- [ ] Controller method `AddNewSpecialization` in `CureHospitalController`
