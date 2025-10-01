using MVC_Application.Models;

namespace MVC_Application.ViewModels
{
    public class StudentIndexViewModel
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public StudentOperationsViewModel StudentForm { get; set; } = new StudentOperationsViewModel();
    }
}
