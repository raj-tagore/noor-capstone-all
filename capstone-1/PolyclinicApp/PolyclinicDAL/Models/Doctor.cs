using System;
using System.Collections.Generic;

namespace PolyclinicDAL.Models;

public partial class Doctor
{
    public string DoctorId { get; set; }

    public string Specialization { get; set; }

    public string DoctorName { get; set; }

    public decimal Fees { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
