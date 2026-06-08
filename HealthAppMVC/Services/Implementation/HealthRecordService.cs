using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using HealthAppMVC.Services.Interface;
using System;
using System.Collections.Generic;

namespace HealthAppMVC.Services.Implementation
{
    public class HealthRecordService
        : IHealthRecordService
    {
        private readonly IHealthRecordRepository
            _healthRecordRepository;
        

        private readonly IAppointmentRepository
            _appointmentRepository;

        public HealthRecordService(
            IHealthRecordRepository
                healthRecordRepository,

            IAppointmentRepository
                appointmentRepository)
        {
            _healthRecordRepository =
                healthRecordRepository;

            _appointmentRepository =
                appointmentRepository;
        }

        public List<HealthRecord>
            GetPatientHistory(
                int patientId)
        {
            return _healthRecordRepository
                .GetByPatientId(patientId);
        }

        public HealthRecord GetRecordById(
            int recordId)
        {
            return _healthRecordRepository
                .GetById(recordId);
        }

        public HealthRecord AddHealthRecord(
            HealthRecord record)
        {
            var appointment =
                _appointmentRepository
                .GetById(
                    record.AppointmentId);

            if (appointment == null)
            {
                throw new Exception(
                    "Appointment not found.");
            }

            if (appointment.Status !=
                AppointmentStatus.Confirmed)
            {
                throw new Exception(
                    "Health record can only be added for completed appointments.");
            }

            bool exists =
                _appointmentRepository
                .HealthRecordExists(
                    record.AppointmentId);

            if (exists)
            {
                throw new Exception(
                    "Health record already exists for this appointment.");
            }

            record.PatientId =
                appointment.PatientId;

            record.DoctorId =
                appointment.DoctorId;

            record.VisitDate =
                DateTime.Now;

            _healthRecordRepository
                .Add(record);

            _appointmentRepository.UpdateStatus(
    record.AppointmentId,
    AppointmentStatus.Completed,
    null);

            return record;
        }
    }
}