using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage =
    "Please select a time slot")]
        public string TimeSlot { get; set; }

        public AppointmentStatus Status
        {
            get;
            set;
        }

        public string CancellationReason
        {
            get;
            set;
        }

        // Navigation Helper

        public string PatientName
        {
            get;
            set;
        }

        public string DoctorName
        {
            get;
            set;
        }
    }
}