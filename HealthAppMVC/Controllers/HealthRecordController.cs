using HealthAppMVC.Models;
using HealthAppMVC.Services.Interface;
using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{

    public class HealthRecordController : Controller
    {
        private readonly IHealthRecordService _healthRecordService;
        private readonly IPatientService _patientService;

        public HealthRecordController(
            IHealthRecordService healthRecordService,
            IPatientService patientService)
        {
            _healthRecordService = healthRecordService;
            _patientService = patientService;
        }

        public ActionResult Create(int appointmentId)
        {
            var model = new CreateHealthRecordDto
            {
                AppointmentId = appointmentId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateHealthRecordDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                await _healthRecordService.AddHealthRecordAsync(dto);

                TempData["Success"] = "Health Record Added Successfully";

                return RedirectToAction("SearchPatientHistory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var record =
                    await _healthRecordService.GetByIdAsync(id);

                return View(record);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public async Task<ActionResult> SearchPatientHistory(int? patientId)
        {
            IEnumerable<HealthRecordDto> records =
                Enumerable.Empty<HealthRecordDto>();

            if (patientId.HasValue)
            {
                var all = await _healthRecordService.GetAllAsync();

                records = all
                    .Where(r => r.PatientName != null); 
            }

            return View(records);
        }

        public async Task<JsonResult> SearchPatientNames(string term)
        {
            var patients =
                await _patientService.SearchByNameAsync(term);

            var result = patients.Select(p => new
            {
                label = p.FullName,
                value = p.PatientId
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

}