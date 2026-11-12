using System.ComponentModel.DataAnnotations;

namespace CureHospitalWebService.Models;

public class DoctorSpecialization
{
    // ===== SOLUTION START: data annotations =====
    [Required]
    [Range(1001, int.MaxValue)]
    // ===== SOLUTION END =====
    public int DoctorId { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    [StringLength(3, MinimumLength = 3)]
    // ===== SOLUTION END =====
    public string SpecializationCode { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    // ===== SOLUTION END =====
    public DateTime SpecializationDate { get; set; }
}
