using MVC_Application.Models;

namespace MVC_Application.ViewModels
{
    public class SchoolViewModel
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();

    }
}
