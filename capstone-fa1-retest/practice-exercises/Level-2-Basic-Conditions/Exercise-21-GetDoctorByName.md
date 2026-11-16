# Exercise 21: GetDoctorByName

## Business Scenario
Patients often search for doctors by name rather than knowing their internal numeric Doctor ID.

## Problem Statement
Write a repository method `GetDoctorByName` that accepts a doctor name (string) and retrieves the corresponding `Doctor` object from the database using LINQ Query Syntax (`from d in context.Doctors where d.DoctorName == doctorName select d`) combined with `FirstOrDefault()`. If no match is found, return `null`. Handle any exceptions by returning `null`.

## DAL / Repository Method Signature
```csharp
public Doctor GetDoctorByName(string doctorName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` accepting `doctorName` as a query parameter and returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorByName(string doctorName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorName | string | Exact name of the doctor |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName

## Entity Relationships
- Queries `Doctor` entity.

## Required Operations
1. Formulate LINQ Query Syntax:
   `from d in context.Doctors where d.DoctorName == doctorName select d`
2. Apply `FirstOrDefault()`.
3. Return the Doctor object or null.

## Business Rules and Validations
1. Match on exact doctor name.
2. Query Syntax must be used.

## Required IF-ELSE Conditions
- None required in DAL; `FirstOrDefault()` handles null when not found.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor found | `Doctor` entity |
| Doctor not found | `null` |
| Exception caught | `null` |

## Exception Handling Requirements
Try-catch returning `null` on failure.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
