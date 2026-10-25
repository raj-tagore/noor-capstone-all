# 14 — FetchAllAppointments

When you need this: TVF via `FromSqlRaw`. Primer: [06](06-sqlparameter-and-raw-sql.md).

## Spec

Call `ufn_FetchAllAppointments`; return `List<ListOfAppointments>`; on exception return **`null`**.

## Line-by-line

```csharp
public List<ListOfAppointments> FetchAllAppointments(string doctorId, DateTime date)
{
    List<ListOfAppointments> lstAppointments;
    try
    {
        SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
        SqlParameter prmDate = new SqlParameter("@DateofAppointment", date.Date);
        lstAppointments = context.ListOfAppointments
            .FromSqlRaw(
                "SELECT * FROM dbo.ufn_FetchAllAppointments(@DoctorID, @DateofAppointment)",
                prmDoctorId, prmDate)
            .AsNoTracking()
            .ToList();
    }
    catch (Exception)
    {
        lstAppointments = null;
    }
    return lstAppointments;
}
```

| Piece | Why |
|---|---|
| `date.Date` | match SQL `DATE` |
| `ListOfAppointments` DbSet + `HasNoKey` | map result shape |
| `AsNoTracking` | read-only list |
| `ToList` | execute now |
| catch → `null` | PS: not empty list |

Empty success is a non-null empty list; that is different from exception → `null`.

## Exam variations

1. Parameter names must match function definition.
2. `FromSqlInterpolated`.
3. Console must null-check before `foreach` if exceptions are possible.
