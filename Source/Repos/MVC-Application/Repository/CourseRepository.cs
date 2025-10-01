using Microsoft.EntityFrameworkCore;
using MVC_Application.Models;
using MVC_Application.Repository.Interfaces;

namespace MVC_Application.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        //public async Task<Course> GetCourseByIdAsync(int id)
        //{
        //    return await _context.Courses.FindAsync(id);
        //}

        //public async Task<Course> AddCourseAsync(Course course)
        //{
        //    _context.Courses.Add(course);
        //    await _context.SaveChangesAsync();
        //    return course;
        //}

        //public async Task<Course> UpdateCourseAsync(Course course)
        //{
        //    _context.Courses.Update(course);
        //    await _context.SaveChangesAsync();
        //    return course;
        //}

        //public async Task<bool> DeleteCourseAsync(int id)
        //{
        //    var course = await _context.Courses.FindAsync(id);
        //    if (course == null)
        //        return false;

        //    _context.Courses.Remove(course);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<List<Student>> GetCourseStudentsAsync(int courseId)
        //{
        //    return await _context.StudentCourse
        //        .Where(sc => sc.CourseId == courseId)
        //        .Include(sc => sc.Student)
        //        .Select(sc => sc.Student)
        //        .ToListAsync();
        //}
    }
}
