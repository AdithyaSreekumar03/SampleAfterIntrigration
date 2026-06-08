using AutoMapper;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Repositories.Interfaces;
using HealthAppWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HealthAppWebAPI.Services.Impl
{

    public class PatientServiceImpl : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public PatientServiceImpl(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PatientDto> AddPatient(PatientDto entity)
        {
            var patient = _mapper.Map<Patient>(entity);

            var savedEntity = await _repository.AddAsync(patient);

            return _mapper.Map<PatientDto>(savedEntity);
        }

        public async Task<List<PatientDto>> GetAllPatients()
        {
            var patients = await _repository.GetAllAsync();

            return _mapper.Map<List<PatientDto>>(patients);
        }

        public async Task<PatientDto> GetById(int patientId)
        {
            var patient = await _repository.GetByIdAsync(patientId);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> UpdatePatient(int id, PatientDto entity)
        {
            var patient = _mapper.Map<Patient>(entity);

            await _repository.UpdateAsync(patient);

            // get updated entity again (optional but good practice)
            var updatedPatient = await _repository.GetByIdAsync(id);

            return _mapper.Map<PatientDto>(updatedPatient);
        }

        public async Task<bool> DeletePatient(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _repository.EmailExistsAsync(email);
        }

        public async Task<int> GetAppointmentCount(int patientId)
        {
            return await _repository.GetAppointmentCountAsync(patientId);
        }

    }
}