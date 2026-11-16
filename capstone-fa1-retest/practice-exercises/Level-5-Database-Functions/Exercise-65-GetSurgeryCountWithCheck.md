# Exercise 65: GetSurgeryCountWithCheck

## Business Scenario
Before booking operating room support teams, the department scheduler checks if any surgeries exist for a specialization on a given date.

## Problem Statement
Write a repository method `GetSurgeryCountWithCheck` taking `specializationName` and `surgeryDate`.
Invoke `ufn_FetchSurgeryCount` via `ExecuteScalar()`.
- If the returned count is `> 0`, return the count.
- If the returned count is `0`, return `-1` to signify no surgeries found for that specialty on that date.
- On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int GetSurgeryCountWithCheck(string specializationName, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCountWithCheck(string specializationName, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationName | string | Specialty title |
| surgeryDate | DateTime | Date |

## Relevant Database Tables
- Scalar Function: `ufn_FetchSurgeryCount`

## Entity Relationships
- Queries scalar function.

## Required Operations
1. Execute scalar function via ADO.NET.
2. Evaluate result: if count > 0 return count; if count == 0 return -1.

## Business Rules and Validations
1. Zero surgeries mapped to -1.

## Required IF-ELSE Conditions
- **Condition:** `if (count > 0) return count; else return -1;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries exist | Count integer > 0 |
| No surgeries exist | `-1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
