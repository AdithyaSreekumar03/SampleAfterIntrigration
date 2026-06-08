using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Models
{
    public class HealthRecord
    {
        public int RecordId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int AppointmentId { get; set; }

        public DateTime VisitDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Diagnosis { get; set; }

        [Required]
        [StringLength(500)]
        public string Prescription { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        // Display properties

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string Specialisation { get; set; }
    }
}