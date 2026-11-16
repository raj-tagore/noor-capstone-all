# Exercise 12: AddSpecializationWithDuplicateCheck

## Business Scenario
Hospital departments introduce clinical specialization categories into the system. Each specialization is governed by a 3-character unique code (e.g., GYN, CAR). The administration needs to ensure duplicate specialization codes are rejected before database insertion.

## Problem Statement
Write a repository method named `AddSpecializationWithDuplicateCheck` that takes `specializationCode` (string) and `specializationName` (string). Use Entity Framework's `Find()` method or a LINQ query to verify if the primary key `SpecializationCode` already exists. If it exists, return `false`. If not, create a new `Specialization` instance, assign properties, add to `DbSet<Specialization>`, call `SaveChanges()`, and return `true`.

## DAL / Repository Method Signature
```csharp
public bool AddSpecializationWithDuplicateCheck(string specializationCode, string specializationName)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action method named `AddSpecializationWithDuplicateCheck` in `CureHospitalController` accepting the two parameters, invoking the repository, and returning `JsonResult` wrapping the boolean result.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSpecializationWithDuplicateCheck(string specializationCode, string specializationName)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationCode | string | 3-character primary key code (e.g., "PED") |
| specializationName | string | Descriptive title (e.g., "Pediatrician") |

## Relevant Database Tables
- **Specialization**: SpecializationCode (CHAR(3), PK), SpecializationName (VARCHAR(50), NOT NULL, UNIQUE)

## Entity Relationships
- `Specialization` has primary key `SpecializationCode`.

## Required Operations
1. Use `context.Specializations.Find(specializationCode)` to search for existing record.
2. If record is found (not null), return `false`.
3. If not found, instantiate `Specialization`, assign `SpecializationCode` and `SpecializationName`.
4. Call `Add()` and `SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. `SpecializationCode` must not exist prior to insertion.
2. Property assignment must assign both code and name.

## Required IF-ELSE Conditions
- **Condition:** If `existingSpec != null`, return `false`; else insert and return `true`.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Specialization code exists | `false` |
| Specialization successfully created | `true` |
| Database/runtime exception | `false` |

## Exception Handling Requirements
All database logic enclosed in try-catch; catch block returns `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
