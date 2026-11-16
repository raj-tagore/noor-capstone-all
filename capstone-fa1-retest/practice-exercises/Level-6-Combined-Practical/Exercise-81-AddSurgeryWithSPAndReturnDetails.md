# Exercise 81: AddSurgeryWithSPAndReturnDetails

## Business Scenario
A composite booking workflow calls `usp_AddSurgeryDetails` to schedule a surgery, and upon success, immediately queries `ufn_FetchSurgeryDetails` to return the complete enriched `SurgeryDetail` entity for confirmation display.

## Problem Statement
Write a repository method `AddSurgeryWithSPAndReturnDetails` taking surgery parameters.
1. Invoke stored procedure `usp_AddSurgeryDetails` (extracting return code and `out int surgeryId`).
2. If procedure return code != 1, return `null`.
3. If return code == 1, invoke TVF `ufn_FetchSurgeryDetails` passing `surgeryDate`.
4. Using LINQ Query Syntax, locate the record where `d.SurgeryId == surgeryId` with `FirstOrDefault()`.
5. Return the single `SurgeryDetail` object.
6. On exception, return `null`.

## DAL / Repository Method Signature
```csharp
public SurgeryDetail AddSurgeryWithSPAndReturnDetails(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the `SurgeryDetail` object.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithSPAndReturnDetails(Models.Surgery surgery)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryCategory | string | Category |
| surgeryDate | DateTime | Date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |

## Relevant Database Tables
- Stored Procedure: `usp_AddSurgeryDetails`
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Stored procedure execution -> TVF query pipeline.

## Required Operations
1. Execute SP to insert surgery.
2. Check if SP return code == 1. If not, return `null`.
3. Call TVF for surgery date.
4. Filter TVF result by `SurgeryId == newSurgeryId`.
5. Return matching `SurgeryDetail`.

## Business Rules and Validations
1. Integrates SP insert with TVF query.

## Required IF-ELSE Conditions
- **Condition:** If `spResult != 1` return `null`; else query TVF and return record.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Successful booking | Single `SurgeryDetail` entity |
| SP validation failure | `null` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
