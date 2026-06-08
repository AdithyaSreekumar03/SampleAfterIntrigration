using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.Models.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class PatientDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("(Male|Female|Other)",
            ErrorMessage = "Gender must be Male, Female or Other")]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Insurance ID too long")]
        public string InsuranceId { get; set; }
    }
}