namespace MVC_Application.Models
{
    public class Student
    {
            public int StudentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

        // Navigation property for courses
        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public string FullName => $"{FirstName} {LastName}";
        
    }
}
