using HealthAppMVC.Enums;
using HealthAppWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Repositories.Impl
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealthAppDbContext _context;

        public DoctorRepository(HealthAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Entry(doctor).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(int id, bool isActive)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return;

            doctor.IsActive = isActive;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetBySpecialisationAsync(
            SpecialisationType specialisation)
        {
            return await _context.Doctors
                .Where(d => d.Specialisation == specialisation.ToString())
                .ToListAsync();
        }
    }
}