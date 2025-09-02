using System.ComponentModel.DataAnnotations;

namespace CoreMVCTutorial.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public enum Branch
    {
        CSE,
        ETC,
        Mechanical,
        Electrical
    }
    public class Students
    {
        public int? StudentId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")] // Ensures gender is selected
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Address is required")] // Ensures address is entered
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 500 characters")] // Address length restriction
        public string Address { get; set; }

        [Required(ErrorMessage = "Branch is required")] // Ensures branch is selected
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
