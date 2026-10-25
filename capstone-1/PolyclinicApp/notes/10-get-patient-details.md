# 10 — GetPatientDetails

When you need this: LINQ fetch-one-or-null pattern. Primers: [02](02-try-catch.md), [03](03-linq.md), [04](04-first-or-default.md).

## Spec

Return `Patient` for `patientId`; on exception return `null`.

## Line-by-line

```csharp
public Patient GetPatientDetails(string patientId)
{
    Patient patient = null;                    // 1 default = failure/empty
    try
    {
        patient = (from p in context.Patients  // 2 query source
                   where p.PatientId == patientId
                   select p).FirstOrDefault(); // 3 execute: null if missing
    }
    catch (Exception)
    {
        patient = null;                        // 4 exception → null
    }
    return patient;                            // 5
}
```

| Step | Role |
|---|---|
| 1 | Init before try so catch/return always defined |
| 2–3 | LINQ as PS asked; `FirstOrDefault` if no row |
| 4 | Swallow + null |
| 5 | Success or null |

## Exam variations

1. `context.Patients.Find(patientId)`.
2. Method syntax: `context.Patients.FirstOrDefault(p => p.PatientId == patientId)`.
3. Missing id without exception already returns `null` via `FirstOrDefault` — catch is for DB failures.
