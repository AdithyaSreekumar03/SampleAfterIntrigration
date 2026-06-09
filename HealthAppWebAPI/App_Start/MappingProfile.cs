using AutoMapper;
using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                            .ForMember(dest => dest.PatientName,
                                opt => opt.MapFrom(src => src.Patient.FullName))
                            .ForMember(dest => dest.DoctorName,
                                opt => opt.MapFrom(src => src.Doctor.FullName))
                            .ForMember(dest => dest.Status,
                                opt => opt.MapFrom(src => src.Status.ToString()))
                            .ForMember(dest => dest.ScheduledDate,
                                opt => opt.MapFrom(src => src.ScheduledDate.ToShortDateString()));

            // ===== Doctor =====
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Specialisation,
                    opt => opt.MapFrom(src => src.Specialisation.ToString()));

            CreateMap<CreateDoctorDto, Doctor>()
                .ForMember(dest => dest.Specialisation,
                    opt => opt.Ignore());

            // ===== HealthRecord =====
            CreateMap<HealthRecord, HealthRecordDto>()
                .ForMember(dest => dest.PatientName,
                    opt => opt.MapFrom(src => src.Appointment.Patient.FullName))
                .ForMember(dest => dest.DoctorName,
                    opt => opt.MapFrom(src => src.Appointment.Doctor.FullName));

            // ===== Patient =====
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<CreatePatientDto, Patient>()
                .ForMember(dest => dest.Gender,
                    opt => opt.Ignore());
        }
    }

}
