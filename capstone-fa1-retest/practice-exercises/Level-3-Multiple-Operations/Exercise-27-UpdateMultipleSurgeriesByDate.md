# Exercise 27: UpdateMultipleSurgeriesByDate

## Business Scenario
Due to scheduled operating theater deep sterilization starting early in the evening, all surgeries scheduled on a particular day must have their end times capped at a specific hour.

## Problem Statement
Write a repository method `UpdateMultipleSurgeriesByDate` accepting `surgeryDate` and `newEndTime`.
Query all surgeries matching `s.SurgeryDate == surgeryDate` using LINQ Query Syntax. If the resulting list is empty, return `false`. Otherwise, iterate through all matching surgeries using `foreach`, set `s.EndTime = newEndTime`, call `SaveChanges()`, and return `true`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool UpdateMultipleSurgeriesByDate(DateTime surgeryDate, decimal newEndTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateMultipleSurgeriesByDate(DateTime surgeryDate, decimal newEndTime)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Date of surgeries |
| newEndTime | decimal | Updated end time |

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryDate, EndTime

## Entity Relationships
- Direct batch update on `Surgery`.

## Required Operations
1. Query surgeries via LINQ Query Syntax:
   `from s in context.Surgeries where s.SurgeryDate == surgeryDate select s`
2. Evaluate if count == 0; return `false`.
3. Loop through list, update `EndTime`.
4. Call `context.SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Must find at least one surgery.

## Required IF-ELSE Conditions
- **Condition:** If list is empty return `false`; else update and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| No surgeries on date | `false` |
| Surgeries updated | `true` |
| Exception caught | `false` |

## Exception Handling Requirements
Try-catch returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
