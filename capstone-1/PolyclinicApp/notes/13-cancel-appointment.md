# 13 — CancelAppointment

When you need this: delete with three-way return codes. Primers: [05](05-dbset-add-remove-save.md), [02](02-try-catch.md).

## Spec

Remove by `AppointmentNo`. Return **`1`** success, **`-1`** not found, **`-99`** exception.

## Line-by-line

```csharp
public int CancelAppointment(int appointmentNo)
{
    int result;
    try
    {
        Appointment appointment = (from a in context.Appointments
                                   where a.AppointmentNo == appointmentNo
                                   select a).FirstOrDefault();
        if (appointment != null)
        {
            context.Appointments.Remove(appointment);
            context.SaveChanges();
            result = 1;
        }
        else
        {
            result = -1;                  // soft fail inside try
        }
    }
    catch (Exception)
    {
        result = -99;                     // hard fail
    }
    return result;
}
```

| Code | Meaning |
|---|---|
| `1` | deleted |
| `-1` | no such appointment |
| `-99` | exception |

Do **not** collapse “not found” into `-99`.

## Exam variations

1. Return `bool` instead — only if PS says so.
2. `SaveChanges()` return value as success metric.
3. Console test uses appointment `9` — exists after fresh seed; second run may get `-1`.
