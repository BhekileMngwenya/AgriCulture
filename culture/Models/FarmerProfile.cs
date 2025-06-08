using System.ComponentModel.DataAnnotations;

namespace culture.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FarmerProfile
    {
        [Key]
        public int Id { get; set; }

        // Personal Information
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        // Contact Information
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        // Address
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Town/City")]
        public string TownCity { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        // Farming Details
        [Display(Name = "Farm Size (hectares)")]
        public double? FarmSize { get; set; }

        [Display(Name = "Type of Farming")]
        public string FarmingType { get; set; }

        [Display(Name = "Number of Workers")]
        public int? NumberOfWorkers { get; set; }

        [Display(Name = "Years of Experience")]
        public int? YearsOfExperience { get; set; }
    }
}
