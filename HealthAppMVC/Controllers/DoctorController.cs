using HealthAppMVC.Models;
using HealthAppMVC.Services.Interface;
using HealthAppWebAPI.Models.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{

    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index(string specialisation = "")
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            if (!string.IsNullOrEmpty(specialisation))
            {
                doctors = await _doctorService
                    .SearchBySpecialisationAsync(specialisation);
            }

            ViewBag.Specialisations =
                Enum.GetValues(typeof(SpecialisationType));

            return View(doctors);
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _doctorService.AddDoctorAsync(dto);

                TempData["Success"] = "Doctor Registered Successfully";
                return RedirectToAction("DoctorServices", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("DoctorServices", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _doctorService.UpdateDoctorAsync(id, dto);

                TempData["Success"] = "Doctor Details Updated Successfully";
                return RedirectToAction("DoctorServices", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public async Task<ActionResult> ChangeStatus(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);

                bool newStatus = !doctor.IsActive;

                await _doctorService
                    .ChangeDoctorStatusAsync(id, newStatus);

                TempData["Success"] = "Doctor status updated.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult SearchDoctor()
        {
            return View();
        }

        public async Task<JsonResult> SearchDoctorNames(string term)
        {
            var doctors = await _doctorService.SearchByNameAsync(term);

            var result = doctors.Select(d => new
            {
                label = d.FullName,
                value = d.DoctorId
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDoctorByName(int id)
        {
            return RedirectToAction("Edit", new { id = id });
        }

        public async Task<ActionResult> DoctorSearch(
            string doctorName,
            string specialisation)
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();

            if (!string.IsNullOrWhiteSpace(doctorName))
            {
                doctors = doctors.Where(d =>
                    d.FullName.ToLower()
                    .Contains(doctorName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(specialisation))
            {
                doctors = doctors
                    .Where(d => d.Specialisation
                    .Equals(specialisation,
                        StringComparison.OrdinalIgnoreCase));
            }

            return View(doctors);
        }

    }
}