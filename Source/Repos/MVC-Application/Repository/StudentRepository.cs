
using Microsoft.EntityFrameworkCore;
using MVC_Application.Models;
using MVC_Application.Repository.Interfaces;
using System.Linq;
namespace MVC_Application.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
                .ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EnrollStudentInCoursesAsync(int studentId, List<int> courseIds)
        {
            var currentEnrollments = await _context.StudentCourse
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();

            var currentCourseIds = currentEnrollments.Select(sc => sc.CourseId);
            var newCourseIds = courseIds ?? new List<int>();

            var toRemove = currentEnrollments
                .Where(sc => !newCourseIds.Contains(sc.CourseId));

            var toAdd = newCourseIds
                .Except(currentCourseIds)
                .Select(courseId => new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                });

            _context.StudentCourse.RemoveRange(toRemove);
            _context.StudentCourse.AddRange(toAdd);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetStudentCoursesAsync(int studentId)
        {
            return await _context.StudentCourse
                .Where(sc => sc.StudentId == studentId)
                .Include(sc => sc.Course)
                .Select(sc => sc.Course)
                .ToListAsync();

        }

    }
}
