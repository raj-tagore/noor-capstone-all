using Microsoft.AspNetCore.Mvc;
using CureHospitalDALCrossPlatform;
using CureHospitalDALCrossPlatform.Models;

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
        try
        {
            var fetch = repository.FetchDoctorIDs(specializationCode);
            return Json(fetch);
        }
        catch
        {
            return Json(null);
        }
    }
    #endregion

    #region AddDoctorSpecialization - Do not modify the signature
    [HttpPost]
    public bool? AddDoctorSpecialization(CureHospitalWebService.Models.DoctorSpecialization doctorSpecialization)
    {
        bool? status = false;
        try
        {
            status = repository.AddDoctorSpecialization(doctorSpecialization.DoctorId, doctorSpecialization.SpecializationCode, doctorSpecialization.SpecializationDate);
        }
        catch
        {
            status = null;
        }
        return status;
    }
    #endregion

    #region UpdateSurgeryTime - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryTime(int surgeryId, decimal startTime, decimal endTime)
    {
        int surg = -1;
        try
        {
            surg = repository.UpdateSurgeryTime(surgeryId, startTime, endTime);
        }
        catch
        {
            surg = -98;
        }
        return Json(surg);
    }
    #endregion

    #region RemoveSurgeryDetails - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)
    {
        bool? status = false;
        try
        {
            status = repository.RemoveSurgeryDetails(surgeryDate);
        }
        catch
        {
            status = null;
        }
        return Json(status);
    }
    #endregion

    #region AddSurgeryDetails - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryDetails(CureHospitalWebService.Models.Surgery surgery)
    {
        int ans = -95;
        int surgeryId = 0;
        try
        {
            int x = repository.AddSurgeryDetails(surgery.DoctorId, surgery.SurgeryCategory, surgery.SurgeryDate, surgery.StartTime, surgery.EndTime, out surgeryId);
            if (x == 1)
            {
                ans = surgeryId;
            }
            else
            {
                ans = x;
            }
        }
        catch
        {
            ans = -95;
        }
        return Json(ans);
    }
    #endregion

    #region GetSurgeryDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetails(DateTime surgeryDate)
    {
        try
        {
            var det = repository.GetSurgeryDetails(surgeryDate);
            return Json(det);
        }
        catch
        {
            return Json(null);
        }
    }
    #endregion

    // =========================================================================
    // INFOSYS .NET PRACTICAL EXAMINATION PRACTICE EXERCISES (01 - 85)
    // =========================================================================

    #region Exercise 01 - GetDoctorDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorDetails(int doctorId)
    {
        var result = repository.GetDoctorDetails(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 02 - GetAllDoctors - Do not modify the signature
    [HttpGet]
    public JsonResult GetAllDoctors()
    {
        var result = repository.GetAllDoctors();
        return Json(result);
    }
    #endregion

    #region Exercise 03 - GetAllSpecializations - Do not modify the signature
    [HttpGet]
    public JsonResult GetAllSpecializations()
    {
        var result = repository.GetAllSpecializations();
        return Json(result);
    }
    #endregion

    #region Exercise 04 - AddNewDoctor - Do not modify the signature
    [HttpPost]
    public JsonResult AddNewDoctor(string doctorName)
    {
        bool result = repository.AddNewDoctor(doctorName);
        return Json(result);
    }
    #endregion

    #region Exercise 05 - AddNewSpecialization - Do not modify the signature
    [HttpPost]
    public JsonResult AddNewSpecialization(string specializationCode, string specializationName)
    {
        bool result = repository.AddNewSpecialization(specializationCode, specializationName);
        return Json(result);
    }
    #endregion

    #region Exercise 06 - UpdateDoctorName - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateDoctorName(int doctorId, string newName)
    {
        bool result = repository.UpdateDoctorName(doctorId, newName);
        return Json(result);
    }
    #endregion

    #region Exercise 07 - DeleteDoctor - Do not modify the signature
    [HttpDelete]
    public JsonResult DeleteDoctor(int doctorId)
    {
        bool result = repository.DeleteDoctor(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 08 - GetSurgeryById - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryById(int surgeryId)
    {
        var result = repository.GetSurgeryById(surgeryId);
        return Json(result);
    }
    #endregion

    #region Exercise 09 - AddSpecializationWithObject - Do not modify the signature
    [HttpPost]
    public JsonResult AddSpecializationWithObject([FromBody] Specialization specialization)
    {
        bool result = repository.AddSpecializationWithObject(specialization);
        return Json(result);
    }
    #endregion

    #region Exercise 10 - DeleteSpecialization - Do not modify the signature
    [HttpDelete]
    public JsonResult DeleteSpecialization(string specializationCode)
    {
        bool result = repository.DeleteSpecialization(specializationCode);
        return Json(result);
    }
    #endregion

    #region Exercise 11 - AddDoctorWithDuplicateCheck - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorWithDuplicateCheck(string doctorName)
    {
        bool result = repository.AddDoctorWithDuplicateCheck(doctorName);
        return Json(result);
    }
    #endregion

    #region Exercise 12 - AddSpecializationWithDuplicateCheck - Do not modify the signature
    [HttpPost]
    public JsonResult AddSpecializationWithDuplicateCheck(string specializationCode, string specializationName)
    {
        bool result = repository.AddSpecializationWithDuplicateCheck(specializationCode, specializationName);
        return Json(result);
    }
    #endregion

    #region Exercise 13 - AddDoctorSpecializationWithDoctorCheck - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorSpecializationWithDoctorCheck(int doctorId, string specializationCode, DateTime specializationDate)
    {
        var result = repository.AddDoctorSpecializationWithDoctorCheck(doctorId, specializationCode, specializationDate);
        return Json(result);
    }
    #endregion

    #region Exercise 14 - AddDoctorSpecializationWithBothChecks - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorSpecializationWithBothChecks(int doctorId, string specializationCode, DateTime specializationDate)
    {
        var result = repository.AddDoctorSpecializationWithBothChecks(doctorId, specializationCode, specializationDate);
        return Json(result);
    }
    #endregion

    #region Exercise 15 - AddDoctorSpecializationWithTripleCheck - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorSpecializationWithTripleCheck(int doctorId, string specializationCode, DateTime specializationDate)
    {
        var result = repository.AddDoctorSpecializationWithTripleCheck(doctorId, specializationCode, specializationDate);
        return Json(result);
    }
    #endregion

    #region Exercise 16 - UpdateDoctorNameWithNullCheck - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateDoctorNameWithNullCheck(int doctorId, string newName)
    {
        var result = repository.UpdateDoctorNameWithNullCheck(doctorId, newName);
        return Json(result);
    }
    #endregion

    #region Exercise 17 - AddSurgeryWithDoctorValidation - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithDoctorValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        bool result = repository.AddSurgeryWithDoctorValidation(doctorId, surgeryDate, startTime, endTime, surgeryCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 18 - AddSurgeryWithDoctorAndCategoryValidation - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithDoctorAndCategoryValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        var result = repository.AddSurgeryWithDoctorAndCategoryValidation(doctorId, surgeryDate, startTime, endTime, surgeryCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 19 - AddSurgeryWithTimeValidation - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithTimeValidation(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        var result = repository.AddSurgeryWithTimeValidation(doctorId, surgeryDate, startTime, endTime, surgeryCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 20 - UpdateSpecializationName - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSpecializationName(string specializationCode, string newSpecializationName)
    {
        bool result = repository.UpdateSpecializationName(specializationCode, newSpecializationName);
        return Json(result);
    }
    #endregion

    #region Exercise 21 - GetDoctorByName - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorByName(string doctorName)
    {
        var result = repository.GetDoctorByName(doctorName);
        return Json(result);
    }
    #endregion

    #region Exercise 22 - AddSurgeryWithConditionalCategory - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithConditionalCategory(int doctorId, DateTime surgeryDate, decimal startTime, decimal endTime, string surgeryCategory)
    {
        bool result = repository.AddSurgeryWithConditionalCategory(doctorId, surgeryDate, startTime, endTime, surgeryCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 23 - UpdateSurgeryDateAndTime - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryDateAndTime(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        bool result = repository.UpdateSurgeryDateAndTime(surgeryId, newDate, newStartTime, newEndTime);
        return Json(result);
    }
    #endregion

    #region Exercise 24 - UpdateSurgeryDateAndTimeWithValidation - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryDateAndTimeWithValidation(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        var result = repository.UpdateSurgeryDateAndTimeWithValidation(surgeryId, newDate, newStartTime, newEndTime);
        return Json(result);
    }
    #endregion

    #region Exercise 25 - UpdateSurgeryAllProperties - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryAllProperties(int surgeryId, int newDoctorId, DateTime newDate, decimal newStartTime, decimal newEndTime, string newCategory)
    {
        var result = repository.UpdateSurgeryAllProperties(surgeryId, newDoctorId, newDate, newStartTime, newEndTime, newCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 26 - UpdateMultipleSurgeriesByDoctor - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateMultipleSurgeriesByDoctor(int doctorId, string newSurgeryCategory)
    {
        var result = repository.UpdateMultipleSurgeriesByDoctor(doctorId, newSurgeryCategory);
        return Json(result);
    }
    #endregion

    #region Exercise 27 - UpdateMultipleSurgeriesByDate - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateMultipleSurgeriesByDate(DateTime surgeryDate, decimal newEndTime)
    {
        bool result = repository.UpdateMultipleSurgeriesByDate(surgeryDate, newEndTime);
        return Json(result);
    }
    #endregion

    #region Exercise 28 - UpdateMultipleSurgeriesConditionally - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateMultipleSurgeriesConditionally(DateTime surgeryDate)
    {
        var result = repository.UpdateMultipleSurgeriesConditionally(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 29 - UpdateDoctorSpecializationDate - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateDoctorSpecializationDate(int doctorId, string specializationCode, DateTime newDate)
    {
        bool result = repository.UpdateDoctorSpecializationDate(doctorId, specializationCode, newDate);
        return Json(result);
    }
    #endregion

    #region Exercise 30 - ReassignSurgeryToNewDoctor - Do not modify the signature
    [HttpPut]
    public JsonResult ReassignSurgeryToNewDoctor(int surgeryId, int newDoctorId)
    {
        var result = repository.ReassignSurgeryToNewDoctor(surgeryId, newDoctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 31 - ReassignSurgeryCategory - Do not modify the signature
    [HttpPut]
    public JsonResult ReassignSurgeryCategory(int surgeryId, string newSpecializationCode)
    {
        var result = repository.ReassignSurgeryCategory(surgeryId, newSpecializationCode);
        return Json(result);
    }
    #endregion

    #region Exercise 32 - TransferDoctorSpecialization - Do not modify the signature
    [HttpPut]
    public JsonResult TransferDoctorSpecialization(int doctorId, string oldSpecCode, string newSpecCode)
    {
        var result = repository.TransferDoctorSpecialization(doctorId, oldSpecCode, newSpecCode);
        return Json(result);
    }
    #endregion

    #region Exercise 33 - UpdateAllSurgeryTimesForSpecialization - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateAllSurgeryTimesForSpecialization(string specializationCode, decimal addHours)
    {
        var result = repository.UpdateAllSurgeryTimesForSpecialization(specializationCode, addHours);
        return Json(result);
    }
    #endregion

    #region Exercise 34 - RemoveDoctorSpecialization - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveDoctorSpecialization(int doctorId, string specializationCode)
    {
        bool result = repository.RemoveDoctorSpecialization(doctorId, specializationCode);
        return Json(result);
    }
    #endregion

    #region Exercise 35 - RemoveSurgeriesByDoctorId - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveSurgeriesByDoctorId(int doctorId)
    {
        var result = repository.RemoveSurgeriesByDoctorId(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 36 - RemoveSurgeryWithDoctorCheck - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveSurgeryWithDoctorCheck(int surgeryId)
    {
        var result = repository.RemoveSurgeryWithDoctorCheck(surgeryId);
        return Json(result);
    }
    #endregion

    #region Exercise 37 - UpdateSurgeryDoctorWithSpecializationCheck - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateSurgeryDoctorWithSpecializationCheck(int surgeryId, int newDoctorId)
    {
        var result = repository.UpdateSurgeryDoctorWithSpecializationCheck(surgeryId, newDoctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 38 - AddSurgeryWithFullValidation - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithFullValidation(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        var result = repository.AddSurgeryWithFullValidation(doctorId, surgeryCategory, surgeryDate, startTime, endTime);
        return Json(result);
    }
    #endregion

    #region Exercise 39 - GetDoctorsBySpecialization - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorsBySpecialization(string specializationCode)
    {
        var result = repository.GetDoctorsBySpecialization(specializationCode);
        return Json(result);
    }
    #endregion

    #region Exercise 40 - GetSpecializationsForDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetSpecializationsForDoctor(int doctorId)
    {
        var result = repository.GetSpecializationsForDoctor(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 41 - GetSurgeriesByDoctorName - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeriesByDoctorName(string doctorName)
    {
        var result = repository.GetSurgeriesByDoctorName(doctorName);
        return Json(result);
    }
    #endregion

    #region Exercise 42 - GetSurgeryCountByDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCountByDoctor(int doctorId)
    {
        var result = repository.GetSurgeryCountByDoctor(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 43 - GetTotalSurgeryHoursByDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetTotalSurgeryHoursByDoctor(int doctorId)
    {
        var result = repository.GetTotalSurgeryHoursByDoctor(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 44 - GetSurgeriesByDateRange - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeriesByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = repository.GetSurgeriesByDateRange(startDate, endDate);
        return Json(result);
    }
    #endregion

    #region Exercise 45 - GetSurgeriesSortedByDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeriesSortedByDate()
    {
        var result = repository.GetSurgeriesSortedByDate();
        return Json(result);
    }
    #endregion

    #region Exercise 46 - GetDoctorSurgeryInfo - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorSurgeryInfo(int doctorId)
    {
        var result = repository.GetDoctorSurgeryInfo(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 47 - GetSurgeryFullDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryFullDetails(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryFullDetails(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 48 - GetDoctorSpecializationDetails - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorSpecializationDetails()
    {
        var result = repository.GetDoctorSpecializationDetails();
        return Json(result);
    }
    #endregion

    #region Exercise 49 - GetSurgeryCountBySpecialization - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCountBySpecialization()
    {
        var result = repository.GetSurgeryCountBySpecialization();
        return Json(result);
    }
    #endregion

    #region Exercise 50 - GetSurgeryHoursByDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryHoursByDoctor()
    {
        var result = repository.GetSurgeryHoursByDoctor();
        return Json(result);
    }
    #endregion

    #region Exercise 51 - GetDoctorsWithSurgeryOnDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorsWithSurgeryOnDate(DateTime surgeryDate)
    {
        var result = repository.GetDoctorsWithSurgeryOnDate(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 52 - GetSurgeriesByDoctorAndDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeriesByDoctorAndDate(int doctorId, DateTime surgeryDate)
    {
        var result = repository.GetSurgeriesByDoctorAndDate(doctorId, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 53 - GetSurgeryCountByDoctorOnDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCountByDoctorOnDate(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryCountByDoctorOnDate(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 54 - GetSpecializationsWithDoctorCount - Do not modify the signature
    [HttpGet]
    public JsonResult GetSpecializationsWithDoctorCount()
    {
        var result = repository.GetSpecializationsWithDoctorCount();
        return Json(result);
    }
    #endregion

    #region Exercise 55 - GetDoctorSurgerySummary - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorSurgerySummary()
    {
        var result = repository.GetDoctorSurgerySummary();
        return Json(result);
    }
    #endregion

    #region Exercise 56 - GetSurgeryDetailsByDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsByDate(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsByDate(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 57 - GetSurgeryDetailsByDateWithCheck - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsByDateWithCheck(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsByDateWithCheck(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 58 - GetSurgeryDetailsCountByDate - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsCountByDate(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsCountByDate(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 59 - GetSurgeryDetailsByDateFiltered - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsByDateFiltered(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsByDateFiltered(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 60 - GetSurgeryDetailsByDateAndDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsByDateAndDoctor(DateTime surgeryDate, string doctorName)
    {
        var result = repository.GetSurgeryDetailsByDateAndDoctor(surgeryDate, doctorName);
        return Json(result);
    }
    #endregion

    #region Exercise 61 - GetSurgeryDetailsWithValidation - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsWithValidation(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsWithValidation(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 62 - GetSurgeryDetailsAndVerifyDoctor - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsAndVerifyDoctor(DateTime surgeryDate, int doctorId)
    {
        var result = repository.GetSurgeryDetailsAndVerifyDoctor(surgeryDate, doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 63 - GetSurgeryDetailsTotalHours - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsTotalHours(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsTotalHours(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 64 - GetSurgeryCount - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCount(string specializationName, DateTime surgeryDate)
    {
        var result = repository.GetSurgeryCount(specializationName, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 65 - GetSurgeryCountWithCheck - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCountWithCheck(string specializationName, DateTime surgeryDate)
    {
        var result = repository.GetSurgeryCountWithCheck(specializationName, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 66 - GetSurgeryCountForMultipleSpecializations - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryCountForMultipleSpecializations(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryCountForMultipleSpecializations(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 67 - CheckIfSurgeriesExistForSpecialization - Do not modify the signature
    [HttpGet]
    public JsonResult CheckIfSurgeriesExistForSpecialization(string specializationName, DateTime surgeryDate)
    {
        bool result = repository.CheckIfSurgeriesExistForSpecialization(specializationName, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 68 - AddSurgeryViaStoredProc - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryViaStoredProc(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        int surgeryId = 0;
        int status = repository.AddSurgeryViaStoredProc(doctorId, surgeryCategory, surgeryDate, startTime, endTime, out surgeryId);
        return Json(new { status, surgeryId });
    }
    #endregion

    #region Exercise 69 - AddSurgeryViaStoredProcSimplified - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryViaStoredProcSimplified(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        var result = repository.AddSurgeryViaStoredProcSimplified(doctorId, surgeryCategory, surgeryDate, startTime, endTime);
        return Json(result);
    }
    #endregion

    #region Exercise 70 - AddDoctorViaStoredProc - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorViaStoredProc(string doctorName, string specializationCode, string specializationName)
    {
        var result = repository.AddDoctorViaStoredProc(doctorName, specializationCode, specializationName);
        return Json(result);
    }
    #endregion

    #region Exercise 71 - AddDoctorViaStoredProcWithPreCheck - Do not modify the signature
    [HttpPost]
    public JsonResult AddDoctorViaStoredProcWithPreCheck(string doctorName, string specializationCode, string specializationName)
    {
        var result = repository.AddDoctorViaStoredProcWithPreCheck(doctorName, specializationCode, specializationName);
        return Json(result);
    }
    #endregion

    #region Exercise 72 - AddSurgeryViaStoredProcWithControllerLogic - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryViaStoredProcWithControllerLogic([FromBody] CureHospitalWebService.Models.Surgery surgery)
    {
        int surgeryId = 0;
        int status = repository.AddSurgeryViaStoredProcWithControllerLogic(surgery.DoctorId, surgery.SurgeryCategory, surgery.SurgeryDate, surgery.StartTime, surgery.EndTime, out surgeryId);
        return Json(new { status, surgeryId });
    }
    #endregion

    #region Exercise 73 - RegisterDoctorWithSpecialization - Do not modify the signature
    [HttpPost]
    public JsonResult RegisterDoctorWithSpecialization(string doctorName, string specializationCode, DateTime specializationDate)
    {
        var result = repository.RegisterDoctorWithSpecialization(doctorName, specializationCode, specializationDate);
        return Json(result);
    }
    #endregion

    #region Exercise 74 - GetDoctorSchedule - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorSchedule(int doctorId, DateTime surgeryDate)
    {
        var result = repository.GetDoctorSchedule(doctorId, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 75 - RescheduleSurgery - Do not modify the signature
    [HttpPut]
    public JsonResult RescheduleSurgery(int surgeryId, DateTime newDate, decimal newStartTime, decimal newEndTime)
    {
        var result = repository.RescheduleSurgery(surgeryId, newDate, newStartTime, newEndTime);
        return Json(result);
    }
    #endregion

    #region Exercise 76 - GetSpecializationReport - Do not modify the signature
    [HttpGet]
    public JsonResult GetSpecializationReport()
    {
        var result = repository.GetSpecializationReport();
        return Json(result);
    }
    #endregion

    #region Exercise 77 - TransferSurgeriesToNewDoctor - Do not modify the signature
    [HttpPut]
    public JsonResult TransferSurgeriesToNewDoctor(int oldDoctorId, int newDoctorId, DateTime surgeryDate)
    {
        var result = repository.TransferSurgeriesToNewDoctor(oldDoctorId, newDoctorId, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 78 - ValidateAndAddSurgery - Do not modify the signature
    [HttpPost]
    public JsonResult ValidateAndAddSurgery(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        var result = repository.ValidateAndAddSurgery(doctorId, surgeryCategory, surgeryDate, startTime, endTime);
        return Json(result);
    }
    #endregion

    #region Exercise 79 - GetSurgeryDetailsWithScalarCount - Do not modify the signature
    [HttpGet]
    public JsonResult GetSurgeryDetailsWithScalarCount(DateTime surgeryDate)
    {
        var result = repository.GetSurgeryDetailsWithScalarCount(surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 80 - RemoveDoctorCascade - Do not modify the signature
    [HttpDelete]
    public JsonResult RemoveDoctorCascade(int doctorId)
    {
        var result = repository.RemoveDoctorCascade(doctorId);
        return Json(result);
    }
    #endregion

    #region Exercise 81 - AddSurgeryWithSPAndReturnDetails - Do not modify the signature
    [HttpPost]
    public JsonResult AddSurgeryWithSPAndReturnDetails(int doctorId, string surgeryCategory, DateTime surgeryDate, decimal startTime, decimal endTime)
    {
        var result = repository.AddSurgeryWithSPAndReturnDetails(doctorId, surgeryCategory, surgeryDate, startTime, endTime);
        return Json(result);
    }
    #endregion

    #region Exercise 82 - UpdateDoctorSpecializationAndVerifySurgeries - Do not modify the signature
    [HttpPut]
    public JsonResult UpdateDoctorSpecializationAndVerifySurgeries(int doctorId, string oldSpecCode, string newSpecCode)
    {
        var result = repository.UpdateDoctorSpecializationAndVerifySurgeries(doctorId, oldSpecCode, newSpecCode);
        return Json(result);
    }
    #endregion

    #region Exercise 83 - GetDoctorAvailability - Do not modify the signature
    [HttpGet]
    public JsonResult GetDoctorAvailability(int doctorId, DateTime surgeryDate)
    {
        var result = repository.GetDoctorAvailability(doctorId, surgeryDate);
        return Json(result);
    }
    #endregion

    #region Exercise 84 - BatchAddDoctorSpecializations - Do not modify the signature
    [HttpPost]
    public JsonResult BatchAddDoctorSpecializations(int doctorId, [FromBody] List<string> specializationCodes)
    {
        var result = repository.BatchAddDoctorSpecializations(doctorId, specializationCodes);
        return Json(result);
    }
    #endregion

    #region Exercise 85 - GetComprehensiveHospitalReport - Do not modify the signature
    [HttpGet]
    public JsonResult GetComprehensiveHospitalReport()
    {
        var result = repository.GetComprehensiveHospitalReport();
        return Json(result);
    }
    #endregion

}
