# Exercise 02: GetAllDoctors

## Business Scenario
The CureHospital management dashboard requires a master list of all registered doctors in the system. The HR department and administrative staff use this list for workforce planning, directory lookups, and generating reports on the hospital's physician roster.

## Problem Statement
Write a repository method named `GetAllDoctors` that retrieves all doctor records from the `Doctors` table and returns them as a `List<Doctor>`. You **must** use LINQ **Query Syntax** (i.e., `from ... in ... select ...`) to fetch the records — Method Syntax (e.g., `.Select()`, `.ToList()` directly on the DbSet without a query expression) is **not permitted**. If any exception occurs, the method should return `null`. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public List<Doctor> GetAllDoctors()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action method named `GetAllDoctors` in `CureHospitalController`. This method takes no parameters. It should call the repository's `GetAllDoctors` method and return the resulting list wrapped in a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetAllDoctors()
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| *(none)* | — | This method takes no input parameters |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK, IDENTITY), DoctorName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Doctor` is accessed as a standalone entity for this exercise. No joins or navigation properties are needed.

## Required Operations
1. Write a LINQ query using **Query Syntax** to select all records from the `Doctors` DbSet.
2. Convert the query result to a `List<Doctor>`.
3. Return the list.

## Business Rules and Validations
1. **LINQ Query Syntax is mandatory** — use `from d in context.Doctors select d` pattern.
2. All doctors must be returned regardless of any status or condition; no filtering is needed.
3. If the table is empty, return an empty list (not `null`).

## Required IF-ELSE Conditions
None. This is a straightforward select-all operation with no conditional branching.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctors exist in the database | `List<Doctor>` containing all doctor records |
| No doctors exist in the database | An empty `List<Doctor>` |
| An exception occurs during database access | `null` |

## Exception Handling Requirements
- Wrap the LINQ query and list conversion inside a `try-catch` block.
- In the `catch` block, return `null`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `GetAllDoctors` in `CureHospitalRepositoryEF`
- [ ] Controller method `GetAllDoctors` in `CureHospitalController`
