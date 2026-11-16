# Exercise 59: GetSurgeryDetailsByDateFiltered

## Business Scenario
Operating theatre supervisors only want to review morning surgical cases (procedures starting before 12:00 PM) retrieved via the database function.

## Problem Statement
Write a repository method `GetSurgeryDetailsByDateFiltered` taking `surgeryDate`.
1. Call `ufn_FetchSurgeryDetails` using `FromSqlInterpolated` to retrieve the day's surgeries into memory.
2. Using LINQ Query Syntax on the returned in-memory list, filter records where `StartTime < 12.00m`:
```csharp
from d in details
where d.StartTime < 12.00m
select d
```
3. Return the filtered `List<SurgeryDetail>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetail> GetSurgeryDetailsByDateFiltered(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsByDateFiltered(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Scheduled date |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- TVF result mapped to `SurgeryDetail`.

## Required Operations
1. Call TVF into `List<SurgeryDetail>`.
2. Apply in-memory LINQ Query Syntax filter: `where d.StartTime < 12.00m`.
3. Return filtered list.

## Business Rules and Validations
1. StartTime strictly less than 12.00.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Morning surgeries found | `List<SurgeryDetail>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
