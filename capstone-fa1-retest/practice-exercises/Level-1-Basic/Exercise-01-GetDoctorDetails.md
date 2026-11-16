# Exercise 01: GetDoctorDetails

## Business Scenario
The CureHospital administration portal needs a feature that allows staff to quickly look up the details of a specific doctor by their unique Doctor ID. This is used across multiple screens such as appointment booking and surgery scheduling to verify doctor information before proceeding.

## Problem Statement
Write a repository method named `GetDoctorDetails` that accepts a `DoctorID` (integer) as input and retrieves the corresponding Doctor record from the database. The method should use the Entity Framework `Find()` method to locate the doctor by their primary key. If the doctor exists, return the Doctor entity object. If no doctor is found with the given ID, return `null`. The method must include proper exception handling — if any exception occurs during the database operation, it should be caught and `null` should be returned. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public Doctor GetDoctorDetails(int doctorId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action method named `GetDoctorDetails` in `CureHospitalController`. It should accept `doctorId` as a **query parameter**. The controller method should call the repository's `GetDoctorDetails` method and return the result wrapped in a `JsonResult`. If the returned Doctor object is `null`, the controller should still return a `JsonResult` containing `null` or an appropriate message such as `"No doctor found"`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorDetails(int doctorId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | The unique identifier of the doctor to retrieve (e.g., 1001, 1002) |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK, IDENTITY), DoctorName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Doctor` is a standalone entity for this exercise. No joins or navigation properties are needed.

## Required Operations
1. Accept the `doctorId` parameter.
2. Use `Find()` on the `Doctors` DbSet to locate the Doctor record by primary key.
3. Return the Doctor entity if found, or `null` if not found.

## Business Rules and Validations
1. The `Find()` method should be used to search by the primary key (`DoctorID`).
2. If no Doctor exists with the given `doctorId`, the method must return `null` (not throw an exception).
3. No input validation is required beyond what `Find()` inherently handles.

## Required IF-ELSE Conditions
None. The `Find()` method inherently returns `null` when no match is found, so no explicit if-else branching is required in the repository method. The controller may optionally check for `null` to return a descriptive message.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor with given ID exists in the database | The `Doctor` entity object |
| No doctor found with the given ID | `null` |
| An exception occurs during database access | `null` |

## Exception Handling Requirements
- Wrap the database operation inside a `try-catch` block.
- In the `catch` block, return `null`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `GetDoctorDetails` in `CureHospitalRepositoryEF`
- [ ] Controller method `GetDoctorDetails` in `CureHospitalController`
