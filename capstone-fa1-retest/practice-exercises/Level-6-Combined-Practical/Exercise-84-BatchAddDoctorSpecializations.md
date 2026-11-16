# Exercise 84: BatchAddDoctorSpecializations

## Business Scenario
During physician credentialing onboardings, a doctor is assigned multiple specialization credentials simultaneously via a list of specialization codes.

## Problem Statement
Write a repository method `BatchAddDoctorSpecializations` taking `doctorId` and `List<string> specializationCodes`.
Execution pipeline:
1. Verify `doctorId` exists in `Doctor`. If not, return `-1`.
2. Initialize `int addedCount = 0`.
3. Loop through each `code` in `specializationCodes`:
   - Check if `code` exists in `Specialization` table.
   - Check if `(doctorId, code)` already exists in `DoctorSpecialization`.
   - If `code` exists and is NOT duplicate, create a new `DoctorSpecialization` with `(doctorId, code, DateTime.Today)`, add to context, and increment `addedCount`.
4. Call `context.SaveChanges()` once after the loop.
5. Return `addedCount`.
6. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int BatchAddDoctorSpecializations(int doctorId, List<string> specializationCodes)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` accepting `doctorId` and `List<string> specializationCodes` in the request body, returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult BatchAddDoctorSpecializations(int doctorId, [FromBody] List<string> specializationCodes)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| specializationCodes | List\<string\> | List of 3-character specialty codes |

## Relevant Database Tables
- **Doctor**: DoctorID
- **Specialization**: SpecializationCode
- **DoctorSpecialization**: DoctorID, SpecializationCode, SpecializationDate

## Entity Relationships
- Batch operations on `DoctorSpecialization`.

## Required Operations
1. Verify doctor (-1 if missing).
2. Loop over codes list.
3. Validate existence and non-duplicate status for each code.
4. Add valid items.
5. Save changes in single batch call and return count.

## Business Rules and Validations
1. Doctor must exist (-1).
2. Skip invalid or duplicate codes; add valid ones.

## Required IF-ELSE Conditions
- **Condition 1:** If doctor not found -> return -1.
- **Condition 2 (inside loop):** If valid code and not duplicate -> add to context and increment count.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor not found | `-1` |
| Valid additions | Count of added specializations (>= 0) |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
