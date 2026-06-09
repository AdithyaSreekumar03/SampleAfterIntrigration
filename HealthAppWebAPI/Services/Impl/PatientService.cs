using AutoMapper;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Repositories.Interfaces;
using HealthAppWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HealthAppWebAPI.Services.Impl
{

    using AutoMapper;
    using HealthAppMVC.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;

        public PatientService(
            IPatientRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _repo.GetAllAsync();
            return _mapper.Map<List<PatientDto>>(patients);
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _repo.GetByIdAsync(id);

            if (patient == null)
                return null;

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task RegisterPatientAsync(CreatePatientDto dto)
        {
            if (dto.DateOfBirth > DateTime.Today)
                throw new Exception("Future date is not allowed.");

            var patient = _mapper.Map<Patient>(dto);

            var gender = (GenderType)Enum.Parse(
                typeof(GenderType),
                dto.Gender,
                true);

            patient.Gender = gender.ToString();


            patient.CreatedDate = DateTime.Now;

            await _repo.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(int id, CreatePatientDto dto)
        {
            var patient = await _repo.GetByIdAsync(id);

            if (patient == null)
                throw new Exception("Patient not found.");

            _mapper.Map(dto, patient);


            var gender = (GenderType)Enum.Parse(
                typeof(GenderType),
                dto.Gender,
                true);

            patient.Gender = gender.ToString();


            await _repo.UpdateAsync(patient);
        }
    }
}