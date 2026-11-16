# Exercise 79: GetSurgeryDetailsWithScalarCount

## Business Scenario
A multi-function reporting pipeline calls the table-valued function `ufn_FetchSurgeryDetails` to pull daily surgeries and enriches each row by calling the scalar function `ufn_FetchSurgeryCount` to display total daily surgeries in that specialization category.

## Problem Statement
Define a DTO class `SurgeryDetailWithCount`:
- `int SurgeryId { get; set; }`
- `string DoctorName { get; set; }`
- `string SpecializationName { get; set; }`
- `decimal StartTime { get; set; }`
- `decimal EndTime { get; set; }`
- `int TotalCategorySurgeries { get; set; }`

Write a repository method `GetSurgeryDetailsWithScalarCount` accepting `surgeryDate`.
1. Call TVF `ufn_FetchSurgeryDetails` for `surgeryDate`.
2. For each surgery detail row, call the scalar function `ufn_FetchSurgeryCount` passing `record.SpecializationName` and `surgeryDate`.
3. Construct `SurgeryDetailWithCount` DTOs and return as `List<SurgeryDetailWithCount>`.
4. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetailWithCount> GetSurgeryDetailsWithScalarCount(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsWithScalarCount(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Selected date |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Scalar: `ufn_FetchSurgeryCount`

## Entity Relationships
- Combines TVF output with scalar function lookups into a DTO.

## Required Operations
1. Declare DTO `SurgeryDetailWithCount`.
2. Call TVF.
3. In `foreach`, call scalar function for each item.
4. Populate DTO list and return.

## Business Rules and Validations
1. Integrates TVF + Scalar function.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<SurgeryDetailWithCount>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SurgeryDetailWithCount`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
