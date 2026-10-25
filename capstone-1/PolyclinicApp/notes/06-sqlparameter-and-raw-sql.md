# 06 — SqlParameter, FromSqlRaw, ExecuteSqlRaw

When you need this: call a stored procedure, scalar UDF, or table-valued function from EF.

## Syntax — parameter

```csharp
var p = new SqlParameter("@DoctorID", doctorId);

var outP = new SqlParameter("@AppointmentNo", SqlDbType.Int)
{
    Direction = ParameterDirection.Output
};

var ret = new SqlParameter("@ReturnValue", SqlDbType.Int)
{
    Direction = ParameterDirection.Output   // capture SP return via EXEC @ReturnValue = …
};
```

Read output after execute:

```csharp
int n = outP.Value == DBNull.Value ? 0 : Convert.ToInt32(outP.Value);
```

## Syntax — TVF / raw query → entities

```csharp
context.ListOfAppointments
    .FromSqlRaw(
        "SELECT * FROM dbo.ufn_FetchAllAppointments(@DoctorID, @DateofAppointment)",
        prmDoctorId, prmDate)
    .AsNoTracking()
    .ToList();
```

Requires a `DbSet<ListOfAppointments>` mapped with `HasNoKey()` (or a key that matches).

## Syntax — scalar UDF via output

```csharp
context.Database.ExecuteSqlRaw(
    "SET @Result = dbo.ufn_CalculateDoctorFees(@DoctorID, @DateofAppointment)",
    prmDoctorId, prmDate, prmResult);
```

## Syntax — stored procedure with return + OUTPUT

```csharp
context.Database.ExecuteSqlRaw(
    "EXEC @ReturnValue = usp_GetDoctorAppointment @PatientID, @DoctorID, @DateOfAppointment, @AppointmentNo OUTPUT",
    prmReturnValue, prmPatientId, prmDoctorId, prmDate, prmAppointmentNo);
```

## Where each piece goes

| Piece | Role |
|---|---|
| `@Name` in SQL | must match `SqlParameter` name |
| `Direction.Output` | SP/UDF writes back into `.Value` |
| `FromSqlRaw` | must compose as a queryable of mapped type |
| `ExecuteSqlRaw` | no result set (or you ignore it); good for EXEC/SET |
| `AsNoTracking` | read-only; no change tracker cost |
| `date.Date` | strip time for `DATE` columns |

Namespaces: `Microsoft.Data.SqlClient`, `System.Data`, `Microsoft.EntityFrameworkCore`.

## This project

| Method | API |
|---|---|
| `FetchAllAppointments` | `FromSqlRaw` + TVF |
| `CalculateDoctorFees` | `ExecuteSqlRaw` + `SET @Result = dbo.fn` |
| `GetDoctorAppointment` | `ExecuteSqlRaw` + `EXEC @ReturnValue = usp_… OUTPUT` |

## Exam variations

1. `FromSqlInterpolated($"SELECT * FROM dbo.fn({id}, {date})")` — safer interpolation.
2. `SqlQueryRaw<decimal>` (EF 8+) for scalar without OUTPUT param.
3. `Direction.ReturnValue` with ADO `SqlCommand` — different from the `EXEC @Ret =` EF pattern above.
4. Missing `OUTPUT` keyword in SQL string → out param never fills.
5. Wrong parameter name vs SQL → runtime error / null result.
