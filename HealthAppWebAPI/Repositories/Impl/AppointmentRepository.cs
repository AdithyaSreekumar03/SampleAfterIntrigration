using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using HealthAppWebAPI.Repositories.Interfaces;
using HealthAppWebAPI.Enums;


namespace HealthAppWebAPI.Repositories.Impl
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HealthAppDbContext _context;

        public AppointmentRepository(HealthAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDoctorSlotBookedAsync(
            int doctorId,
            DateTime date,
            string slot)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.DoctorId == doctorId &&
                    DbFunctions.TruncateTime(a.ScheduledDate) ==
                    DbFunctions.TruncateTime(date) &&
                    a.TimeSlot == slot &&
                    a.Status != AppointmentStatus.Cancelled.ToString());
        }

        public async Task<bool> HasPatientSlotConflictAsync(
            int patientId,
            DateTime date,
            string slot)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == patientId &&
                    DbFunctions.TruncateTime(a.ScheduledDate) ==
                    DbFunctions.TruncateTime(date) &&
                    a.TimeSlot == slot &&
                    a.Status != AppointmentStatus.Cancelled.ToString());
        }

        public async Task<bool> HasAppointmentWithDoctorOnSameDayAsync(
            int patientId,
            int doctorId,
            DateTime date)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == patientId &&
                    a.DoctorId == doctorId &&
                    DbFunctions.TruncateTime(a.ScheduledDate) ==
                    DbFunctions.TruncateTime(date) &&
                    a.Status != AppointmentStatus.Cancelled.ToString());
        }
        public async Task<List<Appointment>> GetUpcomingAppointmentsByDoctorAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                    a.DoctorId == doctorId &&
                    (a.Status == AppointmentStatus.Confirmed.ToString() ||
                     a.Status == AppointmentStatus.Pending.ToString()) &&
                    a.ScheduledDate >= DateTime.Today)
                .OrderBy(a => a.ScheduledDate)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientAsync(
            int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.ScheduledDate)
                .ToListAsync();
        }
    }
}