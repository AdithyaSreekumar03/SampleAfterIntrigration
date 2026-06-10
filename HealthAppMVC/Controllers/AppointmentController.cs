using HealthAppMVC.Models;
using HealthAppMVC.Services.Interface;
using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{

    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index()
        {
            var appointments =
                await _appointmentService.GetAllAppointmentsAsync();

            return View(appointments);
        }

        public async Task<ActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAppointmentDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await LoadDropdowns();
                    return View(dto);
                }

                await _appointmentService.BookAppointmentAsync(dto);

                TempData["Success"] = "Appointment booked successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadDropdowns();
                return View(dto);
            }
        }

        public async Task<ActionResult> Confirm(int id)
        {
            try
            {
                await _appointmentService.ConfirmAppointmentAsync(id);
                TempData["Success"] = "Appointment confirmed.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("UpcomingAppointments");
        }
        
        public async Task<ActionResult> Cancel(int id)
        {
            var appointment =
                await _appointmentService.GetAppointmentByIdAsync(id);

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cancel(int id, string cancellationReason)
        {
            try
            {
                await _appointmentService.CancelAppointmentAsync(id, cancellationReason);

                TempData["Success"] = "Appointment cancelled.";
                return RedirectToAction("UpcomingAppointments");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cancel", new { id });
            }
        }

        private async Task LoadDropdowns()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();

            ViewBag.Patients = new SelectList(patients, "PatientId", "FullName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
        }

        public async Task<ActionResult> UpcomingAppointments(string doctorName)
        {
            var appointments =
                await _appointmentService.GetUpcomingAppointmentsAsync();

            if (!string.IsNullOrWhiteSpace(doctorName))
            {
                appointments = appointments
                    .Where(a => a.DoctorName.ToLower()
                    .Contains(doctorName.ToLower()));
            }

            var appointmentsWithRecords = (await Task.WhenAll(
                appointments.Select(async a => new
                {
                    a.AppointmentId,
                    Exists = await _appointmentService
                        .HealthRecordExistsAsync(a.AppointmentId)
                })
            ))
            .Where(x => x.Exists)
            .Select(x => x.AppointmentId)
            .ToList();

            ViewBag.AppointmentsWithRecords = appointmentsWithRecords;
            ViewBag.DoctorName = doctorName;

            return View(appointments);
        }

        public async Task<JsonResult> SearchDoctorNames(string term)
        {
            var doctors =
                await _doctorService.SearchByNameAsync(term);

            var result = doctors.Select(d => new
            {
                label = d.FullName,
                value = d.FullName
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookAppointment()
        {
            ViewBag.Specialisations =
                Enum.GetValues(typeof(SpecialisationType));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookAppointment(CreateAppointmentDto dto)
        {
            try
            {
                await _appointmentService.BookAppointmentAsync(dto);

                TempData["Success"] = "Appointment booked successfully.";

                return RedirectToAction("PatientServices", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Specialisations =
                    Enum.GetValues(typeof(SpecialisationType));

                return View(dto);
            }
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

        public async Task<JsonResult> GetDoctorsBySpecialisation(string specialisation)
        {
            var doctors =
                await _doctorService
                .SearchBySpecialisationAsync(specialisation);

            var result = doctors
                .Where(d => d.IsActive)
                .Select(d => new
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAvailableSlots(int doctorId, DateTime scheduledDate)
        {
            var slots =
                await _appointmentService
                .GetAvailableSlotsAsync(doctorId, scheduledDate);

            return Json(slots, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewAppointments(string patientName)
        {
            var appointments =
                string.IsNullOrWhiteSpace(patientName)
                ? Enumerable.Empty<AppointmentDto>()
                : await _appointmentService
                    .GetAppointmentsByPatientNameAsync(patientName);

            return View(appointments);
        }
    }

}