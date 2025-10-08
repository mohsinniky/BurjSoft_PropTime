
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
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
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

            // Practice Code

            var intersectionLINQ = currentCourseIds.Intersect(newCourseIds).ToList();
            var unionLINQ = currentCourseIds.Union(newCourseIds).ToList();

            var distinctNames = _context.Students
                .Select(s => s.FirstName).Distinct().ToList();
            var countCourses = _context.Courses.Count();
            var averageId = _context.Students.Average(x => x.StudentId);

            var otherColumnSearchLINQ = _context.Students.Where(s=> s.FirstName == "Mohsin");
            var findLINQ = _context.Students.Find(1017);
            var disOrderedStudents = _context.Students.OrderByDescending(s => s.FirstName);
            var orderedStudents = _context.Students.OrderBy(s => s.FirstName);
            var includedStudents = _context.Students.OrderBy(s => s.FirstName).Include(s => s.StudentCourses).ThenInclude(sc => sc.Course);
            var joinStudents = _context.Students
                .Join(_context.StudentCourse,
                s => s.StudentId,
                sc => sc.StudentId,
                (s, sc) => new { s.FirstName, sc.CourseId }
                );


            var tt = (
                      //from st in _context.Students
                      //join sc in _context.StudentCourse on
                      //st.StudentId == sc.StudentId
                      //select

                      //from st in _context.Students
                      //select st.FirstName join 
                      //from sc in _context.StudentCourse select sc.CourseId

                      from st in _context.Students
                      join sc in _context.StudentCourse on st.StudentId equals sc.StudentId
                      group sc by st.FirstName into g

                      select new 
                      {
                          FirstName= g.Key ,
                          CourseIds= g.Select(x=> x.CourseId).ToList(),
                          CourseName= g.Select(x=> x.Course).ToList()
                      }
                       


                      ).ToList();


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetStudentCoursesAsync(int studentId)
        {
            //return await _context.StudentCourse
            //    .Where(sc => sc.StudentId == studentId)
            //    .Include(sc => sc.Course)
            //    .Select(sc => sc.Course)
            //    .ToListAsync();

            return await (from sc in _context.StudentCourse
                          where sc.StudentId == studentId 
                          select sc.Course).ToListAsync();

        }

    }
}
