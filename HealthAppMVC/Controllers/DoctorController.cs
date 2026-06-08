using HealthAppMVC.Models;
using HealthAppMVC.Services.Interface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(
            IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: Doctor
        public ActionResult Index(
            string specialisation = "")
        {
            var doctors =
                _doctorService.GetAllDoctors();

            if (!string.IsNullOrEmpty(
                specialisation))
            {
                SpecialisationType sp;

                if (Enum.TryParse(
                    specialisation,
                    out sp))
                {
                    doctors =
                        _doctorService
                        .SearchBySpecialisation(sp);
                }
            }

            ViewBag.Specialisations =
                Enum.GetValues(
                    typeof(SpecialisationType));

            return View(doctors);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Doctor doctor =
                    _doctorService
                    .GetDoctorById(id);

                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;

                return RedirectToAction(
                    "Index");
            }
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            try
            {
                _doctorService
                    .AddDoctor(doctor);

                TempData["Success"] =
                    "Doctor added successfully.";

                return RedirectToAction(
                    "Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(doctor);
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Doctor doctor =
                    _doctorService
                    .GetDoctorById(id);

                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;

                return RedirectToAction(
                    "Index");
            }
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            try
            {
                _doctorService
                    .UpdateDoctor(doctor);

                TempData["Success"] =
                    "Doctor updated successfully.";

                return RedirectToAction(
                    "Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(doctor);
            }
        }

        // GET: Doctor/ChangeStatus/5
        public ActionResult ChangeStatus(
            int id)
        {
            try
            {
                Doctor doctor =
                    _doctorService
                    .GetDoctorById(id);

                bool newStatus =
                    !doctor.IsActive;

                _doctorService
                    .ChangeDoctorStatus(
                        id,
                        newStatus);

                TempData["Success"] =
                    "Doctor status updated.";

                return RedirectToAction(
                    "Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;

                return RedirectToAction(
                    "Index");
            }
        }
    }
}