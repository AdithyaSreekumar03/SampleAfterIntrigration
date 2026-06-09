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

        Task AddAsync(Doctor doctor);

        Task UpdateAsync(Doctor doctor);

        Task ChangeStatusAsync(int id, bool isActive);

        Task<List<Doctor>> GetBySpecialisationAsync(
            SpecialisationType specialisation);
    }
}
