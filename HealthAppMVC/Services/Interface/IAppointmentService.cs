using HealthAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthAppWebAPI.Models.Dtos;


namespace HealthAppMVC.Services.Interface
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task BookAppointmentAsync(CreateAppointmentDto dto);
        Task ConfirmAppointmentAsync(int appointmentId);
        Task CancelAppointmentAsync(int appointmentId, string reason);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId);

        Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync();
        Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsByDoctorAsync(int doctorId);

        Task<IEnumerable<string>> GetAvailableSlotsAsync(int doctorId, DateTime scheduledDate);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientNameAsync(string patientName);

        Task<bool> HealthRecordExistsAsync(int appointmentId);
    }
}
