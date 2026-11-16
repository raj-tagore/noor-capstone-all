# Exercise 77: TransferSurgeriesToNewDoctor

## Business Scenario
When a surgeon falls ill on an operating day, hospital administration transfers all of their scheduled surgeries on that date to a qualified substitute doctor, ensuring the substitute has no time conflicts.

## Problem Statement
Write a repository method `TransferSurgeriesToNewDoctor` taking `oldDoctorId`, `newDoctorId`, and `surgeryDate`.
Execution pipeline:
1. Verify `oldDoctorId` exists in `Doctor`. If not, return `-1`.
2. Verify `newDoctorId` exists in `Doctor`. If not, return `-2`.
3. Retrieve all surgeries for `oldDoctorId` on `surgeryDate` using LINQ Query Syntax. If none found, return `-3`.
4. Retrieve all existing surgeries for `newDoctorId` on `surgeryDate`.
5. Conflict check: Verify whether any surgery in the old doctor's list overlaps with any existing surgery of the new doctor (`old.StartTime < new.EndTime && old.EndTime > new.StartTime`). If any conflict exists, return `-4` without transferring.
6. If no conflict, iterate through the old surgeries using `foreach`, set `s.DoctorId = newDoctorId`, call `SaveChanges()`, and return the count of transferred surgeries.
7. On exception, return `-99`.

## DAL / Repository Method Signature
```csharp
public int TransferSurgeriesToNewDoctor(int oldDoctorId, int newDoctorId, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPut]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpPut]
public JsonResult TransferSurgeriesToNewDoctor(int oldDoctorId, int newDoctorId, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| oldDoctorId | int | Outgoing doctor ID |
| newDoctorId | int | Incoming doctor ID |
| surgeryDate | DateTime | Scheduled date |

## Relevant Database Tables
- **Doctor**: DoctorID
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime

## Entity Relationships
- Multi-entity lookups and batch update.

## Required Operations
1. Check old doctor (-1).
2. Check new doctor (-2).
3. Fetch old surgeries (-3 if empty).
4. Fetch new doctor's existing surgeries.
5. Nested loop / conflict comparison (-4 if overlap).
6. Reassign doctor ID and save changes.
7. Return count.

## Business Rules and Validations
1. All-or-nothing transfer: fail if any slot conflicts.

## Required IF-ELSE Conditions
- Validations 1 through 5.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Old doctor missing | `-1` |
| New doctor missing | `-2` |
| No surgeries to transfer | `-3` |
| Schedule conflict detected | `-4` |
| Successful transfer | Count of surgeries transferred |
| Exception | `-99` |

## Exception Handling Requirements
Try-catch block returning `-99`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
