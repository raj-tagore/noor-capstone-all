# 01 — Reading the method table

When you need this: copy signatures and return codes before writing method bodies. The project, database, and connection are already there.

## Copy exactly

| Diagram / table | Code |
|---|---|
| field `context : PolyclinicDbContext` | private field |
| `PolyclinicRepository(PolyclinicDbContext context)` | `this.context = context` |
| parameter types | keep `byte newAge`, `out int appointmentNo`, `List<ListOfAppointments>` |
| entity names | tables map to `Patient`, `Doctor`, `Appointment` (singular) |

## Return contract

| Method | Success | Soft failure | Exception |
|---|---|---|---|
| `GetPatientDetails` | `Patient` | — | `null` |
| `AddNewPatientDetails` | `true` | `false` | `false` |
| `UpdatePatientAge` | `true` | `false` (missing id) | `false` |
| `CancelAppointment` | `1` | `-1` (not found) | `-99` |
| `FetchAllAppointments` | `List<…>` | empty list ok | `null` |
| `CalculateDoctorFees` | function `MONEY` | `0` if SQL null | `-99` |
| `GetDoctorAppointment` | SP return (`1`,`2`,…) | SP negatives | `-99`, `out = 0` |

## Which API the wording picks

| PS says | Use |
|---|---|
| “using a LINQ query” | `from` / `where` / `select` on a `DbSet` |
| “using the table-valued function …” | `FromSqlRaw` |
| “using the scalar function …” | `ExecuteSqlRaw` + output parameter |
| “using the stored procedure …” | `ExecuteSqlRaw` + return/`out` |

## Naming

| Kind | Style | Example |
|---|---|---|
| Variables | camelCase | `patientId` |
| Methods | PascalCase | `GetPatientDetails` |
| Properties | PascalCase | `PatientName` |

Do not rename parameters. Do not ignore SP codes (`1` inserted, `2` already exists, `-1`/`-2`/`-3`).
