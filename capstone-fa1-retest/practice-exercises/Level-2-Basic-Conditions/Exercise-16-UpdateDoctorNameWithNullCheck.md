# Exercise 16: UpdateDoctorNameWithNullCheck

## Business Scenario
Administrative personnel need to correct typographical errors in doctor names. The system must guard against empty or null strings and verify that the target doctor exists before applying updates.

## Problem Statement
Implement a repository method named `UpdateDoctorNameWithNullCheck` that takes `doctorId` and `newName`.
Business rules:
- If `newName` is null, empty, or whitespace, return status code `-2`.
- If the doctor record with the provided `doctorId` is not found, return `-1`.
- If valid, update `DoctorName` to `newName`, call `SaveChanges()`, and return status code `1`.
- On unexpected exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateDoctorNameWithNullCheck(int doctorId, string newName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` accepting `doctorId` and `newName` as query parameters, returning a `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateDoctorNameWithNullCheck(int doctorId, string newName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Target doctor ID |
| newName | string | Revised doctor name |

## Relevant Database Tables
- **Doctor**: DoctorID (PK), DoctorName

## Entity Relationships
- Direct update on single table `Doctor`.

## Required Operations
1. Check if `string.IsNullOrWhiteSpace(newName)`. If true, return `-2`.
2. Search for doctor using `context.Doctors.Find(doctorId)`.
3. If doctor is null, return `-1`.
4. Set `doctor.DoctorName = newName`.
5. Call `context.SaveChanges()`.
6. Return `1`.

## Business Rules and Validations
1. Empty or whitespace names rejected with `-2`.
2. Non-existent doctor ID rejected with `-1`.

## Required IF-ELSE Conditions
- **Condition 1:** `if (string.IsNullOrWhiteSpace(newName)) return -2;`
- **Condition 2:** `if (doc == null) return -1;`
- **Condition 3:** `else update, save, return 1;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Invalid new name | `-2` |
| Doctor not found | `-1` |
| Update successful | `1` |
| Exception caught | `-99` |

## Exception Handling Requirements
Repository method wrapped in try-catch; catch returns `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
