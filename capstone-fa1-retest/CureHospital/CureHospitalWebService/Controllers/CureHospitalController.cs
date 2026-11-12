using CureHospitalDALCrossPlatform;
using CureHospitalWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CureHospitalWebService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CureHospitalController : Controller
{
    private readonly CureHospitalRepositoryEF repository;

    public CureHospitalController(CureHospitalRepositoryEF repository)
    {
        this.repository = repository;
    }

    #region FetchDoctorIDs - Do not modify the signature
    [HttpGet]
    public JsonResult FetchDoctorIDs(string specializationCode)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        List<int> x = new List<int>();
        try
        {
            // ===== SOLUTION START =====
            x = repository.FetchDoctorIDs(specializationCode);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            x = null;
        }
        return Json(x);
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    [HttpPost]
    public bool? AddDoctorSpecialization(DoctorSpecialization doctorSpecialization)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        bool? x = false;
        try
        {
            // ===== SOLUTION START =====
            x = repository.AddDoctorSpecialization(
                doctorSpecialization.DoctorId,
                doctorSpecialization.SpecializationCode,
                doctorSpecialization.SpecializationDate);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            x = null;
        }
        return x;
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        int result = -1;
        try
        {
            // ===== SOLUTION START =====
            result = repository.UpdateSurgeryTime(surgeryId, startTime, endTime);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            result = -98;
        }
        return Json(result);
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        bool? status = false;
        try
        {
            // ===== SOLUTION START =====
            status = repository.RemoveSurgeryDetails(surgeryDate);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            status = null;
        }
        return Json(status);
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryDetails(Models.Surgery surgery)
    {
        // GIVEN
        //To Do: Implement appropriate logic and change the return statement as per your logic
        int x = 0;
        int ans = 0;
        int surgeryId = 0;
        try
        {
            // ===== SOLUTION START =====
            x = repository.AddSurgeryDetails(
                surgery.DoctorId,
                surgery.SurgeryCategory,
                surgery.SurgeryDate,
                surgery.StartTime,
                surgery.EndTime,
                out surgeryId);
            if (x == 1)
            {
                ans = surgeryId;
            }
            else
            {
                ans = x;
            }
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            // The Web API column for this action says -95. The DAL catch stays -98.
            ans = -95;
        }
        return Json(ans);
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetails(DateTime surgeryDate)
    {
        // GIVEN
        List<CureHospitalDALCrossPlatform.Models.SurgeryDetail> details = null;
        try
        {
            // ===== SOLUTION START =====
            details = repository.GetSurgeryDetails(surgeryDate);
            // ===== SOLUTION END =====
        }
        catch (Exception)
        {
            details = null;
        }
        return Json(details);
    }
    #endregion
}
