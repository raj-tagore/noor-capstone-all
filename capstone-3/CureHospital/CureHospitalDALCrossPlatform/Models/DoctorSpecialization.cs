namespace CureHospitalDALCrossPlatform.Models;

public class DoctorSpecialization
{
    public int DoctorId { get; set; }

    public string SpecializationCode { get; set; }

    public DateOnly SpecializationDate { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Specialization SpecializationCodeNavigation { get; set; }
}
