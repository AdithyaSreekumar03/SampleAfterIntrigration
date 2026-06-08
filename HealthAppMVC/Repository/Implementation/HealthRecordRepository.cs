using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthAppMVC.Database;
using System.Web;

namespace HealthAppMVC.Repository.Implementation
{
    public class HealthRecordRepository
          : IHealthRecordRepository
    {
        public HealthRecordRepository()
        {
        }

        public void Add(
            HealthRecord record)
        {
            var nextId = InMemoryDatabase.HealthRecords.Any()
                ? InMemoryDatabase.HealthRecords.Max(r => r.RecordId) + 1
                : 1;

            record.RecordId = nextId;
            record.PatientName = InMemoryDatabase.Patients
                .FirstOrDefault(p => p.PatientId == record.PatientId)?.FullName;
            record.DoctorName = InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == record.DoctorId)?.FullName;
            record.Specialisation = InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == record.DoctorId)?.Specialisation.ToString();

            InMemoryDatabase.HealthRecords.Add(record);
        }

        public HealthRecord GetById(
            int recordId)
        {
            return InMemoryDatabase.HealthRecords
                .FirstOrDefault(r => r.RecordId == recordId);
        }

        public List<HealthRecord> GetByPatientId(
            int patientId)
        {
            return InMemoryDatabase.HealthRecords
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.VisitDate)
                .ToList();
        }
    }
}