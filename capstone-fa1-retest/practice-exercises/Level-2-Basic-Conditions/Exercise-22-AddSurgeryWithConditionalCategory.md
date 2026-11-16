# Exercise 22: AddSurgeryWithConditionalCategory

## Business Scenario
In high-volume intake, elective surgical bookings that lack a designated category default to general gynecology ("GYN"). The system must perform conditional assignment on incoming data before saving.

## Problem Statement
Write a repository method `AddSurgeryWithConditionalCategory` that takes `doctorId`, `surgeryDate`, `startTime`, `endTime`, and `surgeryCategory`.
Business logic:
- If `surgeryCategory` is null, empty, or whitespace, assign it the default value `"GYN"`.
- Next, verify if `doctorId` exists in `Doctor`. If not found, return `false`.
- Create a `Surgery` record with the conditionally assigned category, add to context, call `SaveChanges()`, and return `true`.
- On exception, return `false`.

## DAL / Repository Method Signature
```csharp
public bool AddSurgeryWithConditionalCategory(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` returning `JsonResult` wrapping the boolean status.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryWithConditionalCategory(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Surgeon ID |
| surgeryDate | DateTime | Scheduled date |
| startTime | decimal | Start hour |
| endTime | decimal | End hour |
| surgeryCategory | string | Category (can be null/empty) |

## Relevant Database Tables
- **Doctor**: DoctorID (PK)
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime, SurgeryCategory

## Entity Relationships
- `Surgery` references `Doctor(DoctorID)`.

## Required Operations
1. Evaluate if `string.IsNullOrWhiteSpace(surgeryCategory)`; if so, set category to `"GYN"`.
2. Locate doctor with `context.Doctors.Find(doctorId)`. If null, return `false`.
3. Create `Surgery` entity with the assigned category.
4. Add to DbSet and call `SaveChanges()`.
5. Return `true`.

## Business Rules and Validations
1. Default category to "GYN" if omitted.
2. Doctor must exist.

## Required IF-ELSE Conditions
- **Condition 1:** If category is null/empty -> category = "GYN".
- **Condition 2:** If doctor not found -> return false.
- **Condition 3:** Else insert and return true.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor does not exist | `false` |
| Successfully inserted | `true` |
| Exception caught | `false` |

## Exception Handling Requirements
Try-catch block returning `false`.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
