# 14 — API actions the service calls

When you need this: `ps/51` and `ps/52`. The Angular commits call these URLs. The implementations are in `CureWellServices/Controllers/CureWellController.cs`. Signatures are already there; the body calls the repository.

## Syntax

```csharp
[HttpGet]
public JsonResult GetDoctors()
{
    List<Models.Doctor> list = new List<Models.Doctor>();
    try
    {
        foreach (var item in rep.GetAllDoctors())
        {
            list.Add(new Models.Doctor
            {
                DoctorId = item.DoctorId,
                DoctorName = item.DoctorName
            });
        }
    }
    catch (Exception)
    {
        list = null;
    }
    return Json(list);
}
```

```csharp
[HttpPost]
public bool AddDoctor(Models.Doctor dObj)
{
    bool status = false;
    try
    {
        var doctor = new CureWellDataAccessLayer.Models.Doctor();
        doctor.DoctorName = dObj.DoctorName;
        status = rep.AddDoctor(doctor);
    }
    catch (Exception)
    {
        status = false;
    }
    return status;
}
```

## Where each part goes

| Piece | Role |
|---|---|
| `[HttpGet]` / `[HttpPost]` / `[HttpPut]` / `[HttpDelete]` | matches `http.get` / `post` / `put` / `request('delete')` |
| `rep.GetAllDoctors()` and the other `rep.*` calls | provided DAL. Do not open SQL here |
| copy into `CureWellServices.Models.*` | API model and DAL model are different types with the same property names |
| `return Json(list)` | list endpoints |
| `return status` | bool endpoints |
| `catch` → `null` or `false` | `ps/51` |

There are two `Doctor` classes. `Models.Doctor` is the JSON the browser sends. `CureWellDataAccessLayer.Models.Doctor` is what `rep` accepts. Assign property by property.

## Map

| Action | Verb | Repository |
|---|---|---|
| `GetDoctors` | GET | `GetAllDoctors` |
| `GetSpecializations` | GET | `GetAllSpecializations` |
| `GetAllSurgeryTypeForToday` | GET | `GetAllSurgeryTypeForToday` |
| `AddDoctor` | POST | `AddDoctor` (name only; id is identity) |
| `UpdateDoctorDetails` | PUT | `UpdateDoctorDetails` |
| `UpdateSurgery` | PUT | `UpdateSurgery` (copy id, doctor, date, category, both times) |
| `DeleteDoctor` | DELETE | `DeleteDoctor` |

`[Route("api/[controller]/[action]")]` makes the URL `api/CureWell/GetDoctors`.

## Exam variations

1. Returning the DAL entity directly from `Json` — works if the JSON property names stay camelCase and you do not need to drop navigation properties.
2. `ModelState.IsValid` before update/delete — the committed controller does this on PUT/DELETE. An invalid model leaves `status` false.
3. Setting only `DoctorName` on add — correct. Setting `DoctorId` from the client would fight the identity column.
