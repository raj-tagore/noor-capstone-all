# CureHospital — Infosys .NET Practical Examination Exercise Roadmap & Progress Tracker

## Project Overview

- **Database:** `CureHospitalFA1DB`
- **Tables:** `Doctor`, `Specialization`, `DoctorSpecialization`, `Surgery`
- **Stored Procedures:** `usp_AddSurgeryDetails`, `usp_AddDoctorDetails`
- **Table-Valued Function (TVF):** `ufn_FetchSurgeryDetails(@SurgeryDate)`
- **Scalar Function:** `ufn_FetchSurgeryCount(@SpecializationName, @SurgeryDate)`
- **Keyless Model:** `SurgeryDetail`
- **Language / Framework:** C# .NET 8, Entity Framework Core (Database-First), ASP.NET Core Web API
- **LINQ Standard:** Pure Query Syntax only (`from ... where ... select`), no Method Syntax.

---

## Directory Structure

All 85 examination exercises are located in:
`practice-exercises/`

- [Level-1-Basic/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-1-Basic) (Exercises 01–10)
- [Level-2-Basic-Conditions/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-2-Basic-Conditions) (Exercises 11–22)
- [Level-3-Multiple-Operations/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-3-Multiple-Operations) (Exercises 23–38)
- [Level-4-LINQ-Relationships/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-4-LINQ-Relationships) (Exercises 39–55)
- [Level-5-Database-Functions/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-5-Database-Functions) (Exercises 56–72)
- [Level-6-Combined-Practical/](file:///Users/prabhnoor/Documents/noor-capstone-all/capstone-fa1-retest/practice-exercises/Level-6-Combined-Practical) (Exercises 73–85)

---

## Progress Checklist

| Concept | Level | Exercise Numbers | Status | Variations Count |
|---|---|---|---|---|
| Basic Add | 1 | 04, 05, 09 | Not started | 3 |
| Basic Get (by ID) | 1 | 01, 08 | Not started | 2 |
| Basic Get (All) | 1 | 02, 03 | Not started | 2 |
| Basic Update | 1 | 06 | Not started | 1 |
| Basic Delete | 1 | 07, 10 | Not started | 2 |
| Add with Duplicate Check | 2 | 11, 12 | Not started | 2 |
| Add with FK Validation (1 check) | 2 | 13, 17 | Not started | 2 |
| Add with FK Validation (2 checks) | 2 | 14, 18 | Not started | 2 |
| Add with FK Validation (3 checks) | 2 | 15 | Not started | 1 |
| Add with Time / Business Validation | 2 | 19, 22 | Not started | 2 |
| Update with Null Check | 2 | 16, 20 | Not started | 2 |
| Get by Non-PK (FirstOrDefault) | 2 | 21 | Not started | 1 |
| Conditional Property Assignment | 2 | 22 | Not started | 1 |
| Update Multiple Properties | 3 | 23, 24, 25 | Not started | 3 |
| Update Multiple Objects (foreach) | 3 | 26, 27, 28, 33 | Not started | 4 |
| Update with 2 LINQ Queries | 3 | 30, 31 | Not started | 2 |
| Update with 3 LINQ Queries | 3 | 32, 37 | Not started | 2 |
| Update Composite Key | 3 | 29 | Not started | 1 |
| Delete Composite Key | 3 | 34 | Not started | 1 |
| Delete Multiple Records (RemoveRange) | 3 | 35, 36 | Not started | 2 |
| Add with Full Validation (EF Core) | 3 | 38 | Not started | 1 |
| LINQ Join (2 tables) | 4 | 39, 40, 41, 46 | Not started | 4 |
| LINQ Join (3 tables) | 4 | 47, 48, 55 | Not started | 3 |
| LINQ Aggregates (Count, Sum) | 4 | 42, 43 | Not started | 2 |
| LINQ OrderBy | 4 | 45 | Not started | 1 |
| LINQ Date-Range Filtering | 4 | 44 | Not started | 1 |
| LINQ Multiple WHERE Conditions | 4 | 51, 52 | Not started | 2 |
| LINQ Group By | 4 | 49, 50, 53, 54 | Not started | 4 |
| Custom DTO / Projection Classes | 4 | 46–50, 53–55 | Not started | 8 |
| Table-Valued Function (TVF) Basic | 5 | 56, 57 | Not started | 2 |
| TVF with Conditions / IF-ELSE | 5 | 57, 58 | Not started | 2 |
| TVF with In-Memory Filtering | 5 | 59, 60 | Not started | 2 |
| TVF with Foreach / Calculation | 5 | 61, 63 | Not started | 2 |
| TVF with Second DB Lookup | 5 | 62 | Not started | 1 |
| Scalar Function via ADO.NET | 5 | 64, 65, 66, 67 | Not started | 4 |
| Stored Procedure with OUT parameter | 5 | 68, 69, 72 | Not started | 3 |
| Stored Procedure without OUT param | 5 | 70, 71 | Not started | 2 |
| Stored Procedure + EF Pre-Check | 5 | 71 | Not started | 1 |
| Multi-Table Transactional Insert | 6 | 73 | Not started | 1 |
| Complex Schedule / Nested DTO | 6 | 74, 83, 85 | Not started | 3 |
| Complex Update with Overlap Check | 6 | 75, 77 | Not started | 2 |
| Cross-Table Summary Report | 6 | 76, 85 | Not started | 2 |
| Full Validation Pipeline (7 rules) | 6 | 78 | Not started | 1 |
| TVF + Scalar Combined Pipeline | 6 | 79 | Not started | 1 |
| Cascading Delete across 3 Tables | 6 | 80 | Not started | 1 |
| Stored Procedure -> TVF Pipeline | 6 | 81 | Not started | 1 |
| Cross-Table Update Verification | 6 | 82 | Not started | 1 |
| Batch Operations (List parameter) | 6 | 84 | Not started | 1 |
| ASP.NET Web API Controller Actions | All | All 85 Exercises | Not started | 85 |
