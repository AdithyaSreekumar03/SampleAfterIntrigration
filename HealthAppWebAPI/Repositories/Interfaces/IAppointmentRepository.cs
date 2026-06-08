using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Repositories.Interfaces
{

    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);

        Task<List<Appointment>> GetAllAsync();

        Task<Appointment> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task UpdateAsync(Appointment appointment);
    }

}
