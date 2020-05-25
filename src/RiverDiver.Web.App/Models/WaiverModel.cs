using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RiverDiver.Web.App.Models
{
    public sealed class WaiverModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [DisplayName("Postal / Zip Code")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayName("Province / State")]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [DisplayName("Certification Number")]
        public string CertificationNumber { get; set; }

        public string Allergies { get; set; }
        public string Medications { get; set; }


        [DisplayName("Heart Attack")]
        public bool HeartAttack { get; set; }

        public bool Bronchitis { get; set; }

        public bool Angina { get; set; }

        public bool Asthma { get; set; }

        public bool Diabetes { get; set; }

        public bool Emphysema { get; set; }

        public bool Seizures { get; set; }

        public bool Stroke { get; set; }


        [DisplayName("High Blood Pressure")]
        public bool HighBloodPressure { get; set; }

        public string Signature { get; set; }
    }
}