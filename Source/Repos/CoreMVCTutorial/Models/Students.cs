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
        public string FullName { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public Branch Branch { get; set; }
        public bool TermsAndConditions { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>();
        public List<string> Skills { get; set; } = new List<string>();
    }
}
