# Exercise 63: GetSurgeryDetailsTotalHours

## Business Scenario
Facility managers sum total daily theater usage hours from TVF records to compute room utilization rates.

## Problem Statement
Write a repository method `GetSurgeryDetailsTotalHours` accepting `surgeryDate`.
1. Call TVF `ufn_FetchSurgeryDetails` for `surgeryDate`.
2. If result is null or empty, return `0m`.
3. Using a `foreach` loop, iterate over records and accumulate `totalHours += (record.EndTime - record.StartTime)`.
4. Return total `decimal`. On exception, return `0m`.

## DAL / Repository Method Signature
```csharp
public decimal GetSurgeryDetailsTotalHours(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsTotalHours(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Date to aggregate |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Calculation on TVF output.

## Required Operations
1. Call TVF.
2. Initialize `decimal total = 0m`.
3. In `foreach`, add `(s.EndTime - s.StartTime)`.
4. Return total.

## Business Rules and Validations
1. Sum across all daily surgeries.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Total calculated | `decimal` value (e.g., 9.00m) |
| Exception | `0m` |

## Exception Handling Requirements
Try-catch block returning `0m`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
