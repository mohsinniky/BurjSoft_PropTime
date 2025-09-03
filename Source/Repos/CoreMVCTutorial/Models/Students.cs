using System.ComponentModel.DataAnnotations;

namespace CoreMVCTutorial.Models
{
    public class Students
    {
        public int? StudentId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }  

        public string Address { get; set; }

        public Branch Branch { get; set; }

        public bool TermsAndConditions { get; set; } // Checkbox, no validation needed

        [MaxLength(5, ErrorMessage = "You can select up to 5 hobbies")] // Maximum hobbies allowed
        [MinLength(1, ErrorMessage = "Select at least one Hobby")] // Minimum skills required
        public List<string> Hobbies { get; set; } = new List<string>();

        [Required(ErrorMessage = "At least one skill is required")] // Ensures at least one skill is selected
        [MinLength(1, ErrorMessage = "Select at least one skill")] // Minimum skills required
        [MaxLength(10, ErrorMessage = "You can select up to 10 skills")] // Maximum skills allowed
        public List<string> Skills { get; set; } = new List<string>();
    }
}
