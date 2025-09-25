using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TeacherId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string FatherName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Phone]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Course { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        public bool TermsAndConditions { get; set; }

        // For storing hobbies as comma-separated string in database
        public string Hobbies { get; set; }

        // For storing skills as comma-separated string in database
        public string Skills { get; set; }

        // Helper properties for the UI (not stored in database)
        [NotMapped]
        public List<string> HobbiesList
        {
            get => string.IsNullOrEmpty(Hobbies) ? new List<string>() : Hobbies.Split(',').ToList();
            set => Hobbies = value != null ? string.Join(",", value) : "";
        }

        [NotMapped]
        public List<string> SkillsList
        {
            get => string.IsNullOrEmpty(Skills) ? new List<string>() : Skills.Split(',').ToList();
            set => Skills = value != null ? string.Join(",", value) : "";
        }
    }
}