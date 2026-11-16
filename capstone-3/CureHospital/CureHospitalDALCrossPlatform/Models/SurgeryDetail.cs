namespace CureHospitalDALCrossPlatform.Models;

public class SurgeryDetail
{
    public int SurgeryId { get; set; }

    public int DoctorId { get; set; }

    public string DoctorName { get; set; }

    public string SurgeryCategory { get; set; }

    public string SpecializationName { get; set; }

    public DateTime SurgeryDate { get; set; }

    public decimal StartTime { get; set; }

    public decimal EndTime { get; set; }
}
