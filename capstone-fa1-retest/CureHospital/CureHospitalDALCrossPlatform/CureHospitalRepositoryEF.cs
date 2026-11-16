using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CureHospitalDALCrossPlatform.Models;

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
        List<int> doc = new List<int>();
        try
        {
            doc = (from d in context.DoctorSpecializations
                   where d.SpecializationCode == specializationCode
                   select d.DoctorId).ToList();
        }
        catch
        {
            doc = null;
        }
        return doc;
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)
    {
        bool? status = false;
        try
        {
            DoctorSpecialization spec = new DoctorSpecialization
            {
                DoctorId = doctorId,
                SpecializationCode = specializationCode,
                SpecializationDate = specializationDate
            };
            context.DoctorSpecializations.Add(spec);
            context.SaveChanges();
            status = true;
        }
        catch
        {
            status = false;
        }
        return status;
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)
    {
        int status = -1;
        try
        {
            if (newEndTime <= newStartTime)
            {
                return -1;
            }

            Surgery surg = context.Surgeries.Find(surgeryID);
            if (surg == null)
            {
                return -98;
            }

            surg.StartTime = newStartTime;
            surg.EndTime = newEndTime;
            context.SaveChanges();
            status = 1;
        }
        catch
        {
            status = -98;
        }
        return status;
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    public bool? RemoveSurgeryDetails(DateTime surgeryDate)
    {
        bool? status = false;
        try
        {
            var sList = (from r in context.Surgeries
                         where r.SurgeryDate.Date == surgeryDate.Date
                         select r).ToList();

            if (sList == null || sList.Count == 0)
            {
                return false;
            }

            context.Surgeries.RemoveRange(sList);
            context.SaveChanges();
            status = true;
        }
        catch
        {
            status = false;
        }
        return status;
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)
    {
        surgeryID = 0;
        int status = -98;
        try
        {
            if (surgeryCategory == null)
            {
                return -98;
            }

            SqlParameter docParam = new SqlParameter("@did", doctorID);
            SqlParameter catParam = new SqlParameter("@sc", surgeryCategory);
            SqlParameter dateParam = new SqlParameter("@sd", surgeryDate.Date);
            SqlParameter startParam = new SqlParameter("@st", startTime);
            SqlParameter endParam = new SqlParameter("@et", endTime);

            SqlParameter outParam = new SqlParameter("@si", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter retParam = new SqlParameter("@r", SqlDbType.Int)
            {
                Direction = ParameterDirection.ReturnValue
            };

            context.Database.ExecuteSqlRaw(
                "EXEC @r = usp_AddSurgeryDetails @did, @sc, @sd, @st, @et, @si OUT",
                retParam, docParam, catParam, dateParam, startParam, endParam, outParam
            );

            status = Convert.ToInt32(retParam.Value);
            surgeryID = Convert.ToInt32(outParam.Value);
        }
        catch
        {
            surgeryID = 0;
            status = -98;
        }
        return status;
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)
    {
        List<SurgeryDetail> details = new List<SurgeryDetail>();
        try
        {
            details = context.SurgeryDetails
                .FromSqlInterpolated($"SELECT * FROM ufn_FetchSurgeryDetails({surgeryDate.Date})")
                .ToList();
        }
        catch
        {
            details = null;
        }
        return details;
    }
    #endregion

    // =========================================================================
    // INFOSYS .NET PRACTICAL EXAMINATION PRACTICE EXERCISES (01 - 85)
    // =========================================================================

    #region Exercise 01 - GetDoctorDetails - Do not modify the signature
    public Doctor GetDoctorDetails(int doctorId)
    {
        
        try{
       Doctor doc = context.Doctors.Find(doctorId);
       return doc;
           }
           catch
           {
         
         return null;

           }
            

    }
    #endregion

    #region Exercise 02 - GetAllDoctors - Do not modify the signature
    public List<Doctor> GetAllDoctors()
    {
        List<Doctor> result = new List<Doctor>();
        try
        {
            result = (from d in context.Doctors select d).ToList();
            return result;
        }
        catch
        {
            return null;
        }
       
    }
    #endregion

    #region Exercise 03 - GetAllSpecializations - Do not modify the signature
    public List<Specialization> GetAllSpecializations()
    {
        List<Specialization> result = new List<Specialization>();
        try
        {
            result = (from s in context.Specializations select s).ToList();
            return result;
        }
        catch
        {
            return null;
        }
        
     
    }
    #endregion

    #region Exercise 04 - AddNewDoctor - Do not modify the signature
    public bool AddNewDoctor(string doctorName)
    {
        bool result = false;
        Doctor doc = new Doctor();
        try
        {
            doc.DoctorName = doctorName;
            context.Doctors.Add(doc);
            context.SaveChanges();
            result = true;
        }
        catch
        {
            result = false;
        }
        return result;
    }
    #endregion

    #region Exercise 05 - AddNewSpecialization - Do not modify the signature
    public bool AddNewSpecialization(string specializationCode, string specializationName)
    {
        bool result = false;
       // List<Specialization> s = new Specialization();

        var s = new Specialization();                                                                                                                   
        try
        {
            s.SpecializationCode = specializationCode;
            s.SpecializationName = specializationName;
            
            context.Specializations.Add(s);
            context.SaveChanges();
            result = true;
        }
        catch
        {
            result = false;
        }
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 06 - UpdateDoctorName - Do not modify the signature
    public bool UpdateDoctorName(int doctorId, string newName)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 07 - DeleteDoctor - Do not modify the signature
    public bool DeleteDoctor(int doctorId)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 08 - GetSurgeryById - Do not modify the signature
    public Surgery GetSurgeryById(int surgeryId)
    {
        Surgery result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 09 - AddSpecializationWithObject - Do not modify the signature
    public bool AddSpecializationWithObject(Specialization specialization)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 10 - DeleteSpecialization - Do not modify the signature
    public bool DeleteSpecialization(string specializationCode)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 11 - AddDoctorWithDuplicateCheck - Do not modify the signature
    public bool AddDoctorWithDuplicateCheck(string doctorName)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 12 - AddSpecializationWithDuplicateCheck - Do not modify the signature
    public bool AddSpecializationWithDuplicateCheck(string specializationCode, string specializationName)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 13 - AddDoctorSpecializationWithDoctorCheck - Do not modify the signature
    public int AddDoctorSpecializationWithDoctorCheck(int doctorId, string specializationCode, DateTime specializationDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 14 - AddDoctorSpecializationWithBothChecks - Do not modify the signature
    public int AddDoctorSpecializationWithBothChecks(int doctorId, string specializationCode, DateTime specializationDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 15 - AddDoctorSpecializationWithTripleCheck - Do not modify the signature
    public int AddDoctorSpecializationWithTripleCheck(int doctorId, string specializationCode, DateTime specializationDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 16 - UpdateDoctorNameWithNullCheck - Do not modify the signature
    public int UpdateDoctorNameWithNullCheck(int doctorId, string newName)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 17 - AddSurgeryWithDoctorValidation - Do not modify the signature
    public bool AddSurgeryWithDoctorValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 18 - AddSurgeryWithDoctorAndCategoryValidation - Do not modify the signature
    public int AddSurgeryWithDoctorAndCategoryValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 19 - AddSurgeryWithTimeValidation - Do not modify the signature
    public int AddSurgeryWithTimeValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 20 - UpdateSpecializationName - Do not modify the signature
    public bool UpdateSpecializationName(string specializationCode, string newSpecializationName)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 21 - GetDoctorByName - Do not modify the signature
    public Doctor GetDoctorByName(string doctorName)
    {
        Doctor result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 22 - AddSurgeryWithConditionalCategory - Do not modify the signature
    public bool AddSurgeryWithConditionalCategory(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 23 - UpdateSurgeryDateAndTime - Do not modify the signature
    public bool UpdateSurgeryDateAndTime(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 24 - UpdateSurgeryDateAndTimeWithValidation - Do not modify the signature
    public int UpdateSurgeryDateAndTimeWithValidation(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 25 - UpdateSurgeryAllProperties - Do not modify the signature
    public int UpdateSurgeryAllProperties(int surgeryId, int newDoctorId, DateTime newDate, decimal newStartTime, decimal newEndTime, string newCategory)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 26 - UpdateMultipleSurgeriesByDoctor - Do not modify the signature
    public int UpdateMultipleSurgeriesByDoctor(int doctorId, string newSurgeryCategory)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 27 - UpdateMultipleSurgeriesByDate - Do not modify the signature
    public bool UpdateMultipleSurgeriesByDate(DateTime surgeryDate, decimal newEndTime)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 28 - UpdateMultipleSurgeriesConditionally - Do not modify the signature
    public int UpdateMultipleSurgeriesConditionally(DateTime surgeryDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 29 - UpdateDoctorSpecializationDate - Do not modify the signature
    public bool UpdateDoctorSpecializationDate(int doctorId, string specializationCode, DateTime newDate)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 30 - ReassignSurgeryToNewDoctor - Do not modify the signature
    public int ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 31 - ReassignSurgeryCategory - Do not modify the signature
    public int ReassignSurgeryCategory(int surgeryId, string newSpecializationCode)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 32 - TransferDoctorSpecialization - Do not modify the signature
    public int TransferDoctorSpecialization(int doctorId, string oldSpecCode, string newSpecCode)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 33 - UpdateAllSurgeryTimesForSpecialization - Do not modify the signature
    public int UpdateAllSurgeryTimesForSpecialization(string specializationCode, decimal addHours)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 34 - RemoveDoctorSpecialization - Do not modify the signature
    public bool RemoveDoctorSpecialization(int doctorId, string specializationCode)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 35 - RemoveSurgeriesByDoctorId - Do not modify the signature
    public int RemoveSurgeriesByDoctorId(int doctorId)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 36 - RemoveSurgeryWithDoctorCheck - Do not modify the signature
    public int RemoveSurgeryWithDoctorCheck(int surgeryId)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 37 - UpdateSurgeryDoctorWithSpecializationCheck - Do not modify the signature
    public int UpdateSurgeryDoctorWithSpecializationCheck(int surgeryId, int newDoctorId)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 38 - AddSurgeryWithFullValidation - Do not modify the signature
    public int AddSurgeryWithFullValidation(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 39 - GetDoctorsBySpecialization - Do not modify the signature
    public List<Doctor> GetDoctorsBySpecialization(string specializationCode)
    {
        List<Doctor> result = new List<Doctor>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 40 - GetSpecializationsForDoctor - Do not modify the signature
    public List<Specialization> GetSpecializationsForDoctor(int doctorId)
    {
        List<Specialization> result = new List<Specialization>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 41 - GetSurgeriesByDoctorName - Do not modify the signature
    public List<Surgery> GetSurgeriesByDoctorName(string doctorName)
    {
        List<Surgery> result = new List<Surgery>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 42 - GetSurgeryCountByDoctor - Do not modify the signature
    public int GetSurgeryCountByDoctor(int doctorId)
    {
        int result = -1;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 43 - GetTotalSurgeryHoursByDoctor - Do not modify the signature
    public decimal GetTotalSurgeryHoursByDoctor(int doctorId)
    {
        decimal result = 0m;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 44 - GetSurgeriesByDateRange - Do not modify the signature
    public List<Surgery> GetSurgeriesByDateRange(DateTime startDate, DateTime endDate)
    {
        List<Surgery> result = new List<Surgery>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 45 - GetSurgeriesSortedByDate - Do not modify the signature
    public List<Surgery> GetSurgeriesSortedByDate()
    {
        List<Surgery> result = new List<Surgery>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 46 - GetDoctorSurgeryInfo - Do not modify the signature
    public List<DoctorSurgeryInfo> GetDoctorSurgeryInfo(int doctorId)
    {
        List<DoctorSurgeryInfo> result = new List<DoctorSurgeryInfo>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 47 - GetSurgeryFullDetails - Do not modify the signature
    public List<SurgeryFullDetail> GetSurgeryFullDetails(DateTime surgeryDate)
    {
        List<SurgeryFullDetail> result = new List<SurgeryFullDetail>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 48 - GetDoctorSpecializationDetails - Do not modify the signature
    public List<DoctorSpecDetail> GetDoctorSpecializationDetails()
    {
        List<DoctorSpecDetail> result = new List<DoctorSpecDetail>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 49 - GetSurgeryCountBySpecialization - Do not modify the signature
    public List<SpecializationSurgeryCount> GetSurgeryCountBySpecialization()
    {
        List<SpecializationSurgeryCount> result = new List<SpecializationSurgeryCount>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 50 - GetSurgeryHoursByDoctor - Do not modify the signature
    public List<DoctorSurgeryHours> GetSurgeryHoursByDoctor()
    {
        List<DoctorSurgeryHours> result = new List<DoctorSurgeryHours>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 51 - GetDoctorsWithSurgeryOnDate - Do not modify the signature
    public List<string> GetDoctorsWithSurgeryOnDate(DateTime surgeryDate)
    {
        List<string> result = new List<string>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 52 - GetSurgeriesByDoctorAndDate - Do not modify the signature
    public List<Surgery> GetSurgeriesByDoctorAndDate(int doctorId, DateTime surgeryDate)
    {
        List<Surgery> result = new List<Surgery>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 53 - GetSurgeryCountByDoctorOnDate - Do not modify the signature
    public List<DoctorDateSurgeryCount> GetSurgeryCountByDoctorOnDate(DateTime surgeryDate)
    {
        List<DoctorDateSurgeryCount> result = new List<DoctorDateSurgeryCount>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 54 - GetSpecializationsWithDoctorCount - Do not modify the signature
    public List<SpecDoctorCount> GetSpecializationsWithDoctorCount()
    {
        List<SpecDoctorCount> result = new List<SpecDoctorCount>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 55 - GetDoctorSurgerySummary - Do not modify the signature
    public List<DoctorSurgerySummary> GetDoctorSurgerySummary()
    {
        List<DoctorSurgerySummary> result = new List<DoctorSurgerySummary>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 56 - GetSurgeryDetailsByDate - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetailsByDate(DateTime surgeryDate)
    {
        List<SurgeryDetail> result = new List<SurgeryDetail>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 57 - GetSurgeryDetailsByDateWithCheck - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetailsByDateWithCheck(DateTime surgeryDate)
    {
        List<SurgeryDetail> result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 58 - GetSurgeryDetailsCountByDate - Do not modify the signature
    public int GetSurgeryDetailsCountByDate(DateTime surgeryDate)
    {
        int result = 0;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 59 - GetSurgeryDetailsByDateFiltered - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetailsByDateFiltered(DateTime surgeryDate)
    {
        List<SurgeryDetail> result = new List<SurgeryDetail>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 60 - GetSurgeryDetailsByDateAndDoctor - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetailsByDateAndDoctor(DateTime surgeryDate, string doctorName)
    {
        List<SurgeryDetail> result = new List<SurgeryDetail>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 61 - GetSurgeryDetailsWithValidation - Do not modify the signature
    public int GetSurgeryDetailsWithValidation(DateTime surgeryDate)
    {
        int result = -1;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 62 - GetSurgeryDetailsAndVerifyDoctor - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetailsAndVerifyDoctor(DateTime surgeryDate, int doctorId)
    {
        List<SurgeryDetail> result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 63 - GetSurgeryDetailsTotalHours - Do not modify the signature
    public decimal GetSurgeryDetailsTotalHours(DateTime surgeryDate)
    {
        decimal result = 0m;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 64 - GetSurgeryCount - Do not modify the signature
    public int GetSurgeryCount(string specializationName, DateTime surgeryDate)
    {
        int result = -1;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 65 - GetSurgeryCountWithCheck - Do not modify the signature
    public int GetSurgeryCountWithCheck(string specializationName, DateTime surgeryDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 66 - GetSurgeryCountForMultipleSpecializations - Do not modify the signature
    public List<SpecSurgeryCount> GetSurgeryCountForMultipleSpecializations(DateTime surgeryDate)
    {
        List<SpecSurgeryCount> result = new List<SpecSurgeryCount>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 67 - CheckIfSurgeriesExistForSpecialization - Do not modify the signature
    public bool CheckIfSurgeriesExistForSpecialization(string specializationName, DateTime surgeryDate)
    {
        bool result = false;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 68 - AddSurgeryViaStoredProc - Do not modify the signature
    public int AddSurgeryViaStoredProc(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
    {
        surgeryId = 0;
        int result = 0;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 69 - AddSurgeryViaStoredProcSimplified - Do not modify the signature
    public int AddSurgeryViaStoredProcSimplified(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        int result = -98;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 70 - AddDoctorViaStoredProc - Do not modify the signature
    public int AddDoctorViaStoredProc(string doctorName, string specializationCode, string specializationName)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 71 - AddDoctorViaStoredProcWithPreCheck - Do not modify the signature
    public int AddDoctorViaStoredProcWithPreCheck(string doctorName, string specializationCode, string specializationName)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 72 - AddSurgeryViaStoredProcWithControllerLogic - Do not modify the signature
    public int AddSurgeryViaStoredProcWithControllerLogic(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryId)
    {
        surgeryId = 0;
        int result = 0;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 73 - RegisterDoctorWithSpecialization - Do not modify the signature
    public int RegisterDoctorWithSpecialization(string doctorName, string specializationCode, DateTime specializationDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 74 - GetDoctorSchedule - Do not modify the signature
    public DoctorSchedule GetDoctorSchedule(int doctorId, DateTime surgeryDate)
    {
        DoctorSchedule result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 75 - RescheduleSurgery - Do not modify the signature
    public int RescheduleSurgery(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 76 - GetSpecializationReport - Do not modify the signature
    public List<SpecializationReport> GetSpecializationReport()
    {
        List<SpecializationReport> result = new List<SpecializationReport>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 77 - TransferSurgeriesToNewDoctor - Do not modify the signature
    public int TransferSurgeriesToNewDoctor(int oldDoctorId, int newDoctorId, DateTime surgeryDate)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 78 - ValidateAndAddSurgery - Do not modify the signature
    public int ValidateAndAddSurgery(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 79 - GetSurgeryDetailsWithScalarCount - Do not modify the signature
    public List<SurgeryDetailWithCount> GetSurgeryDetailsWithScalarCount(DateTime surgeryDate)
    {
        List<SurgeryDetailWithCount> result = new List<SurgeryDetailWithCount>();
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 80 - RemoveDoctorCascade - Do not modify the signature
    public int RemoveDoctorCascade(int doctorId)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 81 - AddSurgeryWithSPAndReturnDetails - Do not modify the signature
    public SurgeryDetail AddSurgeryWithSPAndReturnDetails(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        SurgeryDetail result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 82 - UpdateDoctorSpecializationAndVerifySurgeries - Do not modify the signature
    public int UpdateDoctorSpecializationAndVerifySurgeries(int doctorId, string oldSpecCode, string newSpecCode)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 83 - GetDoctorAvailability - Do not modify the signature
    public DoctorAvailability GetDoctorAvailability(int doctorId, DateTime surgeryDate)
    {
        DoctorAvailability result = null;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 84 - BatchAddDoctorSpecializations - Do not modify the signature
    public int BatchAddDoctorSpecializations(int doctorId, List<string> specializationCodes)
    {
        int result = -99;
        // Write your code here
        return result;
    }
    #endregion

    #region Exercise 85 - GetComprehensiveHospitalReport - Do not modify the signature
    public HospitalReport GetComprehensiveHospitalReport()
    {
        HospitalReport result = null;
        // Write your code here
        return result;
    }
    #endregion

}
