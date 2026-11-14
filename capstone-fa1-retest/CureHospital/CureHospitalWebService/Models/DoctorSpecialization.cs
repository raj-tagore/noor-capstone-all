using System.ComponentModel.DataAnnotations;

namespace CureHospitalWebService.Models;

public class DoctorSpecialization
{
    [Required]
    [Range(1001, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string SpecializationCode { get; set; }

    [Required]
    public DateTime SpecializationDate { get; set; }
}
