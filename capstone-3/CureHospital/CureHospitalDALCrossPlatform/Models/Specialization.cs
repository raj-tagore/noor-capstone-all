namespace CureHospitalDALCrossPlatform.Models;

public class Specialization
{
    public string SpecializationCode { get; set; }

    public string SpecializationName { get; set; }

    public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

    public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();
}
