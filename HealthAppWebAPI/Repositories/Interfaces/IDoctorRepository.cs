using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthAppMVC.Enums;

namespace HealthAppWebAPI.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(int id);

        Task<Doctor> AddAsync(Doctor doctor);

        Task UpdateAsync(Doctor doctor);

        Task<bool> ChangeStatusAsync(int id, bool isActive);

        Task<List<Doctor>> SearchBySpecialisationAsync(SpecialisationType specialisation);
    }
}
