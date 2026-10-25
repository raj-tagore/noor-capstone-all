using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PolyclinicDAL.Models;

namespace PolyclinicDAL
{
    public class PolyclinicRepository
    {
        PolyclinicDbContext context;

        public PolyclinicRepository(PolyclinicDbContext context)
        {
            this.context = context;
        }

        public Patient GetPatientDetails(string patientId)
        {
            Patient patient = null;
            try
            {
                patient = (from p in context.Patients
                           where p.PatientId == patientId
                           select p).FirstOrDefault();
            }
            catch (Exception)   
            {
                patient = null;
            }
            return patient;
        }
    
        public bool AddNewPatientDetails(Patient patientObj)
        {
            bool status = false;
            try
            {
                context.Patients.Add(patientObj);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool UpdatePatientAge(string patientId, byte newAge)
        {
            bool status = false;
            try
            {
                Patient patient = (from p in context.Patients
                                   where p.PatientId == patientId
                                   select p).FirstOrDefault();
                if (patient != null)
                {
                    patient.Age = newAge;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public int CancelAppointment(int appointmentNo)
        {
            int result;
            try
            {
                Appointment appointment = (from a in context.Appointments
                                           where a.AppointmentNo == appointmentNo
                                           select a).FirstOrDefault();
                if (appointment != null)
                {
                    context.Appointments.Remove(appointment);
                    context.SaveChanges();
                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        public List<ListOfAppointments> FetchAllAppointments(string doctorId, DateTime date)
        {
            List<ListOfAppointments> lstAppointments;
            try
            {
                SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
                SqlParameter prmDate = new SqlParameter("@DateofAppointment", date.Date);
                lstAppointments = context.ListOfAppointments
                    .FromSqlRaw("SELECT * FROM dbo.ufn_FetchAllAppointments(@DoctorID, @DateofAppointment)", prmDoctorId, prmDate)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception)
            {
                lstAppointments = null;
            }
            return lstAppointments;
        }

        public decimal CalculateDoctorFees(string doctorId, DateTime date)
        {
            decimal fees;
            try
            {
                SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
                SqlParameter prmDate = new SqlParameter("@DateofAppointment", date.Date);
                SqlParameter prmResult = new SqlParameter("@Result", SqlDbType.Money)
                {
                    Direction = ParameterDirection.Output
                };

                context.Database.ExecuteSqlRaw(
                    "SET @Result = dbo.ufn_CalculateDoctorFees(@DoctorID, @DateofAppointment)",
                    prmDoctorId,
                    prmDate,
                    prmResult);

                if (prmResult.Value == DBNull.Value)
                {
                    fees = 0;
                }
                else
                {
                    fees = Convert.ToDecimal(prmResult.Value);
                }
            }
            catch (Exception)
            {
                fees = -99;
            }
            return fees;
        }

        public int GetDoctorAppointment(string patientId, string doctorId, DateTime dateOfAppointment, out int appointmentNo)
        {
            appointmentNo = 0;
            int returnValue;
            try
            {
                SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter prmPatientId = new SqlParameter("@PatientID", patientId);
                SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
                SqlParameter prmDate = new SqlParameter("@DateOfAppointment", dateOfAppointment.Date);
                SqlParameter prmAppointmentNo = new SqlParameter("@AppointmentNo", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                context.Database.ExecuteSqlRaw(
                    "EXEC @ReturnValue = usp_GetDoctorAppointment @PatientID, @DoctorID, @DateOfAppointment, @AppointmentNo OUTPUT",
                    prmReturnValue,
                    prmPatientId,
                    prmDoctorId,
                    prmDate,
                    prmAppointmentNo);

                appointmentNo = prmAppointmentNo.Value == DBNull.Value ? 0 : Convert.ToInt32(prmAppointmentNo.Value);
                returnValue = prmReturnValue.Value == DBNull.Value ? -99 : Convert.ToInt32(prmReturnValue.Value);
            }
            catch (Exception)
            {
                appointmentNo = 0;
                returnValue = -99;
            }
            return returnValue;
        }
    }
}
