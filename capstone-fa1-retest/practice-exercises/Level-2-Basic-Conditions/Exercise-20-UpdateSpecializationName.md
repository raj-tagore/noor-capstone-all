# Exercise 20: UpdateSpecializationName

## Business Scenario
Medical boards periodically rename department classifications (e.g., updating 'Anesthesiologist' to 'Anesthesiology & Critical Care'). The system must allow modifying specialization titles.

## Problem Statement
Write a repository method `UpdateSpecializationName` taking `specializationCode` and `newSpecializationName`. Locate the specialization by its primary key using `Find()`. If not found or if `newSpecializationName` is empty, return `false`. Otherwise, update `SpecializationName`, call `SaveChanges()`, and return `true`. Return `false` on exception.

## DAL / Repository Method Signature
```csharp
public bool UpdateSpecializationName(string specializationCode, string newSpecializationName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` accepting parameters and returning `JsonResult` wrapping the boolean.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult UpdateSpecializationName(string specializationCode, string newSpecializationName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | 3-char code |
| newSpecializationName | string | Revised name |

## Relevant Database Tables
- **Specialization**: SpecializationCode (PK), SpecializationName

## Entity Relationships
- Direct update on `Specialization` table.

## Required Operations
1. Check `string.IsNullOrWhiteSpace(newSpecializationName)`; if true return `false`.
2. Locate specialization using `context.Specializations.Find(specializationCode)`.
3. If not found, return `false`.
4. Update `SpecializationName` and call `SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Specialization code must exist.
2. Name must not be empty.

## Required IF-ELSE Conditions
- **Condition:** If `spec == null || string.IsNullOrWhiteSpace(newSpecializationName)` return `false`; else update and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Not found / invalid name | `false` |
| Successfully updated | `true` |
| Exception caught | `false` |

## Exception Handling Requirements
Try-catch block returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
