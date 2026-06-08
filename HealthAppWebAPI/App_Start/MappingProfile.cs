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
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

            // Use the root-level Patient class (from Entity Framework), not Models.Patient
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();

            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentDto, Appointment>();

            CreateMap<HealthRecord, HealthRecordsDto>();
            CreateMap<HealthRecordsDto, HealthRecord>();
        }

    }

}
