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
 
    List<int> code = new List<int>();
    try
    {
        code =(from p in context.DoctorSpecializations where
               p.SpecializationCode == specializationCode
               select p.DoctorId).ToList();
               return code;
    }
    catch
    {
        return null;

    }
    
     
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    public bool? AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)
    {
        bool status = false;
        DoctorSpecialization doc = new DoctorSpecialization();
        doc.DoctorId = doctorId;
        doc.SpecializationCode = specializationCode;
        doc.SpecializationDate= specializationDate;
        try
        {
            context.DoctorSpecializations.Add(doc);
            context.SaveChanges();
            return status;
        }
        catch
        {
            return status;

        }
        return status;
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    public int UpdateSurgeryTime(int surgeryID, decimal newStartTime, decimal newEndTime)
    {
        return 0;
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    public bool? RemoveSurgeryDetails(DateTime surgeryDate)
    {
        return false;
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    public int AddSurgeryDetails(int doctorID, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime, out int surgeryID)
    {
        surgeryID = 0;
        return 0;
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    public List<SurgeryDetail> GetSurgeryDetails(DateTime surgeryDate)
    {
        return null;
    }
    #endregion
}
