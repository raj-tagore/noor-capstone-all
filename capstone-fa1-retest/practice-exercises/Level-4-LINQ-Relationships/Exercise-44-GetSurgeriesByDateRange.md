# Exercise 44: GetSurgeriesByDateRange

## Business Scenario
Hospital auditor reporting requires viewing all surgical procedures conducted within a specific fiscal or calendar date window.

## Problem Statement
Write a repository method `GetSurgeriesByDateRange` taking `startDate` and `endDate`.
Using LINQ Query Syntax:
```csharp
from s in context.Surgeries
where s.SurgeryDate >= startDate && s.SurgeryDate <= endDate
select s
```
Return `List<Surgery>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<Surgery> GetSurgeriesByDateRange(DateTime startDate, DateTime endDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeriesByDateRange(DateTime startDate, DateTime endDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| startDate | DateTime | Window start |
| endDate | DateTime | Window end |

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryDate

## Entity Relationships
- Filtered on `SurgeryDate`.

## Required Operations
1. Query surgeries bounded by date range via LINQ Query Syntax.
2. Return `.ToList()`.

## Business Rules and Validations
1. Inclusive date bounds (`>=` and `<=`).

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<Surgery>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
