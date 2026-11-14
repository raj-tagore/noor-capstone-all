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
        throw new NotImplementedException();
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    [HttpPost]
    public bool? AddDoctorSpecialization(DoctorSpecialization doctorSpecialization)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryDetails(Models.Surgery surgery)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetails(DateTime surgeryDate)
    {
        throw new NotImplementedException();
    }
    #endregion
}
