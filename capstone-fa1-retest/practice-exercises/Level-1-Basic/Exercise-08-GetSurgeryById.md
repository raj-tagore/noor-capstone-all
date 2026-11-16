# Exercise 08: GetSurgeryById

## Business Scenario
The CureHospital surgical department needs to quickly retrieve the details of a specific surgery when given its Surgery ID. This is essential for pre-operative review, post-operative documentation, and administrative follow-ups. Staff should be able to look up any surgery by its unique identifier to see the assigned doctor, scheduled date, time slot, and surgery category.

## Problem Statement
Write a repository method named `GetSurgeryById` that accepts a `surgeryId` (int) as a parameter and retrieves the corresponding Surgery record from the database. The method should use the Entity Framework `Find()` method to locate the surgery by its primary key. If the surgery exists, return the Surgery entity object. If no surgery is found with the given ID, return `null`. The method must include proper exception handling — if any exception occurs during the database operation, it should be caught and `null` should be returned. Expose this method through a Web API controller action.

## DAL / Repository Method Signature
```csharp
public Surgery GetSurgeryById(int surgeryId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action method named `GetSurgeryById` in `CureHospitalController`. It should accept `surgeryId` as a **query parameter**. The controller method should call the repository's `GetSurgeryById` method and return the result wrapped in a `JsonResult`. If the returned Surgery object is `null`, the controller should still return a `JsonResult` containing `null` or an appropriate message such as `"No surgery found"`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryById(int surgeryId)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | The unique identifier of the surgery to retrieve (e.g., 5000, 5001) |

## Relevant Database Tables
- **Surgery**: SurgeryID (INT, PK, IDENTITY(5000,1)), DoctorID (INT, FK), SurgeryDate (DATE), StartTime (DECIMAL(4,2)), EndTime (DECIMAL(4,2)), SurgeryCategory (CHAR(3), FK)

## Entity Relationships
- `Surgery.DoctorID` references `Doctor.DoctorID` (FK relationship).
- `Surgery.SurgeryCategory` references `Specialization.SpecializationCode` (FK relationship).
- For this exercise, only the Surgery entity itself is retrieved — no navigation properties need to be loaded.

## Required Operations
1. Accept the `surgeryId` parameter.
2. Use `Find()` on the `Surgeries` DbSet to locate the Surgery record by primary key.
3. Return the Surgery entity if found, or `null` if not found.

## Business Rules and Validations
1. The `Find()` method should be used to search by the primary key (`SurgeryID`).
2. If no Surgery exists with the given `surgeryId`, the method must return `null` (not throw an exception).
3. No input validation is required beyond what `Find()` inherently handles.

## Required IF-ELSE Conditions
None. The `Find()` method inherently returns `null` when no match is found, so no explicit if-else branching is required in the repository method. The controller may optionally check for `null` to return a descriptive message.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery with given ID exists in the database | The `Surgery` entity object |
| No surgery found with the given ID | `null` |
| An exception occurs during database access | `null` |

## Exception Handling Requirements
- Wrap the database operation inside a `try-catch` block.
- In the `catch` block, return `null`.
- The `catch` should handle a general `Exception`.

## What Needs to Be Implemented
- [ ] Repository method `GetSurgeryById` in `CureHospitalRepositoryEF`
- [ ] Controller method `GetSurgeryById` in `CureHospitalController`
