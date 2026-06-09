using HealthAppMVC.Enums;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Repositories.Interfaces;
using HealthAppWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;

    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public DoctorService(
            IDoctorRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _repo.GetAllAsync();
            return _mapper.Map<List<DoctorDto>>(doctors);
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _repo.GetByIdAsync(id);

            if (doctor == null)
                return null;

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task AddDoctorAsync(CreateDoctorDto dto)
        {
            if (!Enum.TryParse(
                dto.Specialisation,
                true,
                out SpecialisationType specialisation))
            {
                throw new Exception("Invalid Specialisation.");
            }

            var doctor = _mapper.Map<Doctor>(dto);

            doctor.Specialisation = specialisation.ToString();
            doctor.IsActive = true;

            await _repo.AddAsync(doctor);
        }

        public async Task UpdateDoctorAsync(
            int id,
            CreateDoctorDto dto)
        {
            var doctor = await _repo.GetByIdAsync(id);

            if (doctor == null)
                throw new Exception("Doctor not found.");

            if (!Enum.TryParse(
                dto.Specialisation,
                true,
                out SpecialisationType specialisation))
            {
                throw new Exception("Invalid Specialisation.");
            }

            // map updated fields
            _mapper.Map(dto, doctor);

            doctor.Specialisation = specialisation.ToString();

            await _repo.UpdateAsync(doctor);
        }

        public async Task ChangeStatusAsync(
            int id,
            bool isActive)
        {
            await _repo.ChangeStatusAsync(id, isActive);
        }

        public async Task<List<DoctorDto>> GetDoctorsBySpecialisationAsync(
            string specialisation)
        {
            if (!Enum.TryParse(
                specialisation,
                true,
                out SpecialisationType spec))
            {
                throw new Exception("Invalid Specialisation.");
            }

            var doctors = await _repo.GetBySpecialisationAsync(spec);

            return _mapper.Map<List<DoctorDto>>(doctors);
        }
    }
}