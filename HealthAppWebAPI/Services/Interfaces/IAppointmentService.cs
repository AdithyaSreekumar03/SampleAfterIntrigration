using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthAppWebAPI.Models.Dtos;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Services.Interfaces
{

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAppointmentsAsync();

        Task<List<AppointmentDto>> GetUpcomingAppointmentsForDoctorAsync(
            int doctorId);

        Task<List<AppointmentDto>> GetAppointmentsForPatientAsync(
            int patientId);

        Task BookAppointmentAsync(CreateAppointmentDto dto);

        Task ConfirmAppointmentAsync(int id);

        Task CancelAppointmentAsync(int id, string reason);
    }

}
