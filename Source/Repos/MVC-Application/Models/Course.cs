namespace MVC_Application.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }

        // Navigation property for students
        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public string CourseDisplay => $"{CourseCode} - {CourseName}";
    }
}
