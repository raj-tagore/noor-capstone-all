# Exercise 28: UpdateMultipleSurgeriesConditionally

## Business Scenario
To prevent surgeon fatigue during intensive surgical days, morning surgeries (starting before 10:00 AM) are allotted a 4-hour window, while midday/afternoon surgeries (starting at or after 10:00 AM) are allotted a 6-hour window.

## Problem Statement
Implement a repository method `UpdateMultipleSurgeriesConditionally` that accepts `surgeryDate`.
Retrieve all surgeries on `surgeryDate` using LINQ Query Syntax. If none found, return `0`.
Iterate through the list using `foreach`:
- If `surgery.StartTime < 10.00m`, set `surgery.EndTime = surgery.StartTime + 4.00m`.
- If `surgery.StartTime >= 10.00m`, set `surgery.EndTime = surgery.StartTime + 6.00m`.
Save changes and return the total count of modified surgeries. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int UpdateMultipleSurgeriesConditionally(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the count.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateMultipleSurgeriesConditionally(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Scheduled date |

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryDate, StartTime, EndTime

## Entity Relationships
- Multi-record update on `Surgery`.

## Required Operations
1. Retrieve surgeries for `surgeryDate` via Query Syntax.
2. If empty, return `0`.
3. In `foreach`, evaluate `s.StartTime < 10.00m`.
4. Assign respective `EndTime`.
5. Execute `SaveChanges()`.
6. Return list count.

## Business Rules and Validations
1. Morning surgeries (< 10.00): duration = 4 hrs.
2. Later surgeries (>= 10.00): duration = 6 hrs.

## Required IF-ELSE Conditions
- **Condition inside loop:**
  `if (s.StartTime < 10.00m) s.EndTime = s.StartTime + 4; else s.EndTime = s.StartTime + 6;`

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| No surgeries found | `0` |
| Modified surgeries | Count of records modified |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
