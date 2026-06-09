using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.Models.Dtos
{
    public class CreateDoctorDto
    {
        public string FullName { get; set; }

        public string Specialisation { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal ConsultationFee { get; set; }

        public string DoctorEmail { get; set; }

        public string DoctorPhoneNo { get; set; }
    }
}