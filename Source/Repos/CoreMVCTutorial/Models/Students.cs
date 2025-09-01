using System.ComponentModel.DataAnnotations;

namespace CoreMVCTutorial.Models
{
    // Enum for Branch selection
    public enum Branch
    {
        CSE,
        ETC,
        Mechanical,
        Electrical
    }

    // Student model with only three fields for practice
    public class Students
    {
        public int? StudentId { get; set; } // Unique identifier
        public string FullName { get; set; } // Student's name
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } // Date of birth
        public Branch Branch { get; set; } // Branch selection
    }
}
