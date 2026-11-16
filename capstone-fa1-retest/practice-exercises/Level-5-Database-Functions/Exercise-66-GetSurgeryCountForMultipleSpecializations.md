# Exercise 66: GetSurgeryCountForMultipleSpecializations

## Business Scenario
Executive dashboards compile a breakdown of surgery volume across all registered specializations on a given date by repeatedly invoking the scalar function.

## Problem Statement
Define a DTO class `SpecSurgeryCount`:
- `string SpecializationName { get; set; }`
- `int SurgeryCount { get; set; }`

Write a repository method `GetSurgeryCountForMultipleSpecializations` accepting `surgeryDate`.
1. Retrieve all specializations from `context.Specializations` using LINQ Query Syntax.
2. For each specialization, invoke `ufn_FetchSurgeryCount` passing `spec.SpecializationName` and `surgeryDate`.
3. Construct `SpecSurgeryCount` object and collect into a list.
4. Return `List<SpecSurgeryCount>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SpecSurgeryCount> GetSurgeryCountForMultipleSpecializations(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCountForMultipleSpecializations(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Selected date |

## Relevant Database Tables
- **Specialization**: SpecializationName
- Scalar Function: `ufn_FetchSurgeryCount`

## Entity Relationships
- Loops over `Specialization` table calling scalar function.

## Required Operations
1. Query all specializations.
2. Loop over list with `foreach`.
3. Execute scalar function for each item.
4. Populate DTO list and return.

## Business Rules and Validations
1. Scalar function called per specialization.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Results compiled | `List<SpecSurgeryCount>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SpecSurgeryCount`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
