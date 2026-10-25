List<Unit> unitList = new List<Unit>()
{
    new Unit { UnitId = 10, UnitName = "ETA" },
    new Unit { UnitId = 20, UnitName = "FSI" },
    new Unit { UnitId = 30, UnitName = "ECS" }
};

List<Employee> employeeList = new List<Employee>()
{
    new Employee { EmployeeName = "John", UnitId = 10, ProjectCode = "ETAMYS", Salary = 30000, JobLevel = 3, JoiningDate = new DateTime(2014, 3, 5) },
    new Employee { EmployeeName = "Jack", UnitId = 10, ProjectCode = "ETACHN", Salary = 35000, JobLevel = 3, JoiningDate = new DateTime(2011, 3, 5) },
    new Employee { EmployeeName = "Albus", UnitId = 10, ProjectCode = "ETACHN", Salary = 15000, JobLevel = 4, JoiningDate = new DateTime(2011, 3, 5) },
    new Employee { EmployeeName = "Ron", UnitId = 20, ProjectCode = "FSIAUS", Salary = 10000, JobLevel = 4, JoiningDate = new DateTime(2007, 2, 5) },
    new Employee { EmployeeName = "Iwa", UnitId = 20, ProjectCode = "FSIAUS", Salary = 15000, JobLevel = 4, JoiningDate = new DateTime(2007, 2, 5) },
    new Employee { EmployeeName = "Albert", UnitId = 30, ProjectCode = null, Salary = 20000, JobLevel = 3, JoiningDate = new DateTime(2005, 1, 5) }
};

void Print<T>(string title, IEnumerable<T> rows)
{
    Console.WriteLine(title);
    foreach (var row in rows)
        Console.WriteLine("  " + row);
    Console.WriteLine();
}

// 6. EmployeeName and UnitName of all employees, ascending by employee name.
var qery = 
from e in employeeList
join u in unitList on e.UnitId equals u.UnitId
orderby e.EmployeeName ascending
select new { e.EmployeeName, u.UnitName }

            



    join u in table2 on e.colt1 equals u.colt2





























// 1. EmployeeName, UnitId and ProjectCode where Salary is between 15000 and 20000.
var query1 =
    from e in employeeList
    where e.Salary >= 15000 && e.Salary <= 20000
    select new { e.EmployeeName, e.UnitId, e.ProjectCode };

// 2. EmployeeName and TotalExperience in years for employees with more than 3 years.
var query2 =
    from e in employeeList
    let totalExperience = (DateTime.Today - e.JoiningDate).Days / 365
    where totalExperience > 3
    select new { e.EmployeeName, TotalExperience = totalExperience };

// 3. EmployeeName of employees who are not assigned to any project.
var query3 =
    from e in employeeList
    where e.ProjectCode == null
    select new { e.EmployeeName };

// 4. Total salary drawn by all employees.
// var salaries = 
//     from e in employeeList
//     select e.Salary;
// var salarysum = qery2.Sum();
var query4 =
    from e in employeeList
    group e.Salary by 1 into g
    select g.Sum();

// 5. Total salary drawn by employees in JobLevel 3.
var query5 =
    from e in employeeList
    where e.JobLevel == 3
    group e.Salary by 1 into g
    select g.Sum();

// 6. EmployeeName and UnitName of all employees, ascending by employee name.
var query6 =
    from e in employeeList
    join u in unitList on e.UnitId equals u.UnitId
    orderby e.EmployeeName
    select new { e.EmployeeName, u.UnitName };

// 7. EmployeeName and Salary of employees drawing the minimum salary.
var query7 =
    from e in employeeList
    where e.Salary == (from x in employeeList select x.Salary).Min()
    select new { e.EmployeeName, e.Salary };

// 8. EmployeeName and JoiningDate of the most experienced employees.
var query8 =
    from e in employeeList
    where e.JoiningDate == (from x in employeeList select x.JoiningDate).Min()
    select new { e.EmployeeName, e.JoiningDate };

// 9. UnitId and number of employees in each unit.
var query9 =
    from e in employeeList
    group e by e.UnitId into g
    select new { UnitId = g.Key, NumberOfEmployees = g.Count() };

// 10. UnitId and number of employees in each unit whose Salary is greater than 10000.
var query10 =
    from e in employeeList
    where e.Salary > 10000
    group e by e.UnitId into g
    select new { UnitId = g.Key, NumberOfEmployees = g.Count() };

// 11. UnitId and total salary drawn by employees in each unit.
var query11 =
    from e in employeeList
    group e.Salary by e.UnitId into g
    select new { UnitId = g.Key, TotalSalary = g.Sum() };

// 12. UnitId and total salary for units whose total salary is greater than 20000.
var query12 =
    from e in employeeList
    group e.Salary by e.UnitId into g
    let totalSalary = g.Sum()
    where totalSalary > 20000
    select new { UnitId = g.Key, TotalSalary = totalSalary };

// 13. UnitId and JobLevel 3 salary total, for units where that total is greater than 20000.
var query13 =
    from e in employeeList
    where e.JobLevel == 3
    group e.Salary by e.UnitId into g
    let totalSalary = g.Sum()
    where totalSalary > 20000
    select new { UnitId = g.Key, TotalSalary = totalSalary };

// 14. UnitName and maximum salary in each unit, descending by maximum salary.
var query14 =
    from e in employeeList
    join u in unitList on e.UnitId equals u.UnitId
    group e.Salary by u.UnitName into g
    let maxSalary = g.Max()
    orderby maxSalary descending
    select new { UnitName = g.Key, MaxSalary = maxSalary };

// 15. Employees who draw the minimum salary in their unit.
var query15 =
    from e in employeeList
    group e by e.UnitId into g
    let minUnitSalary = (from x in g select x.Salary).Min()
    from emp in g
    where emp.Salary == minUnitSalary
    select new { emp.EmployeeName, emp.UnitId, emp.Salary, emp.ProjectCode };

// 16. Employees whose salary is greater than the average salary in their unit.
var query16 =
    from e in employeeList
    join u in unitList on e.UnitId equals u.UnitId
    group new { e, u } by e.UnitId into g
    let averageSalary = (from x in g select x.e.Salary).Average()
    from x in g
    where x.e.Salary > averageSalary
    select new { x.e.EmployeeName, x.u.UnitName, x.e.Salary, x.e.ProjectCode };

// 17. UnitId, JobLevel and number of employees in that unit and job level.
var query17 =
    from e in employeeList
    group e by new { e.UnitId, e.JobLevel } into g
    select new { g.Key.UnitId, g.Key.JobLevel, NumberOfEmployees = g.Count() };

Print("1. Salary between 15000 and 20000", query1);
Print("2. More than 3 years of experience", query2);
Print("3. Not assigned to any project", query3);
Print("4. Total salary of all employees", query4);
Print("5. Total salary of JobLevel 3", query5);
Print("6. Employee and unit, ordered by name", query6);
Print("7. Minimum salary", query7);
Print("8. Most experienced", query8);
Print("9. Employees per unit", query9);
Print("10. Employees per unit with salary greater than 10000", query10);
Print("11. Total salary per unit", query11);
Print("12. Units with total salary greater than 20000", query12);
Print("13. JobLevel 3 salary per unit, where that total is greater than 20000", query13);
Print("14. Maximum salary per unit, descending", query14);
Print("15. Minimum salary in each unit", query15);
Print("16. Salary greater than the unit average", query16);
Print("17. Employees per unit and job level", query17);
