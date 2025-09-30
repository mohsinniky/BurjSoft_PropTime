using MVC_Application.Models;
using MVC_Application.Repository.Interfaces;
using MVC_Application.Services.Interfaces;
using MVC_Application.ViewModels;

namespace MVC_Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<Student> CreateStudentAsync(Student student, List<int> courseIds)
        {
            // Add student
            var newStudent = await _studentRepository.AddStudentAsync(student);

            // Enroll in courses if any selected
            if (courseIds != null && courseIds.Any())
            {
                await _studentRepository.EnrollStudentInCoursesAsync(newStudent.StudentId, courseIds);
            }

            return newStudent;
        }

        public async Task<Student> UpdateStudentAsync(Student student, List<int> courseIds)
        {
            // Update student
            var updatedStudent = await _studentRepository.UpdateStudentAsync(student);

            // Update course enrollments
            if (courseIds != null)
            {
                await _studentRepository.EnrollStudentInCoursesAsync(updatedStudent.StudentId, courseIds);
            }

            return updatedStudent;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

        public async Task<StudentOperationsViewModel> GetStudentFormDataAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();

            return new StudentOperationsViewModel
            {
                Student = new Student(),
                AvailableCourses = courses,
                SelectedCourseIds = new List<int>()
            };
        }

        public async Task<StudentOperationsViewModel> GetStudentFormDataAsync(int studentId)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId);
            var courses = await _courseRepository.GetAllCoursesAsync();
            var studentCourses = await _studentRepository.GetStudentCoursesAsync(studentId);
            var selectedCourseIds = studentCourses.Select(c => c.CourseId).ToList();

            return new StudentOperationsViewModel
            {
                Student = student,
                AvailableCourses = courses,
                SelectedCourseIds = selectedCourseIds
            };
        }

        public async Task<List<Course>> GetStudentCoursesAsync(int studentId)
        {
            return await _studentRepository.GetStudentCoursesAsync(studentId);
        }
    }
}
