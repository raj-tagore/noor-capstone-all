# Exercise 72: AddSurgeryViaStoredProcWithControllerLogic

## Business Scenario
In Web API tier design, the repository returns technical status codes, while the controller translates those codes into client-friendly JSON responses with meaningful payload messages.

## Problem Statement
Repository requirement:
Implement `AddSurgeryViaStoredProcWithControllerLogic` taking surgery parameters and returning status code `int` with `out int surgeryId` by calling `usp_AddSurgeryDetails`.

Controller requirement:
Implement controller action `AddSurgeryViaStoredProcWithControllerLogic(Models.Surgery surgery)`:
- Invoke repository method.
- If status == 1, return `Json(new { success = true, surgeryId = surgeryId, message = "Surgery booked successfully" })`.
- If status == -1, return `Json(new { success = false, errorCode = -1, message = "Invalid doctor ID" })`.
- If status == -2, return `Json(new { success = false, errorCode = -2, message = "Invalid specialization category" })`.
- If status == -3, return `Json(new { success = false, errorCode = -3, message = "Doctor is not certified for this category" })`.
- If status == -4, return `Json(new { success = false, errorCode = -4, message = "Start time must be strictly before end time" })`.
- If status == -5, return `Json(new { success = false, errorCode = -5, message = "Surgery slot conflicts with existing booking" })`.
- If status == -6, return `Json(new { success = false, errorCode = -6, message = "Cannot schedule surgeries in the past" })`.
- On error/exception, return `Json(new { success = false, errorCode = -98, message = "Server error occurred" })`.

## DAL / Repository Method Signature
```csharp
public int AddSurgeryViaStoredProcWithControllerLogic(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
```

## ASP.NET Web API Controller Requirement
Create an `[HttpPost]` action in `CureHospitalController` implementing the full response translation matrix.

## Suggested Controller Method Signature
```csharp
[HttpPost]
public JsonResult AddSurgeryViaStoredProcWithControllerLogic(Models.Surgery surgery)
```

## Input Parameters
| Parameter | Type | Description |
|-----------|------|-------------|
| surgery | Models.Surgery | Surgery payload |

## Relevant Database Tables
- Stored Procedure: `usp_AddSurgeryDetails`

## Entity Relationships
- Client -> Controller -> Repository -> Stored Procedure.

## Required Operations
1. Repository invokes SP.
2. Controller evaluates status code through a switch or if-else ladder.
3. Controller returns structured JSON response.

## Business Rules and Validations
1. Every SP error code has an explicit user-facing message.

## Required IF-ELSE Conditions
- Full conditional branch in controller mapping return codes 1, -1, -2, -3, -4, -5, -6, -98.

## Expected Return Values
| Status Code | Controller Response |
|-------------|---------------------|
| `1` | Success JSON with surgeryId |
| `< 0` | Failure JSON with specific message |

## Exception Handling Requirements
Try-catch in DAL and Controller returning appropriate error JSON.

## What Needs to Be Implemented
- [ ] Repository method in `CureHospitalRepositoryEF`
- [ ] Controller action in `CureHospitalController`
