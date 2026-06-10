using HealthAppMVC.Models;
using System.Collections.Generic;
using HealthAppWebAPI.Models.Dtos;
using System.Threading.Tasks;

namespace HealthAppMVC.Services.Interface
{


    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(CreateDoctorDto dto);
        Task UpdateDoctorAsync(int id, CreateDoctorDto dto);
        Task ChangeDoctorStatusAsync(int doctorId, bool isActive);
        Task<IEnumerable<DoctorDto>> SearchBySpecialisationAsync(string specialisation);
        Task<IEnumerable<DoctorDto>> SearchByNameAsync(string name);

    }
}