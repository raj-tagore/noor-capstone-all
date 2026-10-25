# 12 — UpdatePatientAge

When you need this: load → mutate → SaveChanges. Primers: [03](03-linq.md), [05](05-dbset-add-remove-save.md).

## Spec

Signature uses **`byte newAge`**. Return `true` if updated, `false` if missing or exception.

## Line-by-line

```csharp
public bool UpdatePatientAge(string patientId, byte newAge)
{
    bool status = false;
    try
    {
        Patient patient = (from p in context.Patients
                           where p.PatientId == patientId
                           select p).FirstOrDefault();
        if (patient != null)              // soft miss → false, not exception
        {
            patient.Age = newAge;         // tracked property change
            context.SaveChanges();        // UPDATE
            status = true;
        }
    }
    catch (Exception)
    {
        status = false;
    }
    return status;
}
```

## Trap in the provided console test

`UpdatePatientAge("P111", 53)` → seed has no `P111` → returns `false`. Use `P104` (etc.) to see success.

## Exam variations

1. Parameter type `int age` then cast `(byte)age` — only if signature allows.
2. `Find` + null check.
3. Execute raw `UPDATE Patients SET Age=…` — usually not what EF PSs want.
