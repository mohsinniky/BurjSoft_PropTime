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
    public class Teacher
    {
        public int? TeacherId { get; set; }

        public string FullName { get; set; }

        public string FatherName { get; set; } // Added for modal

        [EmailAddress]
        public string Email { get; set; } // Added for modal

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Phone]
        public string Phone { get; set; } // Added for modal

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Course { get; set; } // Added for modal

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public bool TermsAndConditions { get; set; }

        public List<string> Hobbies { get; set; } = new List<string>();

        public List<string> Skills { get; set; } = new List<string>();
    }
}
