# Exercise 69: AddSurgeryViaStoredProcSimplified

## Business Scenario
A simplified API interface wraps `usp_AddSurgeryDetails` such that the repository method directly returns the generated `SurgeryID` upon success, or the negative error code upon failure, eliminating C# `out` parameters.

## Problem Statement
Write a repository method `AddSurgeryViaStoredProcSimplified` taking `doctorId`, `surgeryCategory`, `surgeryDate`, `startTime`, and `endTime`.
Invoke `usp_AddSurgeryDetails`.
- If the return code equals `1`, return the generated `SurgeryID` (from output parameter `@si`).
- If the return code is negative (e.g., -1, -2, -3, -4, -5, -6), return that return code directly.
- On exception, return `-98`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryViaStoredProcSimplified(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the resulting integer.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryViaStoredProcSimplified(Models.Surgery surgery)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryCategory | string | Category |
| surgeryDate | DateTime | Date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |

## Relevant Database Tables
- Stored Procedure: `usp_AddSurgeryDetails`

## Entity Relationships
- Stored procedure executes validation and insert.

## Required Operations
1. Execute SP with output parameter.
2. Check return code. If 1, return output surgery ID.
3. Otherwise return return code.

## Business Rules and Validations
1. Direct ID return on success.

## Required IF-ELSE Conditions
- **Condition:** `if (returnCode == 1) return surgeryId; else return returnCode;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Success | Generated SurgeryID (> 0) |
| Failure | Negative error code (-1 to -6) |
| Exception | `-98` |

## Exception Handling Requirements
Try-catch returning `-98`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
