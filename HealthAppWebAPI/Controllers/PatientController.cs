using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace HealthAppWebAPI.Controllers
{
    public class PatientController : ApiController
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllPatients()
        {
            var patients = await _service.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPatientById(int id)
        {
            var patient = await _service.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPatient([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.AddPatient(patientDto);

            return CreatedAtRoute("DefaultApi", new { id = result.PatientId }, result);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdatePatient(id, patientDto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            var isDeleted = await _service.DeletePatient(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok("Patient deleted successfully");
        }

        [HttpGet]
        [Route("api/patient/email-exists")]
        public async Task<IHttpActionResult> EmailExists(string email)
        {
            var exists = await _service.EmailExists(email);
            return Ok(exists);
        }

        [HttpGet]
        [Route("api/patient/appointment-count/{patientId}")]
        public async Task<IHttpActionResult> GetAppointmentCount(int patientId)
        {
            var count = await _service.GetAppointmentCount(patientId);
            return Ok(count);
        }
    }
}