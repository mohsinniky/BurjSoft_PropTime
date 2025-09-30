using MVC_Application.Models;

namespace MVC_Application.Repository.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> AddCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
        Task<List<Student>> GetCourseStudentsAsync(int courseId);
    }
}
