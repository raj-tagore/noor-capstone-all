# Exercise 45: GetSurgeriesSortedByDate

## Business Scenario
The operating theater daily dashboard displays all hospital surgeries arranged in chronological order by date and then by start time.

## Problem Statement
Write a repository method `GetSurgeriesSortedByDate` returning all surgeries ordered chronologically.
Using LINQ Query Syntax:
```csharp
from s in context.Surgeries
orderby s.SurgeryDate, s.StartTime
select s
```
Return `List<Surgery>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<Surgery> GetSurgeriesSortedByDate()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeriesSortedByDate()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryDate, StartTime

## Entity Relationships
- Single-table ordered query.

## Required Operations
1. Query `Surgeries` with `orderby s.SurgeryDate, s.StartTime`.
2. Return `.ToList()`.

## Business Rules and Validations
1. Ascending order on date then start time.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries retrieved | Ordered `List<Surgery>` |
| Exception caught | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
