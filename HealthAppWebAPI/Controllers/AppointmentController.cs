using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HealthAppWebAPI.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentsController : ApiController
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await _service.GetAllAppointmentsAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Book(CreateAppointmentDto dto)
        {
            try
            {
                await _service.BookAppointmentAsync(dto);
                return Ok("Appointment booked.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}/confirm")]
        public async Task<IHttpActionResult> Confirm(int id)
        {
            try
            {
                await _service.ConfirmAppointmentAsync(id);
                return Ok("Appointment confirmed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}/cancel")]
        public async Task<IHttpActionResult> Cancel(
            int id,
            CancelAppointmentDto dto)
        {
            try
            {
                await _service.CancelAppointmentAsync(
                    id,
                    dto.CancellationReason);

                return Ok("Appointment cancelled.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("doctor/{doctorId}/upcoming")]
        public async Task<IHttpActionResult> GetUpcomingForDoctor(
            int doctorId)
        {
            var result = await _service
                .GetUpcomingAppointmentsForDoctorAsync(doctorId);

            return Ok(result);
        }

        [HttpGet]
        [Route("patient/{patientId}")]
        public async Task<IHttpActionResult> GetPatientAppointments(
            int patientId)
        {
            var result = await _service
                .GetAppointmentsForPatientAsync(patientId);

            return Ok(result);
        }
    }
}