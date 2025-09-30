using MVC_Application.Models;

namespace MVC_Application.ViewModels
{
    public class StudentOperationsViewModel
    {
        public Student Student { get; set; } = new Student();
        public List<Course> AvailableCourses { get; set; } = new List<Course>();
        public List<int> SelectedCourseIds { get; set; } = new List<int>();
    }
}
