# 17 — Console tester (already written)

When you need this: see which repository method each test calls, and how `out` is used. You do not build or run from the command line; Visual Studio does that.

## Wiring already in `Program`

```csharp
static PolyclinicDbContext context;
static PolyclinicRepository repository;

static Program()
{
    context = new PolyclinicDbContext();
    repository = new PolyclinicRepository(context);
}
```

## Which test calls which method

| Test | Call |
|---|---|
| `TestAddNewPatientDetails` | `AddNewPatientDetails` with a new `Patient` (`P113`) |
| `TestCalculateDoctorFees` | `CalculateDoctorFees("D2", DateTime.Today.AddDays(5))` |
| `TestCancelAppointment` | `CancelAppointment(9)` |
| `TestFetchAllAppointments` | `FetchAllAppointments("D2", DateTime.Today.AddDays(5))` |
| `TestGetDoctorAppointmentUsingUSP` | `GetDoctorAppointment(..., out appointmentNo)` |
| `TestGetPatientDetails` | `GetPatientDetails("P104")` |
| `TestUpdatePatientAge` | `UpdatePatientAge("P111", 53)` — `P111` is not in the seed data, so this returns `false` |

## `out` at the call site

```csharp
int appointmentNo = 0;
int returnResult = repository.GetDoctorAppointment("P105", "D1", DateTime.Today, out appointmentNo);
```

`returnResult` is the procedure’s return code. `appointmentNo` is filled by the `out` parameter.

## What the printed messages mean

- Fees test treats `result > 0` as success. A real `0` (no appointments that day) still prints the failure line.
- `FetchAllAppointments` returning `null` makes the tester’s `foreach` throw. An empty list does not.
- Running the add/cancel tests twice hits a duplicate `P113` and a missing appointment `9`.
