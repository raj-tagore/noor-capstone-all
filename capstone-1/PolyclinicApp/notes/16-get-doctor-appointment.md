# 16 — GetDoctorAppointment

When you need this: stored procedure with return value + OUTPUT. Primer: [06](06-sqlparameter-and-raw-sql.md).

## Spec

Call `usp_GetDoctorAppointment`. Return SP return code. Set `out appointmentNo`. On exception: return **`-99`**, `appointmentNo = 0`.

## SP return codes (from SQL)

| Code | Meaning |
|---|---|
| `1` | inserted |
| `2` | already exists (AppointmentNo filled) |
| `-1` | bad PatientID |
| `-2` | bad DoctorID |
| `-3` | date in the past |
| `-99` | SQL catch / C# catch |

## Line-by-line

```csharp
public int GetDoctorAppointment(
    string patientId, string doctorId, DateTime dateOfAppointment, out int appointmentNo)
{
    appointmentNo = 0;                             // 1 must assign out before leave
    int returnValue;
    try
    {
        var prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
            { Direction = ParameterDirection.Output };
        var prmPatientId = new SqlParameter("@PatientID", patientId);
        var prmDoctorId = new SqlParameter("@DoctorID", doctorId);
        var prmDate = new SqlParameter("@DateOfAppointment", dateOfAppointment.Date);
        var prmAppointmentNo = new SqlParameter("@AppointmentNo", SqlDbType.Int)
            { Direction = ParameterDirection.Output };

        context.Database.ExecuteSqlRaw(
            "EXEC @ReturnValue = usp_GetDoctorAppointment @PatientID, @DoctorID, @DateOfAppointment, @AppointmentNo OUTPUT",
            prmReturnValue, prmPatientId, prmDoctorId, prmDate, prmAppointmentNo);

        appointmentNo = prmAppointmentNo.Value == DBNull.Value
            ? 0 : Convert.ToInt32(prmAppointmentNo.Value);
        returnValue = prmReturnValue.Value == DBNull.Value
            ? -99 : Convert.ToInt32(prmReturnValue.Value);
    }
    catch (Exception)
    {
        appointmentNo = 0;
        returnValue = -99;
    }
    return returnValue;
}
```

| Piece | Why |
|---|---|
| `out` assigned first | C# rule |
| `EXEC @ReturnValue = usp_…` | capture SP `RETURN` |
| `@AppointmentNo OUTPUT` | capture OUTPUT param |
| both `DBNull` guards | defensive |

## Exam variations

1. Caller: `int no; int r = repo.GetDoctorAppointment(…, out no);`
2. Map return codes to messages in the console (PS DAL usually just returns the int).
3. Wrong: forgetting `OUTPUT` in the SQL string.
