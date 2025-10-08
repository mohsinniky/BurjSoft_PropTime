using MVC_Application.DTOs;
using MVC_Application.Models;
using MVC_Application.ViewModels;

namespace MVC_Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> UpsertStudentAsync(StudentUpsertDto studentUpsertDto); // Single method for create/update
        Task<bool> DeleteStudentAsync(int id);
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<List<CourseDto>> GetStudentCoursesAsync(int studentId);
        Task<(List<StudentDto> Students, int TotalCount)> GetStudentsPageAsync(int page, int pageSize);

    }
}
