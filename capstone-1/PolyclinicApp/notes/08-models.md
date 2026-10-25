# 08 — Models

When you need this: property names and types on the generated entities, plus the TVF result class you add yourself.

## Generated entities (tables)

**Patient** — `PatientId`, `PatientName`, `Age` (`byte`), `Gender`, `ContactNumber`.

**Doctor** — `DoctorId`, `Specialization`, `DoctorName`, `Fees` (`decimal`).

**Appointment** — `AppointmentNo`, `PatientId`, `DoctorId`, `DateofAppointment` (`DateOnly`).

`CHAR`/`VARCHAR` are `string`. Use these property names in LINQ (`p.PatientId`, not `p.PatientID`).

## Hand-made: ListOfAppointments

TVF returns: `DoctorName`, `Specialization`, `PatientID`, `PatientName`, `AppointmentNo`.

```csharp
public class ListOfAppointments
{
    public string DoctorName { get; set; }
    public string Specialization { get; set; }
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public int AppointmentNo { get; set; }
}
```

Must be registered on context with **`HasNoKey()`** — not a table.

## Diagram names vs C#

| PS / diagram | C# type |
|---|---|
| Patients | `Patient` |
| Doctors | `Doctor` |
| Appointments | `Appointment` |
| ListOfAppointments | class you add, same name |

## Exam variations

1. Diagram says `Patients` — the generated class is still `Patient`.
2. Extra TVF columns — add matching properties and `HasNoKey()` stays.
3. `DateofAppointment` is `DateOnly`; compare with `DateOnly.FromDateTime(date)` in LINQ, or pass `date.Date` into SQL parameters.
