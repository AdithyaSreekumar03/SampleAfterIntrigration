using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.Models.Dtos
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string ScheduledDate { get; set; }

        public string TimeSlot { get; set; }

        public string Status { get; set; }
    }
}