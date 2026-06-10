using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;


namespace HealthAppWebAPI.Controllers
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await _service.GetAllPatientsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var patient = await _service.GetPatientByIdAsync(id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(CreatePatientDto dto)
        {
            try
            {
                await _service.RegisterPatientAsync(dto);
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(
            int id,
            CreatePatientDto dto)
        {
            try
            {
                await _service.UpdatePatientAsync(id, dto);
                return Ok("Patient updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> SearchByName(string name)
        {
            var all = await _service.GetAllPatientsAsync();

            var result = all
                .Where(p => p.FullName.ToLower()
                .Contains(name.ToLower()))
                .ToList();

            return Ok(result);
        }

    }
}