# Exercise 51: GetDoctorsWithSurgeryOnDate

## Business Scenario
Operating theatre security requires a list of doctor names scheduled for surgical interventions on any given day.

## Problem Statement
Write a repository method `GetDoctorsWithSurgeryOnDate` accepting `surgeryDate`.
Using LINQ Query Syntax, join `Surgery` and `Doctor`, filter by `surgeryDate`, and select doctor names with distinct filtering:
```csharp
(from s in context.Surgeries
 join d in context.Doctors on s.DoctorId equals d.DoctorId
 where s.SurgeryDate == surgeryDate
 select d.DoctorName).Distinct().ToList()
```
Return `List<string>`. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public List<string> GetDoctorsWithSurgeryOnDate(DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult` wrapping the list of strings.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorsWithSurgeryOnDate(DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgeryDate | DateTime | Scheduled date |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, SurgeryDate

## Entity Relationships
- `Surgery` joined to `Doctor`.

## Required Operations
1. Join tables in Query Syntax.
2. Filter on date.
3. Select `d.DoctorName`.
4. Apply `.Distinct().ToList()`.

## Business Rules and Validations
1. Distinct doctor names.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Names found | `List<string>` |
| None found | Empty list |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
