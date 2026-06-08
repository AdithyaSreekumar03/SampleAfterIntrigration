using HealthAppMVC.Enums;
using HealthAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthAppMVC.Database
{
    public static class InMemoryDatabase
    {
        public static List<Patient> Patients { get; } = new List<Patient>
        {
            new Patient
            {
                PatientId = 1,
                FullName = "Alice Johnson",
                DateOfBirth = new DateTime(1990, 1, 20),
                Gender = GenderType.Female,
                PhoneNumber = "0123456789",
                Email = "alice@example.com",
                InsuranceId = "INS-1001",
                CreatedDate = DateTime.Now.AddMonths(-3)
            },
            new Patient
            {
                PatientId = 2,
                FullName = "Bob Smith",
                DateOfBirth = new DateTime(1985, 5, 10),
                Gender = GenderType.Male,
                PhoneNumber = "0987654321",
                Email = "bob@example.com",
                InsuranceId = "INS-1002",
                CreatedDate = DateTime.Now.AddMonths(-1)
            },
            new Patient
            {
                PatientId = 3,
                FullName = "Carol White",
                DateOfBirth = new DateTime(1992, 3, 15),
                Gender = GenderType.Female,
                PhoneNumber = "0555666777",
                Email = "carol@example.com",
                InsuranceId = "INS-1003",
                CreatedDate = DateTime.Now.AddMonths(-2)
            },
            new Patient
            {
                PatientId = 4,
                FullName = "David Brown",
                DateOfBirth = new DateTime(1988, 7, 8),
                Gender = GenderType.Male,
                PhoneNumber = "0444555666",
                Email = "david@example.com",
                InsuranceId = "INS-1004",
                CreatedDate = DateTime.Now.AddDays(-15)
            }
        };
        public static List<Doctor> Doctors { get; } = new List<Doctor>
        {
            new Doctor
            {
                DoctorId = 1,
                FullName = "Dr. Emma Clark",
                Specialisation = SpecialisationType.GeneralPhysician,
                DoctorPhoneNo = "0112233445",
                DoctorEmail = "emma.clark@example.com",
                YearsOfExperience = 8,
                ConsultationFee = 50m,
                IsActive = true
            },
            new Doctor
            {
                DoctorId = 2,
                FullName = "Dr. John Doe",
                Specialisation = SpecialisationType.Dermatologist,
                DoctorPhoneNo = "0223344556",
                DoctorEmail = "john.doe@example.com",
                YearsOfExperience = 12,
                ConsultationFee = 75m,
                IsActive = true
            },
            new Doctor
            {
                DoctorId = 3,
                FullName = "Dr. Sarah Wilson",
                Specialisation = SpecialisationType.Cardiologist,
                DoctorPhoneNo = "0334455667",
                DoctorEmail = "sarah.wilson@example.com",
                YearsOfExperience = 15,
                ConsultationFee = 100m,
                IsActive = true
            },
            new Doctor
            {
                DoctorId = 4,
                FullName = "Dr. Michael Lee",
                Specialisation = SpecialisationType.Orthopedic,
                DoctorPhoneNo = "0445566778",
                DoctorEmail = "michael.lee@example.com",
                YearsOfExperience = 10,
                ConsultationFee = 85m,
                IsActive = true
            }
        };
        public static List<Appointment> Appointments { get; } = new List<Appointment>
        {
            new Appointment
            {
                AppointmentId = 1,
                PatientId = 1,
                DoctorId = 1,
                ScheduledDate = DateTime.Today.AddDays(1),
                TimeSlot = "09:00 AM",
                Status = AppointmentStatus.Completed,
                CancellationReason = null,
                PatientName = "Alice Johnson",
                DoctorName = "Dr. Emma Clark"
            },
            new Appointment
            {
                AppointmentId = 2,
                PatientId = 2,
                DoctorId = 2,
                ScheduledDate = DateTime.Today.AddDays(2),
                TimeSlot = "10:00 AM",
                Status = AppointmentStatus.Completed,
                CancellationReason = null,
                PatientName = "Bob Smith",
                DoctorName = "Dr. John Doe"
            },
            new Appointment
            {
                AppointmentId = 3,
                PatientId = 3,
                DoctorId = 3,
                ScheduledDate = DateTime.Today.AddDays(3),
                TimeSlot = "11:00 AM",
                Status = AppointmentStatus.Pending,
                CancellationReason = null,
                PatientName = "Carol White",
                DoctorName = "Dr. Sarah Wilson"
            },
            new Appointment
            {
                AppointmentId = 4,
                PatientId = 4,
                DoctorId = 4,
                ScheduledDate = DateTime.Today.AddDays(4),
                TimeSlot = "02:00 PM",
                Status = AppointmentStatus.Confirmed,
                CancellationReason = null,
                PatientName = "David Brown",
                DoctorName = "Dr. Michael Lee"
            }
        };

        public static List<HealthRecord> HealthRecords { get; } = new List<HealthRecord>
        {
            new HealthRecord
            {
                RecordId = 1,
                PatientId = 1,
                DoctorId = 1,
                AppointmentId = 1,
                VisitDate = DateTime.Today.AddDays(-5),
                Diagnosis = "Common Cold",
                Prescription = "Rest and hydration",
                Notes = "Follow up in 1 week",
                PatientName = "Alice Johnson",
                DoctorName = "Dr. Emma Clark",
                Specialisation = "GeneralPhysician"
            },
            new HealthRecord
            {
                RecordId = 2,
                PatientId = 2,
                DoctorId = 2,
                AppointmentId = 2,
                VisitDate = DateTime.Today.AddDays(-10),
                Diagnosis = "Skin Irritation",
                Prescription = "Topical cream - apply twice daily",
                Notes = "Avoid sun exposure",
                PatientName = "Bob Smith",
                DoctorName = "Dr. John Doe",
                Specialisation = "Dermatologist"
            },
        };
    }
}
