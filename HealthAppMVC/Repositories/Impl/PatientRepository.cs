using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthAppMVC.Database;

namespace HealthAppMVC.Repository.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        public PatientRepository()
        {
        }

        public List<Patient> GetAll()
        {
            return InMemoryDatabase.Patients
                .OrderBy(p => p.FullName)
                .ToList();
        }

        public Patient GetById(int id)
        {
            return InMemoryDatabase.Patients
                .FirstOrDefault(p => p.PatientId == id);
        }

        public void Add(Patient patient)
        {
            var nextId = InMemoryDatabase.Patients.Any()
                ? InMemoryDatabase.Patients.Max(p => p.PatientId) + 1
                : 1;

            patient.PatientId = nextId;
            patient.CreatedDate = DateTime.Now;

            InMemoryDatabase.Patients.Add(patient);
        }

        public void Update(Patient patient)
        {
            var existing = InMemoryDatabase.Patients
                .FirstOrDefault(p => p.PatientId == patient.PatientId);

            if (existing == null)
                return;

            existing.FullName = patient.FullName;
            existing.DateOfBirth = patient.DateOfBirth;
            existing.Gender = patient.Gender;
            existing.PhoneNumber = patient.PhoneNumber;
            existing.Email = patient.Email;
            existing.InsuranceId = patient.InsuranceId;
        }

       

        public bool EmailExists(string email)
        {
            return InMemoryDatabase.Patients
                .Any(p => string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public int GetAppointmentCount(int patientId)
        {
            return InMemoryDatabase.Appointments
                .Count(a => a.PatientId == patientId);
        }
    }
}