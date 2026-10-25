# 05 — DbSet: Add, Remove, SaveChanges

When you need this: insert / update / delete through EF instead of raw SQL.

## Syntax

```csharp
context.Patients.Add(entity);
context.Patients.AddRange(e1, e2);
context.Patients.Remove(entity);
context.Patients.RemoveRange(list);
context.Patients.Update(entity);          // mark whole entity modified
context.Patients.Find(pk);
context.Patients.Attach(entity);          // start tracking as Unchanged

int rows = context.SaveChanges();         // sends INSERT/UPDATE/DELETE
```

## Where each call goes

| Call | Effect when SaveChanges runs |
|---|---|
| `Add` | `INSERT` |
| property change on tracked entity | `UPDATE` |
| `Remove` | `DELETE` |
| `SaveChanges` | flush change tracker to DB; returns rows affected |

Typical insert:

```csharp
context.Patients.Add(patientObj);
context.SaveChanges();
```

Typical update:

```csharp
var row = (from …).FirstOrDefault();
if (row != null)
{
    row.Age = newAge;       // tracked → Modified
    context.SaveChanges();
}
```

Typical delete:

```csharp
context.Appointments.Remove(appointment);
context.SaveChanges();
```

## This project

- `AddNewPatientDetails` → `Add` + `SaveChanges`
- `UpdatePatientAge` → load → set property → `SaveChanges`
- `CancelAppointment` → load → `Remove` → `SaveChanges`; return `1` / `-1` / `-99`

## Exam variations

1. Check `SaveChanges() > 0` as success (optional; we use try/catch + bool).
2. `AddRange` for bulk insert.
3. `context.Entry(entity).State = EntityState.Modified` instead of property edit.
4. Delete without load: `context.Appointments.Remove(new Appointment { AppointmentNo = id })` (attach-by-key pattern) — works only if key is set correctly.
5. Forgetting `SaveChanges` → nothing hits the DB.
