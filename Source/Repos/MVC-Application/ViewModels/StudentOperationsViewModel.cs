using MVC_Application.DTOs;
using MVC_Application.Models;

namespace MVC_Application.ViewModels
{
    public class StudentOperationsViewModel
    {
        public StudentViewModel StudentForm { get; set; } = new StudentViewModel();
        public List<CourseViewModel> AvailableCourses { get; set; } = new List<CourseViewModel>();
        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
        public List<GradeDto> AvailableGrades { get; set; } = new List<GradeDto>(); // New


        // Simple pagination properties
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    }

}
