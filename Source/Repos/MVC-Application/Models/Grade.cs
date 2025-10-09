namespace MVC_Application.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        // Navigation property
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
