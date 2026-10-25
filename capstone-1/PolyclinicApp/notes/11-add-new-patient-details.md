# 11 — AddNewPatientDetails

When you need this: insert via DbSet. Primers: [05](05-dbset-add-remove-save.md), [02](02-try-catch.md).

## Spec

Insert into `Patients`; `true` on success, `false` on failure/exception.

## Line-by-line

```csharp
public bool AddNewPatientDetails(Patient patientObj)
{
    bool status = false;                 // 1 assume fail
    try
    {
        context.Patients.Add(patientObj); // 2 stage INSERT
        context.SaveChanges();            // 3 send to SQL
        status = true;                    // 4 only if no throw
    }
    catch (Exception)
    {
        status = false;                   // 5 PK clash, check fail, etc.
    }
    return status;
}
```

| Step | Role |
|---|---|
| `Add` | track as Added |
| `SaveChanges` | `INSERT`; throws on constraint violation (e.g. duplicate `PatientId`, bad `Gender`) |
| `status = true` | after successful save only |

Caller builds the entity (see console): set `PatientId`, `PatientName`, `Age`, `Gender`, `ContactNumber`.

## Exam variations

1. Validate `PatientId` starts with `P` in C# before Add.
2. Return `SaveChanges() > 0`.
3. `AddRange` for multiple patients.
