using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Repositories.Interfaces
{

    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();

        Task<Patient> GetByIdAsync(int id);

        Task<Patient> AddAsync(Patient patient);

        Task UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);

        Task<int> GetAppointmentCountAsync(int patientId);
    }

}
