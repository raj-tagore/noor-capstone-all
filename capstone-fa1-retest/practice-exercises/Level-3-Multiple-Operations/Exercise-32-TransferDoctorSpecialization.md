# Exercise 32: TransferDoctorSpecialization

## Business Scenario
A physician changes their recognized board certification from one discipline to another. The system must remove the old certification mapping and establish the new certification mapping in `DoctorSpecialization`.

## Problem Statement
Write a repository method `TransferDoctorSpecialization` accepting `doctorId`, `oldSpecCode`, and `newSpecCode`.
Execute three LINQ validations:
1. Find old record in `DoctorSpecialization` for `(doctorId, oldSpecCode)`. If not found, return `-1`.
2. Check if `newSpecCode` exists in `Specialization` table. If not found, return `-2`.
3. Check if doctor already holds `newSpecCode` in `DoctorSpecialization`. If already holds, return `-3`.
If validations pass:
- Remove the old `DoctorSpecialization` record.
- Add a new `DoctorSpecialization` record with `(doctorId, newSpecCode, DateTime.Today)`.
- Save changes and return `1`.
- On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int TransferDoctorSpecialization(int doctorId, string oldSpecCode, string newSpecCode)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult TransferDoctorSpecialization(int doctorId, string oldSpecCode, string newSpecCode)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| oldSpecCode | string | Current specialization code |
| newSpecCode | string | Replacement specialization code |

## Relevant Database Tables
- **DoctorSpecialization**: DoctorID, SpecializationCode, SpecializationDate
- **Specialization**: SpecializationCode

## Entity Relationships
- Junction table `DoctorSpecialization`.

## Required Operations
1. Query old mapping; if null return `-1`.
2. Query target specialization; if null return `-2`.
3. Query new mapping; if already exists return `-3`.
4. Call `context.DoctorSpecializations.Remove(oldMapping)`.
5. Create new mapping with `newSpecCode` and today's date, call `Add()`.
6. Call `context.SaveChanges()` and return `1`.

## Business Rules and Validations
1. Old mapping must exist (-1).
2. New specialization must exist (-2).
3. New mapping must not already exist (-3).

## Required IF-ELSE Conditions
- **Condition 1:** If oldMapping == null -> -1.
- **Condition 2:** Else if targetSpec == null -> -2.
- **Condition 3:** Else if alreadyMapped != null -> -3.
- **Condition 4:** Else transfer and return 1.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Old mapping missing | `-1` |
| New specialization invalid | `-2` |
| Already holds new specialization | `-3` |
| Success | `1` |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
