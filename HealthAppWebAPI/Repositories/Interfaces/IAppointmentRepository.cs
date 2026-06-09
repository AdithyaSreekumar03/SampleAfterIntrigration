using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();

        Task<Appointment> GetByIdAsync(int id);

        Task AddAsync(Appointment appointment);

        Task UpdateAsync(Appointment appointment);

        Task<bool> IsDoctorSlotBookedAsync(
            int doctorId,
            DateTime date,
            string slot);

        Task<bool> HasPatientSlotConflictAsync(
            int patientId,
            DateTime date,
            string slot);

        Task<bool> HasAppointmentWithDoctorOnSameDayAsync(
            int patientId,
            int doctorId,
            DateTime date);

        Task<List<Appointment>> GetUpcomingAppointmentsByDoctorAsync(
            int doctorId);

        Task<List<Appointment>> GetAppointmentsByPatientAsync(
            int patientId);
    }
}
