using System.ComponentModel.DataAnnotations;

namespace CureHospitalWebService.Models;

public class Surgery
{
    public int SurgeryId { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    // ===== SOLUTION END =====
    public int DoctorId { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    [StringLength(3, MinimumLength = 3)]
    // ===== SOLUTION END =====
    public string SurgeryCategory { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    // ===== SOLUTION END =====
    public DateTime SurgeryDate { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    [Range(typeof(decimal), "0.00", "23.00")]
    // ===== SOLUTION END =====
    public decimal StartTime { get; set; }

    // ===== SOLUTION START: data annotations =====
    [Required]
    [Range(typeof(decimal), "0.00", "23.00")]
    // ===== SOLUTION END =====
    public decimal EndTime { get; set; }
}
