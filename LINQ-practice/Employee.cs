public class Employee
{
    static int counter = 1001;

    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int JobLevel { get; set; }
    public DateTime JoiningDate { get; set; }
    public string? ProjectCode { get; set; }
    public double Salary { get; set; }
    public int UnitId { get; set; }

    public Employee()
    {
        EmployeeId = "E" + counter;
        counter++;
    }
}
