# Exercise 33: UpdateAllSurgeryTimesForSpecialization

## Business Scenario
Hospital scheduling updates operating block times for an entire specialty division (e.g., shifting all Cardiology surgeries by +1.5 hours) due to equipment maintenance.

## Problem Statement
Write a repository method `UpdateAllSurgeryTimesForSpecialization` taking `specializationCode` and `addHours` (decimal).
Retrieve all surgeries where `SurgeryCategory == specializationCode` using LINQ Query Syntax.
If none found, return `0`.
In a `foreach` loop, adjust each surgery's `StartTime += addHours` and `EndTime += addHours`.
Call `SaveChanges()` and return the count of updated surgeries. Return `-99` on exception.

## DAL / Repository Method Signature
```csharp
public int UpdateAllSurgeryTimesForSpecialization(string specializationCode, decimal addHours)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult` wrapping the count.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateAllSurgeryTimesForSpecialization(string specializationCode, decimal addHours)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | Specialization code (e.g. "CAR") |
| addHours | decimal | Shift offset (e.g. 1.50) |

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryCategory, StartTime, EndTime

## Entity Relationships
- Filtered on `SurgeryCategory`.

## Required Operations
1. Query surgeries matching category via LINQ Query Syntax.
2. If count is 0, return `0`.
3. Loop over items and add `addHours` to `StartTime` and `EndTime`.
4. Call `SaveChanges()` and return count.

## Business Rules and Validations
1. Update applied across all matching records.

## Required IF-ELSE Conditions
- **Condition:** If list is empty return 0; else update and return count.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| No surgeries in category | `0` |
| Surgeries shifted | Count of surgeries updated |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
