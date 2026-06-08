using HealthAppMVC.Models;
using HealthAppMVC.Services.Interface;
using System;

using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{
    public class HealthRecordController
        : Controller
    {
        private readonly IHealthRecordService
            _healthRecordService;

        public HealthRecordController(
            IHealthRecordService
                healthRecordService)
        {
            _healthRecordService =
                healthRecordService;
        }

        // GET:
        // HealthRecord/Create?appointmentId=1
        public ActionResult Create(
            int appointmentId)
        {
            HealthRecord model =
                new HealthRecord
                {
                    AppointmentId =
                        appointmentId
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            HealthRecord record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(record);
                }

               record =  _healthRecordService
                    .AddHealthRecord(
                        record);

                TempData["Success"] =
                    "Health Record Added Successfully";

                return RedirectToAction(
                    "History",
                    new
                    {
                        patientId =
                        record.PatientId
                    });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(record);
            }
        }

        // HealthRecord/History/1
        public ActionResult History(
            int patientId)
        {
            var records =
                _healthRecordService
                .GetPatientHistory(
                    patientId);

            ViewBag.PatientId =
                patientId;

            return View(records);
        }

        // HealthRecord/Details/1
        public ActionResult Details(
            int id)
        {
            var record =
                _healthRecordService
                .GetRecordById(id);

            if (record == null)
            {
                return HttpNotFound();
            }

            return View(record);
        }
    }
}