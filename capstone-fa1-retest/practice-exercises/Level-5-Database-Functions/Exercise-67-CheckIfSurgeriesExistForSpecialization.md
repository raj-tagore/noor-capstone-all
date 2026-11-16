# Exercise 67: CheckIfSurgeriesExistForSpecialization

## Business Scenario
A rapid health check validates whether any surgical procedures in Anesthesiology or Cardiology are active on a date before granting maintenance access to operating rooms.

## Problem Statement
Write a repository method `CheckIfSurgeriesExistForSpecialization` taking `specializationName` and `surgeryDate`.
Invoke `ufn_FetchSurgeryCount`. If count > 0, return `true`. If count == 0, return `false`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool CheckIfSurgeriesExistForSpecialization(string specializationName, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult CheckIfSurgeriesExistForSpecialization(string specializationName, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationName | string | Specialty title |
| surgeryDate | DateTime | Date |

## Relevant Database Tables
- Scalar Function: `ufn_FetchSurgeryCount`

## Entity Relationships
- Scalar function query.

## Required Operations
1. Execute scalar function.
2. Return `count > 0`.

## Business Rules and Validations
1. Returns boolean presence check.

## Required IF-ELSE Conditions
- **Condition:** `return count > 0;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries exist | `true` |
| No surgeries | `false` |
| Exception | `false` |

## Exception Handling Requirements
Try-catch returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
