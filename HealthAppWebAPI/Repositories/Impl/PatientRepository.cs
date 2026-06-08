using HealthAppWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HealthAppWebAPI.Repositories.Impl
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(x => x.PatientId == id);

            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Patients
                .AnyAsync(p => p.Email == email);
        }


        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }


        public async Task<int> GetAppointmentCountAsync(int patientId)
        {
            return await _context.Appointments
                .CountAsync(a => a.PatientId == patientId &&
                                (a.Status == "Pending" || a.Status == "Confirmed"));
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task UpdateAsync(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
