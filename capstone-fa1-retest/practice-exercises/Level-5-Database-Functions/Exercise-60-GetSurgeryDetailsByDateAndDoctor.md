# Exercise 60: GetSurgeryDetailsByDateAndDoctor

## Business Scenario
A surgeon reviews only their own assigned surgeries on a given date by querying the TVF.

## Problem Statement
Write a repository method `GetSurgeryDetailsByDateAndDoctor` taking `surgeryDate` (DateTime) and `doctorName` (string).
1. Call `ufn_FetchSurgeryDetails` for `surgeryDate`.
2. Using LINQ Query Syntax on the result set, filter where `d.DoctorName.ToLower() == doctorName.ToLower()` (or exact string comparison).
3. Return `List<SurgeryDetail>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SurgeryDetail> GetSurgeryDetailsByDateAndDoctor(DateTime surgeryDate, string doctorName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryDetailsByDateAndDoctor(DateTime surgeryDate, string doctorName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Scheduled date |
| doctorName | string | Doctor name |

## Relevant Database Tables
- TVF: `ufn_FetchSurgeryDetails`
- Model: `SurgeryDetail`

## Entity Relationships
- Filtered on `DoctorName` field of `SurgeryDetail`.

## Required Operations
1. Call TVF.
2. In-memory LINQ query filtering on `DoctorName`.
3. Return `.ToList()`.

## Business Rules and Validations
1. Match doctor name case-insensitively.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Surgeries found | `List<SurgeryDetail>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
