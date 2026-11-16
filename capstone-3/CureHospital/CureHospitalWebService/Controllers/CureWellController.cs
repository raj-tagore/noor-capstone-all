using CureHospitalDALCrossPlatform;
using CureHospitalDALCrossPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CureHospitalWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CureWellController : Controller
    {
        CureHospitalRepositoryEF repository;

        public CureWellController(CureHospitalRepositoryEF repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetAllSurgeryDates()
        {
            try
            {
                return Json(repository.GetAllSurgeryDates());
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        [HttpGet]
        public JsonResult GetSurgeryDetails(DateTime surgeryDate)
        {
            try
            {
                return Json(repository.GetSurgeryDetails(surgeryDate));
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        [HttpPut]
        public bool UpdateSurgeryDetails(SurgeryDetail updatedSurgery)
        {
            try
            {
                return repository.UpdateSurgeryDetails(updatedSurgery);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult AddSurgeryDetails(Models.Surgery surgery)
        {
            try
            {
                int surgeryId;
                int result = repository.AddSurgeryDetails(
                    surgery.DoctorId,
                    surgery.SurgeryCategory,
                    surgery.SurgeryDate,
                    surgery.StartTime,
                    surgery.EndTime,
                    out surgeryId);

                if (result == 1)
                {
                    return Json(surgeryId);
                }

                return Json(result);
            }
            catch (Exception)
            {
                return Json(-98);
            }
        }
    }
}
