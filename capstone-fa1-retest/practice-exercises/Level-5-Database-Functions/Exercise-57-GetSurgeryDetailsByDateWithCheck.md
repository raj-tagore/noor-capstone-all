# Exercise 57: GetSurgeryDetailsByDateWithCheck

## Business Scenario
The clinical scheduler requires checking if any surgical cases exist for a chosen date before rendering the operating schedule.

## Problem Statement
Write a repository method `GetSurgeryDetailsByDateWithCheck` accepting `surgeryDate`.
Call the TVF `ufn_FetchSurgeryDetails`. Check if the returned list is null or has zero records (`Count == 0`).
If empty, return `null`. If records exist, return the `List<SurgeryDetail>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetail> GetSurgeryDetailsByDateWithCheck(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsByDateWithCheck(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Surgery date |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Queries TVF result mapped to `SurgeryDetail`.

## Required Operations
1. Execute TVF query.
2. Check `list == null || list.Count == 0`.
3. If empty, return `null`.
4. Otherwise, return list.

## Business Rules and Validations
1. Return null if list is empty.

## Required IF-ELSE Conditions
- **Condition:** If `list == null || list.Count == 0` return `null`; else return `list`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<SurgeryDetail>` |
| No surgeries on date | `null` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
