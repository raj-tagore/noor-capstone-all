# Exercise 11: AddDoctorWithDuplicateCheck

## Business Scenario
At CureHospital, doctor records must maintain unique names across the organization to prevent clinical confusion when assigning doctors to operating rooms. Before onboarding and inserting any doctor record, the system must verify whether a doctor with the identical name already exists.

## Problem Statement
Write a repository method named `AddDoctorWithDuplicateCheck` that accepts a doctor name as input. The method must check if any doctor with the specified name already exists in the `Doctor` table using a LINQ query syntax statement. If a record already exists, the insertion should not proceed, and the method should return `false`. If no duplicate is detected, create a new `Doctor` entity, set its `DoctorName`, add it to the database, save the changes, and return `true`. Ensure all operations are enclosed in a try-catch block returning `false` on any exception.

## DAL / Repository Method Signature
```csharp
public bool AddDoctorWithDuplicateCheck(string doctorName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action method named `AddDoctorWithDuplicateCheck` in `CureHospitalController` that takes `doctorName` as a query parameter or body parameter, delegates to the repository, and returns a `JsonResult` containing the boolean status.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddDoctorWithDuplicateCheck(string doctorName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Name of the doctor to be verified and added |

## Relevant Database Tables
- **Doctor**: DoctorID (INT, PK, IDENTITY), DoctorName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Doctor` is the root entity; no joins needed for this operation.

## Required Operations
1. Validate incoming `doctorName` (ensure not null/empty).
2. Execute a LINQ Query Syntax query against `context.Doctors` filtering by `DoctorName == doctorName`.
3. Check whether the query yields an existing doctor record.
4. If found, return `false`.
5. If not found, instantiate a `Doctor` object, assign `DoctorName = doctorName`, invoke `context.Doctors.Add()`, and execute `context.SaveChanges()`.
6. Return `true` upon successful persistence.

## Business Rules and Validations
1. Duplication check must be case-insensitive or exact according to database collation.
2. If duplicate exists, no database insert should be executed.
3. Database `SaveChanges()` must be called only after adding.

## Required IF-ELSE Conditions
- **Condition 1:** If existing doctor record != null, return `false`.
- **Condition 2:** Else proceed to create, add, save, and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor name already exists | `false` |
| Doctor successfully added | `true` |
| Exception or invalid data | `false` |

## Exception Handling Requirements
Wrap the entire method in a `try-catch` block. Catch any exception and return `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
