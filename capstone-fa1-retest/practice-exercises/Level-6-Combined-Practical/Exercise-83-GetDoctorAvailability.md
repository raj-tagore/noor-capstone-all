# Exercise 73: GetDoctorAvailability
# Exercise 83: GetDoctorAvailability

## Business Scenario
Operating theatre managers evaluate open operating windows for a doctor during standard operating hours (8:00 AM to 8:00 PM, 12 available hours) to schedule additional procedures.

## Problem Statement
Define a DTO class `DoctorAvailability`:
- `int DoctorId { get; set; }`
- `string DoctorName { get; set; }`
- `DateTime SurgeryDate { get; set; }`
- `int SurgeryCount { get; set; }`
- `decimal TotalBookedHours { get; set; }`
- `decimal AvailableHours { get; set; }`

Write a repository method `GetDoctorAvailability` taking `doctorId` and `surgeryDate`.
1. Find doctor via `context.Doctors.Find(doctorId)`. If null, return `null`.
2. Retrieve doctor's surgeries on `surgeryDate` using LINQ Query Syntax.
3. Compute `TotalBookedHours = surgeries.Sum(s => s.EndTime - s.StartTime)`.
4. Compute `AvailableHours = 12.00m - TotalBookedHours` (clamped to 0 if negative).
5. Assemble and return `DoctorAvailability` DTO. Return `null` on exception.

## DAL / Repository Method Signature
```csharp
public DoctorAvailability GetDoctorAvailability(int doctorId, DateTime surgeryDate)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpGet]` action in `CureHospitalController` returning `JsonResult`.

## Suggested Controller Method Signature
```csharp
[HttpGet]
public JsonResult GetDoctorAvailability(int doctorId, DateTime surgeryDate)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| doctorId | int | Doctor ID |
| surgeryDate | DateTime | Workday date |

## Relevant Database Tables
- **Doctor**: DoctorID, DoctorName
- **Surgery**: SurgeryID, DoctorID, SurgeryDate, StartTime, EndTime

## Entity Relationships
- Direct calculation over `Doctor` and related `Surgery` records.

## Required Operations
1. Create `DoctorAvailability` DTO.
2. Locate doctor; return null if missing.
3. Fetch surgeries for date.
4. Calculate booked hours and available hours.
5. Return DTO.

## Business Rules and Validations
1. Base operating day is 12 hours (8.00 to 20.00).

## Required IF-ELSE Conditions
- **Condition:** If `doctor == null` return `null`; else compute and return DTO.

## Expected Return Values
| Scenario | Return Value |
|----------|-------------|
| Doctor found | `DoctorAvailability` DTO |
| Doctor not found | `null` |
| Exception | `null` |

## Exception Handling Requirements
Try-catch returning `null`.

## What Needs to Be Implemented
- [ ] DTO class `DoctorAvailability`
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
