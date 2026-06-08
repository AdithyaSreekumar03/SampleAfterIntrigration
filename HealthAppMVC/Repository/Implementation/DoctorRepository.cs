using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthAppMVC.Database;

namespace HealthAppMVC.Repository.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        public DoctorRepository()
        {
        }

        public List<Doctor> GetAll()
        {
            return InMemoryDatabase.Doctors
                .OrderBy(d => d.FullName)
                .ToList();
        }

        public Doctor GetById(int id)
        {
            return InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == id);
        }

        public void Add(Doctor doctor)
        {
            var nextId = InMemoryDatabase.Doctors.Any()
                ? InMemoryDatabase.Doctors.Max(d => d.DoctorId) + 1
                : 1;

            doctor.DoctorId = nextId;
            InMemoryDatabase.Doctors.Add(doctor);
        }

        public void Update(Doctor doctor)
        {
            var existing = InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == doctor.DoctorId);

            if (existing == null)
                return;

            existing.FullName = doctor.FullName;
            existing.Specialisation = doctor.Specialisation;
            existing.DoctorPhoneNo = doctor.DoctorPhoneNo;
            existing.DoctorEmail = doctor.DoctorEmail;
            existing.YearsOfExperience = doctor.YearsOfExperience;
            existing.ConsultationFee = doctor.ConsultationFee;
            existing.IsActive = doctor.IsActive;
        }

        public void ChangeStatus(
            int id,
            bool isActive)
        {
            var existing = InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == id);

            if (existing == null)
                return;

            existing.IsActive = isActive;
        }

        public List<Doctor> SearchBySpecialisation(
            SpecialisationType specialisation)
        {
            return InMemoryDatabase.Doctors
                .Where(d => d.Specialisation == specialisation)
                .OrderBy(d => d.FullName)
                .ToList();
        }
    }
}