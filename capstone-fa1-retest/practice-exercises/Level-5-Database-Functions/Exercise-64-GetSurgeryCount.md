# Exercise 64: GetSurgeryCount

## Business Scenario
Hospital management monitors specialty load by invoking the database scalar function `ufn_FetchSurgeryCount` via ADO.NET from the EF repository.

## Problem Statement
Write a repository method `GetSurgeryCount` taking `specializationName` (string) and `surgeryDate` (DateTime).
Execute the scalar function `ufn_FetchSurgeryCount` using ADO.NET through EF Core's underlying connection:
1. Obtain connection from `context.Database.GetDbConnection()`.
2. If connection is not Open, open it.
3. Create a command with command text: `"SELECT dbo.ufn_FetchSurgeryCount(@name, @date)"`.
4. Add parameters `@name` and `@date`.
5. Execute via `command.ExecuteScalar()` and convert result to `int`.
6. Return the integer count. On exception, return `-1`.

## DAL / Repository Method Signature
```csharp
public int GetSurgeryCount(string specializationName, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` taking `specializationName` and `surgeryDate` as query parameters and returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCount(string specializationName, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| specializationName | string | Specialization name (e.g. "Cardiologist") |
| surgeryDate | DateTime | Scheduled date |

## Relevant Database Tables
- Scalar Function: `ufn_FetchSurgeryCount(@SpecializationName VARCHAR(50), @SurgeryDate DATE) RETURNS INT`

## Entity Relationships
- Function computes count of surgeries matching specialization name and date.

## Required Operations
1. Open DB connection if needed.
2. Setup command and parameters.
3. Execute `ExecuteScalar()`.
4. Convert to integer and return.

## Business Rules and Validations
1. Standard ADO.NET scalar invocation pattern.

## Required IF-ELSE Conditions
- Connection state checking (`ConnectionState.Open`).

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Count executed | Integer count >= 0 |
| Exception | `-1` |

## Exception Handling Requirements
Try-catch block returning `-1`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
