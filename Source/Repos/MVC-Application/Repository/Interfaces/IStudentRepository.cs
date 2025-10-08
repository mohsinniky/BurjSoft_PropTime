using MVC_Application.Models;

namespace MVC_Application.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<bool> EnrollStudentInCoursesAsync(int studentId, List<int> courseIds);
        Task<List<Course>> GetStudentCoursesAsync(int studentId);
        Task<(List<Student> Students, int TotalCount)> GetStudentsPageAsync(int page, int pageSize);

    }
}
