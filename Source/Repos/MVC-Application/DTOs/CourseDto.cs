namespace MVC_Application.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public string CourseDisplay => $"{CourseCode} - {CourseName}";
    }
}
