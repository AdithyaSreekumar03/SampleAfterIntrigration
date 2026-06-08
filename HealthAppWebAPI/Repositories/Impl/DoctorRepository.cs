using HealthAppMVC.Enums;
using HealthAppWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HealthAppMVC.Enums;

namespace HealthAppWebAPI.Repositories.Impl
{
    using HealthAppWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<bool> ChangeStatusAsync(int id, bool isActive)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            return false;

        doctor.IsActive = isActive;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<List<Doctor>> SearchBySpecialisationAsync(SpecialisationType specialisation)
    {

            var specString = specialisation.ToString();

            return await _context.Doctors
                .Where(d => d.Specialisation == specString)
                .ToListAsync();

        }

        public async Task UpdateAsync(Doctor doctor)
    {
        if (doctor == null)
            throw new ArgumentNullException(nameof(doctor));

        _context.Entry(doctor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
}