namespace CureHospitalDALCrossPlatform.Models;

public class DoctorSurgeryInfo
{
    public int SurgeryId { get; set; }
    public string DoctorName { get; set; }
    public DateTime SurgeryDate { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
    public string SurgeryCategory { get; set; }
}

public class SurgeryFullDetail
{
    public int SurgeryId { get; set; }
    public string DoctorName { get; set; }
    public string SpecializationName { get; set; }
    public DateTime SurgeryDate { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
}

public class DoctorSpecDetail
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string SpecializationCode { get; set; }
    public string SpecializationName { get; set; }
    public DateTime SpecializationDate { get; set; }
}

public class SpecializationSurgeryCount
{
    public string SpecializationCode { get; set; }
    public int SurgeryCount { get; set; }
}

public class DoctorSurgeryHours
{
    public int DoctorId { get; set; }
    public decimal TotalHours { get; set; }
}

public class DoctorDateSurgeryCount
{
    public int DoctorId { get; set; }
    public DateTime SurgeryDate { get; set; }
    public int SurgeryCount { get; set; }
}

public class SpecDoctorCount
{
    public string SpecializationCode { get; set; }
    public string SpecializationName { get; set; }
    public int DoctorCount { get; set; }
}

public class DoctorSurgerySummary
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public int TotalSurgeries { get; set; }
    public decimal TotalHours { get; set; }
}

public class SpecSurgeryCount
{
    public string SpecializationName { get; set; }
    public int SurgeryCount { get; set; }
}

public class ScheduleItem
{
    public int SurgeryId { get; set; }
    public string SpecializationName { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
}

public class DoctorSchedule
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime SurgeryDate { get; set; }
    public List<ScheduleItem> Surgeries { get; set; } = new List<ScheduleItem>();
}

public class SpecializationReport
{
    public string SpecializationCode { get; set; }
    public string SpecializationName { get; set; }
    public int DoctorCount { get; set; }
    public int SurgeryCount { get; set; }
    public decimal TotalSurgeryHours { get; set; }
}

public class SurgeryDetailWithCount
{
    public int SurgeryId { get; set; }
    public string DoctorName { get; set; }
    public string SpecializationName { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
    public int TotalCategorySurgeries { get; set; }
}

public class DoctorAvailability
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime SurgeryDate { get; set; }
    public int SurgeryCount { get; set; }
    public decimal TotalBookedHours { get; set; }
    public decimal AvailableHours { get; set; }
}

public class HospitalReport
{
    public int TotalDoctors { get; set; }
    public int TotalSpecializations { get; set; }
    public int TotalSurgeries { get; set; }
    public int UpcomingSurgeries { get; set; }
    public string BusiestDoctorName { get; set; }
    public string MostCommonSpecialization { get; set; }
}
