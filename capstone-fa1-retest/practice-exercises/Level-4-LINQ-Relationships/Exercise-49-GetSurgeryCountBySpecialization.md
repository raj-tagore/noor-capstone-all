# Exercise 49: GetSurgeryCountBySpecialization

## Business Scenario
Department heads need aggregated metrics detailing the total number of surgeries performed under each clinical category code.

## Problem Statement
Define a DTO class `SpecializationSurgeryCount`:
- `string SpecializationCode { get; set; }`
- `int SurgeryCount { get; set; }`

Write a repository method `GetSurgeryCountBySpecialization`.
Using LINQ Query Syntax with `group by`:
```csharp
from s in context.Surgeries
group s by s.SurgeryCategory into g
select new SpecializationSurgeryCount
{
    SpecializationCode = g.Key,
    SurgeryCount = g.Count()
}
```
Return `List<SpecializationSurgeryCount>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<SpecializationSurgeryCount> GetSurgeryCountBySpecialization()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetSurgeryCountBySpecialization()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Surgery**: SurgeryID, SurgeryCategory

## Entity Relationships
- Grouping on `SurgeryCategory`.

## Required Operations
1. Create `SpecializationSurgeryCount` DTO.
2. Group surgeries by category via LINQ Query Syntax.
3. Project key and count into DTO.
4. Return `.ToList()`.

## Business Rules and Validations
1. Group by query syntax.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Aggregated groups | `List<SpecializationSurgeryCount>` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `SpecializationSurgeryCount`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
