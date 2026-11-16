# Exercise 31: ReassignSurgeryCategory

## Business Scenario
A pre-operative review may determine that a patient's surgery should be recategorized under a different clinical specialty code (e.g., from 'GYN' to 'ANE').

## Problem Statement
Write a repository method `ReassignSurgeryCategory` taking `surgeryId` and `newSpecializationCode`.
Pattern — Two LINQ Queries:
1. Query 1: Find the surgery by `surgeryId` using LINQ Query Syntax. If not found, return `-1`.
2. Query 2: Find the target specialization in `Specialization` table by `newSpecializationCode` using LINQ Query Syntax. If not found, return `-2`.
3. If both exist, set `surgery.SurgeryCategory = newSpecializationCode`, save changes, and return `1`.
4. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int ReassignSurgeryCategory(int surgeryId, string newSpecializationCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult ReassignSurgeryCategory(int surgeryId, string newSpecializationCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryId | int | Target surgery ID |
| newSpecializationCode | string | New category code |

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryCategory
- **Specialization**: SpecializationCode

## Entity Relationships
- `Surgery.SurgeryCategory` foreign key to `Specialization.SpecializationCode`.

## Required Operations
1. Find surgery via LINQ query; return `-1` if null.
2. Find specialization via LINQ query; return `-2` if null.
3. Update `SurgeryCategory`.
4. Save changes and return `1`.

## Business Rules and Validations
1. Surgery must exist (-1).
2. Category must exist (-2).

## Required IF-ELSE Conditions
- **Condition 1:** If surgery is null -> return `-1`.
- **Condition 2:** Else if spec is null -> return `-2`.
- **Condition 3:** Else update and return `1`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgery not found | `-1` |
| Specialization not found | `-2` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
