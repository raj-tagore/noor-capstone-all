# Exercise 85: GetComprehensiveHospitalReport

## Business Scenario
Hospital executive directors request a comprehensive hospital summary report detailing overall medical staff volume, surgical activity, and upcoming caseloads without requiring any input parameters.

## Problem Statement
Define a DTO class `HospitalReport`:
- `int TotalDoctors { get; set; }`
- `int TotalSpecializations { get; set; }`
- `int TotalSurgeries { get; set; }`
- `int UpcomingSurgeries { get; set; }`
- `string BusiestDoctorName { get; set; }`
- `string MostCommonSpecialization { get; set; }`

Write a repository method `GetComprehensiveHospitalReport`.
Using LINQ Query Syntax:
1. `TotalDoctors`: count from `context.Doctors`.
2. `TotalSpecializations`: count from `context.Specializations`.
3. `TotalSurgeries`: count from `context.Surgeries`.
4. `UpcomingSurgeries`: count from `context.Surgeries where s.SurgeryDate >= DateTime.Today`.
5. `BusiestDoctorName`: group surgeries by `DoctorId`, find the group with max count, join with `Doctor` to retrieve `DoctorName`.
6. `MostCommonSpecialization`: group `DoctorSpecialization` by `SpecializationCode`, find max count, join with `Specialization` to retrieve `SpecializationName`.
7. Assemble and return `HospitalReport` DTO. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public HospitalReport GetComprehensiveHospitalReport()
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetComprehensiveHospitalReport()
```

## Input Parameters
- None.

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Specialization**: SpecializationCode, SpecializationName
- **DoctorSpecialization**: DoctorID, SpecializationCode
- **Surgery**: SurgeryID, DoctorID, SurgeryDate

## Entity Relationships
- Comprehensive aggregation spanning all four database tables.

## Required Operations
1. Create `HospitalReport` DTO.
2. Execute LINQ Query Syntax aggregations.
3. Compute busiest doctor and common specialization.
4. Return populated DTO.

## Business Rules and Validations
1. Parameterless executive summary.

## Required IF-ELSE Conditions
- None.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Report generated | `HospitalReport` DTO |
| Exception | `null` |

## Exception Handling Requirements
Try-catch block returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `HospitalReport`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
