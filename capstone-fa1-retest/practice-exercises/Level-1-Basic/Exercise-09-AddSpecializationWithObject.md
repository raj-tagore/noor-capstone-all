# Exercise 09: AddSpecializationWithObject

## Business Scenario
As CureHospital continues to grow, the IT department is building a streamlined interface where administrators can submit complete specialization details through a single form. The system receives the entire specialization entity as a JSON payload from the front-end and should persist it directly to the database. This approach simplifies the controller by accepting a pre-built entity object rather than individual fields.

## Problem Statement
Write a repository method named `AddSpecializationWithObject` that accepts a `Specialization` entity object as a parameter and inserts it directly into the database. Unlike Exercise 05 (which accepts individual parameters), this method receives the complete entity object and adds it to the `Specializations` DbSet using `context.Specializations.Add()`, then calls `SaveChanges()`. Return `true` on success or `false` if any exception occurs. Expose this method through a Web API controller action that accepts the specialization as a JSON body.

## DAL / Repository Method Signature
```csharp
public bool AddSpecializationWithObject(Specialization specialization)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action method named `AddSpecializationWithObject` in `CureHospitalController`. It should accept a `Specialization` object from the **request body** using the `[FromBody]` attribute. The controller should pass the received object directly to the repository method. Return the boolean result wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSpecializationWithObject([FromBody] Specialization specialization)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specialization | Specialization (entity object) | A complete Specialization entity containing SpecializationCode and SpecializationName |

### Properties of the Specialization object:
| Property | Type | Description |
|----------|------|-------------|
| SpecializationCode | string (CHAR(3)) | A 3-character code (e.g., "NEU", "ORT") |
| SpecializationName | string (VARCHAR(50)) | The full specialization name (e.g., "Neurologist") |

## Relevant Database Tables
- **Specialization**: SpecializationCode (CHAR(3), PK), SpecializationName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Specialization` is inserted as a standalone entity. No related tables are affected in this exercise.

## Required Operations
1. Accept the `Specialization` entity object as a parameter.
2. Add the object to the `Specializations` DbSet using `context.Specializations.Add()`.
3. Call `context.SaveChanges()` to persist the new record.
4. Return `true` indicating success.

## Business Rules and Validations
1. The entire `Specialization` object is passed directly to `Add()` — no need to create a new object or assign properties individually.
2. `SpecializationCode` is the primary key (CHAR(3)) — duplicate codes will cause a constraint violation.
3. `SpecializationName` has a UNIQUE constraint — duplicate names will cause a constraint violation.
4. Both constraints are enforced by the database; exceptions from `SaveChanges()` should be caught.

## Required IF-ELSE Conditions
None. The method follows a straight-through flow: add → save → return `true`. The `catch` block handles failures.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specialization is successfully added to the database | `true` |
| An exception occurs (e.g., duplicate code, duplicate name, null values) | `false` |

## Exception Handling Requirements
- Wrap the add and save operations inside a `try-catch` block.
- In the `catch` block, return `false`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `AddSpecializationWithObject` in `CureHospitalRepositoryEF`
- [ ] Controller method `AddSpecializationWithObject` in `CureHospitalController`
