using MVC_Application.Models;

namespace MVC_Application.ViewModels
{
    public class StudentOperationsViewModel
    {
        // For student form (create/edit)
        public StudentViewModel StudentForm { get; set; } = new StudentViewModel();

        // Available courses for dropdown
        public List<CourseViewModel> AvailableCourses { get; set; } = new List<CourseViewModel>();

        // Students list for table
        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();

    }

}
