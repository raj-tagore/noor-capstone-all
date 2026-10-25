# 07 — PolyclinicDbContext (line-by-line)

When you need this: read the generated context and add a keyless `DbSet` for a TVF. File: `PolyclinicDAL/Models/PolyclinicDbContext.cs`. Connection setup is already in the project.

## What you use

```csharp
public virtual DbSet<Patient> Patients { get; set; }
public virtual DbSet<ListOfAppointments> ListOfAppointments { get; set; }
```

`context.Patients` is that `DbSet`. Repository methods query it; you do not construct the context yourself if the console already passes one in.

## Mapping you may add for a TVF result

Inside `OnModelCreating`:

```csharp
modelBuilder.Entity<ListOfAppointments>(entity =>
{
    entity.HasNoKey();
});
```

`HasNoKey()` is required before `FromSqlRaw` on a type that is not a table.

## Other fluent calls already on the entities

| Call | Meaning |
|---|---|
| `HasKey` | primary key |
| `HasColumnName("DoctorID")` | C# `DoctorId` ↔ SQL column |
| `HasMaxLength` / `IsFixedLength` | `VARCHAR` / `CHAR` |
| `HasColumnType("date")` / `"money"` | SQL type |
| `HasOne().WithMany().HasForeignKey()` | FK |

## Exam variations

1. Renaming a `DbSet` property means every `context.Patients` call must change.
2. A TVF type without `HasNoKey()` will not compose with `FromSqlRaw`.
