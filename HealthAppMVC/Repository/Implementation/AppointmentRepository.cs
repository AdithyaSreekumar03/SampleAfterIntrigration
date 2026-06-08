using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthAppMVC.Database;
using System.Web;

namespace HealthAppMVC.Repository.Implementation
{
    public class AppointmentRepository
       : IAppointmentRepository
    {
        public AppointmentRepository()
        {
        }

        public List<Appointment> GetAll()
        {
            return InMemoryDatabase.Appointments
                .Select(a =>
                {
                    a.PatientName = InMemoryDatabase.Patients
                        .FirstOrDefault(p => p.PatientId == a.PatientId)?.FullName;
                    a.DoctorName = InMemoryDatabase.Doctors
                        .FirstOrDefault(d => d.DoctorId == a.DoctorId)?.FullName;
                    return a;
                })
                .ToList();
        }

        public Appointment GetById(int id)
        {
            var appointment = InMemoryDatabase.Appointments
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment != null)
            {
                appointment.PatientName = InMemoryDatabase.Patients
                    .FirstOrDefault(p => p.PatientId == appointment.PatientId)?.FullName;
                appointment.DoctorName = InMemoryDatabase.Doctors
                    .FirstOrDefault(d => d.DoctorId == appointment.DoctorId)?.FullName;
            }

            return appointment;
        }

        public void Add(
            Appointment appointment)
        {
            var nextId = InMemoryDatabase.Appointments.Any()
                ? InMemoryDatabase.Appointments.Max(a => a.AppointmentId) + 1
                : 1;

            appointment.AppointmentId = nextId;
            appointment.PatientName = InMemoryDatabase.Patients
                .FirstOrDefault(p => p.PatientId == appointment.PatientId)?.FullName;
            appointment.DoctorName = InMemoryDatabase.Doctors
                .FirstOrDefault(d => d.DoctorId == appointment.DoctorId)?.FullName;

            InMemoryDatabase.Appointments.Add(appointment);
        }

        public void UpdateStatus(
            int appointmentId,
            AppointmentStatus status,
            string cancellationReason)
        {
            var existing = InMemoryDatabase.Appointments
                .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (existing == null)
                return;

            existing.Status = status;
            existing.CancellationReason = cancellationReason;
        }

       

        public bool IsSlotAvailable(
            int doctorId,
            string date,
            string timeSlot)
        {
            DateTime dt = Convert.ToDateTime(date).Date;

            return !InMemoryDatabase.Appointments
                .Any(a => a.DoctorId == doctorId
                          && a.ScheduledDate.Date == dt
                          && a.TimeSlot == timeSlot
                          && a.Status != AppointmentStatus.Cancelled);
        }

        public List<Appointment>
            GetAppointmentsByPatient(
            int patientId)
        {
            return InMemoryDatabase.Appointments
                .Where(a => a.PatientId == patientId)
                .Select(a => new Appointment
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    ScheduledDate = a.ScheduledDate,
                    TimeSlot = a.TimeSlot,
                    Status = a.Status,
                    CancellationReason = a.CancellationReason
                })
                .ToList();
        }

        public List<Appointment>
            GetAppointmentsByDoctor(
            int doctorId)
        {
            return InMemoryDatabase.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new Appointment
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    ScheduledDate = a.ScheduledDate,
                    TimeSlot = a.TimeSlot,
                    Status = a.Status,
                    CancellationReason = a.CancellationReason
                })
                .ToList();
        }

        private Appointment
            MapAppointment(
            object reader)
        {
            // Not used in in-memory implementation but kept for signature compatibility
            return null;
        }

        public bool IsDoctorSlotBooked(
    int doctorId,
    DateTime scheduledDate,
    string timeSlot)
        {
            return InMemoryDatabase.Appointments
                .Any(a => a.DoctorId == doctorId
                          && a.ScheduledDate.Date == scheduledDate.Date
                          && a.TimeSlot == timeSlot
                          && a.Status != AppointmentStatus.Cancelled);
        }

        public bool HasPatientAppointmentOnDate(
    int patientId,
    int doctorId,
    DateTime scheduledDate)
        {
            return InMemoryDatabase.Appointments
                .Any(a => a.PatientId == patientId
                          && a.DoctorId == doctorId
                          && a.ScheduledDate.Date == scheduledDate.Date
                          && a.Status != AppointmentStatus.Cancelled);
        }

        public bool HasPatientSlotConflict(
    int patientId,
    DateTime scheduledDate,
    string timeSlot)
        {
            return InMemoryDatabase.Appointments
                .Any(a => a.PatientId == patientId
                          && a.ScheduledDate.Date == scheduledDate.Date
                          && a.TimeSlot == timeSlot
                          && a.Status != AppointmentStatus.Cancelled);
        }

        public bool HealthRecordExists(
    int appointmentId)
        {
            return InMemoryDatabase.HealthRecords
                .Any(hr => hr.AppointmentId == appointmentId);
        }

    }
}