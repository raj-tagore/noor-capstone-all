# 02 — try / catch

When you need this: every repository method in this PS wraps DB work and maps failures to fixed return values.

## Syntax skeleton

```csharp
try
{
    // code that might throw
}
catch (ExceptionType name)
{
    // runs only if that type (or subclass) was thrown
}
finally
{
    // always runs (optional)
}
```

Multiple catches (most specific first):

```csharp
try { … }
catch (SqlException ex) { … }
catch (Exception ex) { … }
```

Empty catch (ignore exception object — common in exams):

```csharp
catch (Exception)
{
    status = false;
}
```

## Where each part goes

| Block | Put here |
|---|---|
| `try` | EF/LINQ/SQL calls, success assignments |
| `catch` | Failure return values (`false`, `null`, `-99`) |
| after try/catch | `return …` |

Pattern used everywhere in this repo:

```csharp
T result = /* default failure */;
try
{
    // work
    result = /* success */;
}
catch (Exception)
{
    result = /* failure per PS */;
}
return result;
```

## This project

| Method | catch sets |
|---|---|
| `GetPatientDetails` | `patient = null` |
| `Add` / `Update` | `status = false` |
| `CancelAppointment` | `result = -99` |
| `FetchAllAppointments` | `lstAppointments = null` |
| `CalculateDoctorFees` | `fees = -99` |
| `GetDoctorAppointment` | `appointmentNo = 0`, `returnValue = -99` |

## Exam variations

1. `catch (Exception ex) { Console.WriteLine(ex.Message); … }` — log then return.
2. Soft fail vs exception: missing row often handled with `if (entity == null)` **inside** try (`-1`), not catch (`-99`).
3. No `finally` needed for these methods.
4. Re-throw: `catch { throw; }` — almost never what these PSs want.
