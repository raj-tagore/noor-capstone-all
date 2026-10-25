# 03 — LINQ (query and method syntax)

When you need this: PS says “fetch using a LINQ query” against `context.Patients` / `Appointments`.

## Syntax — query

```csharp
var q =
    from row in context.Patients
    where row.PatientId == patientId
    select row;
```

With join / order:

```csharp
from a in context.Appointments
join p in context.Patients on a.PatientId equals p.PatientId
where a.DoctorId == doctorId
orderby a.AppointmentNo
select new { a.AppointmentNo, p.PatientName };
```

## Syntax — method (equivalent ideas)

```csharp
context.Patients.Where(p => p.PatientId == patientId).Select(p => p);
context.Patients.FirstOrDefault(p => p.PatientId == patientId);
```

## Where each clause goes

| Clause | Meaning |
|---|---|
| `from x in source` | range variable over `IQueryable`/`IEnumerable` |
| `where` | filter → SQL `WHERE` when EF translates |
| `select` | projection (entity or anonymous/DTO) |
| `join … on … equals` | inner join |
| `orderby` | sort |

Execution is **deferred** until you force it: `FirstOrDefault()`, `ToList()`, `Count()`, `foreach`, etc. That is when SQL hits the database.

## This project

Query syntax + terminator:

```csharp
patient = (from p in context.Patients
           where p.PatientId == patientId
           select p).FirstOrDefault();
```

Same pattern for appointments in `CancelAppointment` / `UpdatePatientAge`.

## Exam variations

1. Method syntax only: `context.Patients.FirstOrDefault(p => p.PatientId == patientId)`.
2. Many rows: `.ToList()` instead of `FirstOrDefault`.
3. Project columns: `select new ListOfAppointments { … }` (rare if TVF already returns shape).
4. `Find(pk)` is **not** LINQ query syntax — see [04](04-first-or-default.md). If PS says “LINQ query”, prefer `from`/`where`/`select`.
