using MVC_Application.Models;
using MVC_Application.ViewModels;

namespace MVC_Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student, List<int> courseIds);
        Task<Student> UpdateStudentAsync(Student student, List<int> courseIds);
        Task<bool> DeleteStudentAsync(int id);
        Task<StudentOperationsViewModel> GetStudentFormDataAsync();
        Task<StudentOperationsViewModel> GetStudentFormDataAsync(int studentId);
        Task<List<Course>> GetStudentCoursesAsync(int studentId);
        Task<List<Course>> GetAllCoursesAsync();


    }
}
