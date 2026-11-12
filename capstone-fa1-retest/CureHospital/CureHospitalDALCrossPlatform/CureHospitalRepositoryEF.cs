using System.Data;
using CureHospitalDALCrossPlatform.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CureHospitalDALCrossPlatform;

public class CureHospitalRepositoryEF
{
    private readonly CureHospitalDbContext context;

    public CureHospitalRepositoryEF(CureHospitalDbContext context)
    {
        this.context = context;
    }

    #region FetchDoctorIDs - Do not modify the signature
    public List<int> FetchDoctorIDs(string specializationCode)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        List<int> x = new List<int>();
        try
        {
            // ===== SOLUTION START =====
            x = context.DoctorSpecializations
                .Where(p => p.SpecializationCode == specializationCode)
                .Select(d => d.DoctorId)
                .ToList();
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            x = null;
        }
        return x;
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        bool result = false;
        try
        {
            // ===== SOLUTION START =====
            DoctorSpecialization obj = new DoctorSpecialization();
            obj.DoctorId = doctorId;
            obj.SpecializationCode = specializationCode;
            obj.SpecializationDate = specializationDate;
            context.DoctorSpecializations.Add(obj);
            context.SaveChanges();
            result = true;
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            result = false;
        }
        return result;
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        int result = -1;
        try
        {
            // ===== SOLUTION START =====
            // -1 when the new end time is not greater than the new start time.
            // Surgery 5007 is not in the table. Find does not throw, and the test table expects -98.
            if (newEndTime <= newStartTime)
            {
                result = -1;
            }
            else
            {
                var obj = context.Surgeries.Find(surgeryID);
                if (obj == null)
                {
                    result = -98;
                }
                else
                {
                    obj.StartTime = newStartTime;
                    obj.EndTime = newEndTime;
                    context.SaveChanges();
                    result = 1;
                }
            }
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            result = -98;
        }
        return result;
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    public bool? RemoveSurgeryDetails(DateTime surgeryDate)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        bool status = false;
        try
        {
            // ===== SOLUTION START =====
            var day = surgeryDate.Date;
            var obj = (from p in context.Surgeries
                       where p.SurgeryDate == day
                       select p).ToList();
            if (obj.Count > 0)
            {
                context.Surgeries.RemoveRange(obj);
                context.SaveChanges();
                status = true;
            }
            else
            {
                status = false;
            }
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            status = false;
        }
        return status;
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        surgeryID = -1;
        int result = 0;
        try
        {
            // ===== SOLUTION START =====
            // A null category is the published case that must come back as -98 with OUT 0.
            // Passing it through to SQL would be a missing category (-2), not a DAL exception.
            if (surgeryCategory == null)
            {
                throw new ArgumentNullException(nameof(surgeryCategory));
            }

            SqlParameter prdid = new SqlParameter("@did", doctorID);
            SqlParameter prsc = new SqlParameter("@sc", surgeryCategory);
            SqlParameter prsd = new SqlParameter("@sd", surgeryDate.Date);
            SqlParameter prst = new SqlParameter("@st", startTime);
            SqlParameter pret = new SqlParameter("@et", endTime);
            SqlParameter prsi = new SqlParameter("@si", SqlDbType.Int);
            prsi.Direction = ParameterDirection.Output;
            SqlParameter pmr = new SqlParameter("@r", SqlDbType.Int);
            pmr.Direction = ParameterDirection.Output;
            context.Database.ExecuteSqlRaw(
                "EXEC @r = usp_AddSurgeryDetails @did, @sc, @sd, @st, @et, @si OUT",
                pmr, prdid, prsc, prsd, prst, pret, prsi);
            result = Convert.ToInt32(pmr.Value);
            surgeryID = Convert.ToInt32(prsi.Value);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            result = -98;
            surgeryID = 0;
        }
        return result;
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        List<SurgeryDetail> obj = new List<SurgeryDetail>();
        try
        {
            // ===== SOLUTION START =====
            obj = context.SurgeryDetails
                .FromSqlInterpolated($"SELECT * FROM ufn_FetchSurgeryDetails({surgeryDate.Date})")
                .ToList();
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            obj = null;
        }
        return obj;
    }
    #endregion
}
