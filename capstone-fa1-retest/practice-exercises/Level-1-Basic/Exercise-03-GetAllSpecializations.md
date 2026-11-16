# Exercise 03: GetAllSpecializations

## Business Scenario
When scheduling a new surgery or assigning a specialization to a doctor, the CureHospital system needs to present a complete list of available medical specializations. The front-end application uses this list to populate dropdown menus so that users can select from valid specializations such as Cardiologist, Gynecologist, or Anesthesiologist.

## Problem Statement
Write a repository method named `GetAllSpecializations` that retrieves all specialization records from the `Specializations` table and returns them as a `List<Specialization>`. You **must** use LINQ **Query Syntax** (i.e., `from ... in ... select ...`) to fetch the records. Method Syntax is **not permitted**. If any exception occurs, the method should return `null`. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public List<Specialization> GetAllSpecializations()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action method named `GetAllSpecializations` in `CureHospitalController`. This method takes no parameters. It should call the repository's `GetAllSpecializations` method and return the resulting list wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetAllSpecializations()
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| *(none)* | — | This method takes no input parameters |

## Relevant Database Tables
- **Specialization**: SpecializationCode (CHAR(3), PK), SpecializationName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Specialization` is accessed as a standalone entity for this exercise. No joins or navigation properties are needed.

## Required Operations
1. Write a LINQ query using **Query Syntax** to select all records from the `Specializations` DbSet.
2. Convert the query result to a `List<Specialization>`.
3. Return the list.

## Business Rules and Validations
1. **LINQ Query Syntax is mandatory** — use `from s in context.Specializations select s` pattern.
2. All specializations must be returned; no filtering is needed.
3. If the table is empty, return an empty list (not `null`).

## Required IF-ELSE Conditions
None. This is a straightforward select-all operation with no conditional branching.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specializations exist in the database | `List<Specialization>` containing all specialization records |
| No specializations exist in the database | An empty `List<Specialization>` |
| An exception occurs during database access | `null` |

## Exception Handling Requirements
- Wrap the LINQ query and list conversion inside a `try-catch` block.
- In the `catch` block, return `null`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `GetAllSpecializations` in `CureHospitalRepositoryEF`
- [ ] Controller method `GetAllSpecializations` in `CureHospitalController`
