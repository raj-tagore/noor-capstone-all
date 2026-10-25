# 04 — FirstOrDefault and siblings

When you need this: end a LINQ query that should return zero or one row without throwing when empty.

## Syntax

```csharp
sequence.FirstOrDefault();
sequence.FirstOrDefault(predicate);

sequence.First();
sequence.Single();
sequence.SingleOrDefault();

context.Patients.Find(primaryKey);   // DbSet API, not LINQ operators
sequence.Where(…).ToList();
sequence.Any(…);
sequence.Count(…);
```

## Behavior cheat sheet

| Method | 0 rows | 1 row | 2+ rows |
|---|---|---|---|
| `FirstOrDefault` | `default` (`null` for classes) | first | first (rest ignored) |
| `First` | **throws** | first | first |
| `SingleOrDefault` | `default` | that one | **throws** |
| `Single` | **throws** | that one | **throws** |
| `Find(pk)` | `null` | tracked entity | n/a (by PK) |
| `ToList` | empty list | list | list |
| `Any` | `false` | `true` | `true` |

## Where it goes

After the query expression (parentheses required for query syntax):

```csharp
(from p in context.Patients where … select p).FirstOrDefault();
```

Or as method chain:

```csharp
context.Patients.Where(…).FirstOrDefault();
```

## This project

PS asked for LINQ → use `FirstOrDefault` for “one patient / one appointment or none”.

## Exam variations

1. `Find(patientId)` — shorter; OK if PS does **not** demand LINQ.
2. Must prove uniqueness → `SingleOrDefault`.
3. Must throw if missing → `First` / `Single`.
4. All matching rows → `ToList()`.
5. Existence check only → `Any(p => p.PatientId == id)`.
