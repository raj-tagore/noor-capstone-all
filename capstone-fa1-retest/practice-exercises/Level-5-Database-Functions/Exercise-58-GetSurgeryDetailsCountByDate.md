# Exercise 58: GetSurgeryDetailsCountByDate

## Business Scenario
Intake coordinators display the total count of scheduled surgeries on any date using the TVF.

## Problem Statement
Write a repository method `GetSurgeryDetailsCountByDate` taking `surgeryDate`.
Invoke the TVF `ufn_FetchSurgeryDetails`, retrieve the list of results, and return its `Count`.
If null or on exception, return `0`.

## DAL / Repository Method Signature
```csharp
public int GetSurgeryDetailsCountByDate(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the count integer.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsCountByDate(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Surgery date |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Operates on TVF results.

## Required Operations
1. Call TVF via `FromSqlInterpolated`.
2. Check if null; return count or 0.

## Business Rules and Validations
1. Returns count of surgery rows.

## Required IF-ELSE Conditions
- **Condition:** If `list == null` return 0; else return `list.Count`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | Integer count >= 0 |
| Exception | `0` |

## Exception Handling Requirements
Try-catch block returning `0`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
