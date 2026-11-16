namespace CureHospitalDALCrossPlatform.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; }

    public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

    public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();
}
