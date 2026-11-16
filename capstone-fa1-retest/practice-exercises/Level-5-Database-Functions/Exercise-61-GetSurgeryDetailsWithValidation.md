# Exercise 61: GetSurgeryDetailsWithValidation

## Business Scenario
Quality assurance audits identify high-complexity surgeries lasting longer than 4 continuous hours.

## Problem Statement
Write a repository method `GetSurgeryDetailsWithValidation` taking `surgeryDate`.
1. Call TVF `ufn_FetchSurgeryDetails` for the date.
2. If list is null or empty, return `0`.
3. Iterate through the records using a `foreach` loop:
   For each record, check if `(record.EndTime - record.StartTime) > 4.00m`.
   Count all surgeries exceeding 4 hours.
4. Return the integer count. On exception, return `-1`.

## DAL / Repository Method Signature
```csharp
public int GetSurgeryDetailsWithValidation(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the integer.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsWithValidation(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Target date |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Iteration on TVF returned models.

## Required Operations
1. Call TVF.
2. Initialize `longSurgeryCount = 0`.
3. In `foreach`, evaluate `if ((s.EndTime - s.StartTime) > 4.00m) longSurgeryCount++;`.
4. Return count.

## Business Rules and Validations
1. Threshold duration > 4.00 hrs.

## Required IF-ELSE Conditions
- **Condition inside loop:** `if ((s.EndTime - s.StartTime) > 4.00m) count++;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Audited count | Integer >= 0 |
| Exception | `-1` |

## Exception Handling Requirements
Try-catch block returning `-1`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
