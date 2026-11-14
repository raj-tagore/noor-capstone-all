using System.ComponentModel.DataAnnotations;

namespace CureHospitalWebService.Models;

public class Surgery
{
    public int SurgeryId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string SurgeryCategory { get; set; }

    [Required]
    public DateTime SurgeryDate { get; set; }

    [Required]
    [Range(typeof(decimal), "0.00", "23.00")]
    public decimal StartTime { get; set; }

    [Required]
    [Range(typeof(decimal), "0.00", "23.00")]
    public decimal EndTime { get; set; }
}
