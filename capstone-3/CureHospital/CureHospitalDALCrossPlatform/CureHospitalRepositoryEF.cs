using CureHospitalDALCrossPlatform.Models;

namespace CureHospitalDALCrossPlatform
{
    public class CureHospitalRepositoryEF
    {
        CureHospitalDbContext context;

        public CureHospitalRepositoryEF(CureHospitalDbContext cureHospitalDbContext)
        {
            context = cureHospitalDbContext;
        }

        public List<string> GetAllSurgeryDates()
        {
            try
            {
                return context.Surgeries
                    .Select(surgery => surgery.SurgeryDate)
                    .Distinct()
                    .OrderBy(date => date)
                    .AsEnumerable()
                    .Select(date => date.ToString("yyyy-MM-dd"))
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)
        {
            try
            {
                DateOnly date = DateOnly.FromDateTime(surgeryDate);
                return (from surgery in context.Surgeries
                        join doctor in context.Doctors on surgery.DoctorId equals doctor.DoctorId
                        join specialization in context.Specializations on surgery.SurgeryCategory equals specialization.SpecializationCode
                        where surgery.SurgeryDate == date
                        select new SurgeryDetail
                        {
                            SurgeryId = surgery.SurgeryId,
                            DoctorId = doctor.DoctorId,
                            DoctorName = doctor.DoctorName,
                            SurgeryCategory = surgery.SurgeryCategory.Trim(),
                            SpecializationName = specialization.SpecializationName,
                            SurgeryDate = surgery.SurgeryDate.ToDateTime(TimeOnly.MinValue),
                            StartTime = surgery.StartTime,
                            EndTime = surgery.EndTime
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateSurgeryDetails(SurgeryDetail updatedSurgery)
        {
            try
            {
                Surgery surgery = context.Surgeries.Find(updatedSurgery.SurgeryId);
                if (surgery == null)
                {
                    return false;
                }

                surgery.StartTime = updatedSurgery.StartTime;
                surgery.EndTime = updatedSurgery.EndTime;
                surgery.SurgeryDate = DateOnly.FromDateTime(updatedSurgery.SurgeryDate);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int AddSurgeryDetails(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
        {
            surgeryId = 0;
            try
            {
                Surgery surgery = new Surgery
                {
                    DoctorId = doctorId,
                    SurgeryCategory = surgeryCategory,
                    SurgeryDate = DateOnly.FromDateTime(surgeryDate),
                    StartTime = startTime,
                    EndTime = endTime
                };
                context.Surgeries.Add(surgery);
                context.SaveChanges();
                surgeryId = surgery.SurgeryId;
                return 1;
            }
            catch (Exception)
            {
                surgeryId = 0;
                return -98;
            }
        }
    }
}
