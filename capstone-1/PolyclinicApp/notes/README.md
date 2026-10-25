# Polyclinic notes

What you write in the exam: repository methods, LINQ, `SaveChanges`, and calls to functions/procedures. The solution, packages, database, connection string, and Visual Studio build/run are already set up.

## Read order

| File | Purpose |
|---|---|
| [01-reading-the-problem.md](01-reading-the-problem.md) | Signatures and return codes |
| [02-try-catch.md](02-try-catch.md) | `try` / `catch` and status values |
| [03-linq.md](03-linq.md) | Query vs method syntax |
| [04-first-or-default.md](04-first-or-default.md) | `FirstOrDefault` and siblings |
| [05-dbset-add-remove-save.md](05-dbset-add-remove-save.md) | `Add` / `Remove` / `SaveChanges` |
| [06-sqlparameter-and-raw-sql.md](06-sqlparameter-and-raw-sql.md) | Stored procs, scalar FN, TVF |
| [07-dbcontext-line-by-line.md](07-dbcontext-line-by-line.md) | `DbSet`s and fluent mapping you may add |
| [08-models.md](08-models.md) | Entity properties + keyless TVF type |
| [09-repository-skeleton.md](09-repository-skeleton.md) | `context` field + method map |
| `10`–`16` | One file per repository method |
| [17-console-program.md](17-console-program.md) | How the given tester calls the repository |

## If the PS says X, open file Y

| PS phrase | Open |
|---|---|
| LINQ query against a table | [03](03-linq.md), [04](04-first-or-default.md), [10](10-get-patient-details.md) |
| Insert / update / delete | [05](05-dbset-add-remove-save.md), [11](11-add-new-patient-details.md)–[13](13-cancel-appointment.md) |
| Table-valued function | [06](06-sqlparameter-and-raw-sql.md), [14](14-fetch-all-appointments.md) |
| Scalar function | [06](06-sqlparameter-and-raw-sql.md), [15](15-calculate-doctor-fees.md) |
| Stored procedure + `out` | [06](06-sqlparameter-and-raw-sql.md), [16](16-get-doctor-appointment.md) |
| Return `true`/`false`, `null`, `1`/`-1`/`-99` | [02](02-try-catch.md), [01](01-reading-the-problem.md) |
