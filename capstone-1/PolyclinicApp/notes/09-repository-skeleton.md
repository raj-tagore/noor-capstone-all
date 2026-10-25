# 09 — PolyclinicRepository skeleton

When you need this: match the class diagram before filling method bodies. File: `PolyclinicDAL/PolyclinicRepository.cs`.

## Syntax

```csharp
public class PolyclinicRepository
{
    PolyclinicDbContext context;   // field — private by default

    public PolyclinicRepository(PolyclinicDbContext context)
    {
        this.context = context;
    }

    // methods…
}
```

## Where each part goes

| Diagram | Code |
|---|---|
| `context` member | field (not `public` unless PS requires) |
| ctor parameter | assign to field |
| methods | signatures **exact** |

## Method index → notes

| Method | Note file |
|---|---|
| `GetPatientDetails` | [10](10-get-patient-details.md) |
| `AddNewPatientDetails` | [11](11-add-new-patient-details.md) |
| `UpdatePatientAge` | [12](12-update-patient-age.md) |
| `CancelAppointment` | [13](13-cancel-appointment.md) |
| `FetchAllAppointments` | [14](14-fetch-all-appointments.md) |
| `CalculateDoctorFees` | [15](15-calculate-doctor-fees.md) |
| `GetDoctorAppointment` | [16](16-get-doctor-appointment.md) |

## Usings this file needs

```csharp
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PolyclinicDAL.Models;
```

## Exam variations

1. A public property `Context` is not required. The diagram shows a field.
2. If the diagram’s constructor takes a context, assign the parameter. Do not `new` a second context inside the repository.
