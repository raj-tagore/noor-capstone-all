namespace PolyclinicDAL.Models
{
    public class ListOfAppointments
    {
        public string DoctorName { get; set; }

        public string Specialization { get; set; }

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public int AppointmentNo { get; set; }
    }
}
