# 15 — CalculateDoctorFees

When you need this: scalar UDF. Primer: [06](06-sqlparameter-and-raw-sql.md).

## Spec

Call `ufn_CalculateDoctorFees`; return its `MONEY` value; on exception return **`-99`**.

## Line-by-line

```csharp
public decimal CalculateDoctorFees(string doctorId, DateTime date)
{
    decimal fees;
    try
    {
        SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
        SqlParameter prmDate = new SqlParameter("@DateofAppointment", date.Date);
        SqlParameter prmResult = new SqlParameter("@Result", SqlDbType.Money)
        {
            Direction = ParameterDirection.Output
        };

        context.Database.ExecuteSqlRaw(
            "SET @Result = dbo.ufn_CalculateDoctorFees(@DoctorID, @DateofAppointment)",
            prmDoctorId, prmDate, prmResult);

        if (prmResult.Value == DBNull.Value)
            fees = 0;                              // SUM of no rows → NULL in SQL
        else
            fees = Convert.ToDecimal(prmResult.Value);
    }
    catch (Exception)
    {
        fees = -99;
    }
    return fees;
}
```

| Piece | Why |
|---|---|
| `SqlDbType.Money` | matches UDF return |
| `OUTPUT` + `SET @Result = dbo.fn(...)` | pull scalar into C# |
| `DBNull` → `0` | no appointments that day |
| `-99` | only on exception |

Clinic formula in SQL: `SUM(Fees * 0.6)` for that doctor/date.

## Exam variations

1. `SqlQueryRaw<decimal?>("SELECT dbo.ufn_…({0},{1})", …)`.
2. Treat null as `-99` (would disagree with “exception only” wording).
3. Console treats `result > 0` as success — zero fees prints the error message even when call succeeded.
