using System;
using System.Collections.Generic;

namespace PolyclinicDAL.Models;

public partial class Appointment
{
    public int AppointmentNo { get; set; }

    public string PatientId { get; set; }

    public string DoctorId { get; set; }

    public DateOnly DateofAppointment { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Patient Patient { get; set; }
}
