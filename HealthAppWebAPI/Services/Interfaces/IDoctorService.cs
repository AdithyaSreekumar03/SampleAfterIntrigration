using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Services.Interfaces
{
    using HealthAppWebAPI.Models.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();

        Task<DoctorDto> GetDoctorByIdAsync(int id);

        Task AddDoctorAsync(CreateDoctorDto dto);

        Task UpdateDoctorAsync(int id, CreateDoctorDto dto);

        Task ChangeStatusAsync(int id, bool isActive);

        Task<List<DoctorDto>> GetDoctorsBySpecialisationAsync(string specialisation);
    }
}
