using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HealthAppWebAPI.Enums;
using HealthAppWebAPI.Models.Dtos;
using HealthAppWebAPI.Repositories.Interfaces;
using HealthAppWebAPI.Services.Interfaces;
using System.Threading.Tasks;
using HealthAppWebAPI.Constants;

namespace HealthAppWebAPI.Services.Impl
{


    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;

        public AppointmentService(
            IAppointmentRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _repo.GetAllAsync();
            return _mapper.Map<List<AppointmentDto>>(appointments);
        }

        public async Task<List<AppointmentDto>> GetUpcomingAppointmentsForDoctorAsync(int doctorId)
        {
            var appointments = await _repo
                .GetUpcomingAppointmentsByDoctorAsync(doctorId);

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }

        public async Task<List<AppointmentDto>> GetAppointmentsForPatientAsync(int patientId)
        {
            var appointments = await _repo
                .GetAppointmentsByPatientAsync(patientId);

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }

        public async Task BookAppointmentAsync(CreateAppointmentDto dto)
        {
            if (dto.ScheduledDate.Date < DateTime.Today)
                throw new Exception("Past date not allowed.");

            if (!TimeSlots.Slots.Contains(dto.TimeSlot))
                throw new Exception("Invalid time slot.");

            if (dto.ScheduledDate.Date == DateTime.Today)
            {
                DateTime slotDateTime = GetSlotDateTime(dto.ScheduledDate, dto.TimeSlot);

                if (slotDateTime < DateTime.Now)
                    throw new Exception("Cannot book a past time slot.");
            }

            if (await _repo.IsDoctorSlotBookedAsync(dto.DoctorId, dto.ScheduledDate, dto.TimeSlot))
                throw new Exception("Doctor already booked.");

            if (await _repo.HasPatientSlotConflictAsync(dto.PatientId, dto.ScheduledDate, dto.TimeSlot))
                throw new Exception("Patient conflict.");

            if (await _repo.HasAppointmentWithDoctorOnSameDayAsync(dto.PatientId, dto.DoctorId, dto.ScheduledDate))
                throw new Exception("Duplicate doctor booking.");

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                ScheduledDate = dto.ScheduledDate,
                TimeSlot = dto.TimeSlot,
                Status = AppointmentStatus.Pending.ToString()
            };

            await _repo.AddAsync(appointment);
        }

        public async Task ConfirmAppointmentAsync(int id)
        {
            var appointment = await _repo.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            appointment.Status = AppointmentStatus.Confirmed.ToString();

            await _repo.UpdateAsync(appointment);
        }

        public async Task CancelAppointmentAsync(int id, string reason)
        {
            var appointment = await _repo.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            if (appointment.Status == AppointmentStatus.Completed.ToString())
                throw new Exception("Cannot cancel completed appointment.");

            appointment.Status = AppointmentStatus.Cancelled.ToString();
            appointment.CancellationReason = reason;

            await _repo.UpdateAsync(appointment);
        }

        private DateTime GetSlotDateTime(DateTime date, string slot)
        {
            string timePart = DateTime.Parse(slot).ToString("HH:mm");

            return DateTime.Parse(
                date.ToString("yyyy-MM-dd") + " " + timePart);
        }
    }
}